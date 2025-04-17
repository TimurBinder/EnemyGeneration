using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform[] _startPoints;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;
    [SerializeField] private float _repeatRate;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => CreateEnemy(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            maxSize: _maxSize,
            defaultCapacity: _defaultCapacity
        );
    }

    private void Start()
    {
        StartCoroutine(GetEnemy(_repeatRate));
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_prefab);
        return enemy;
    }

    private IEnumerator GetEnemy(float delay)
    {
        while (true)
        {
            Enemy enemy = _pool.Get();
            enemy.ExitedPlatform += _pool.Release;
            yield return new WaitForSeconds(delay);
        }
    }

    private void ActionOnGet(Enemy enemy)
    {
        Transform randomStartPoint = _startPoints[Random.Range(0, _startPoints.Length)];
        enemy.gameObject.transform.position = randomStartPoint.position;
        float maxRotationAngle = 180f;
        enemy.gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(-maxRotationAngle, maxRotationAngle), 0);
        enemy.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Enemy enemy) 
    {
        enemy.gameObject.transform.rotation = Quaternion.identity;
        enemy.ExitedPlatform -= _pool.Release;
        enemy.gameObject.SetActive(false);
    }
}
