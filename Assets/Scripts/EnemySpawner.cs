using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private TargetPoint _targetPoint;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;
    [SerializeField] private float _repeatRate;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => CreateEnemy(),
            actionOnGet: (obj) => OnActionGet(obj),
            actionOnRelease: (obj) => OnActionRelease(obj),
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
        while (enabled)
        {
            Enemy enemy = _pool.Get();
            enemy.SetTarget(_targetPoint);
            enemy.ExitedPlatform += _pool.Release;
            enemy.TargetAchieved += _pool.Release;
            enemy.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnActionGet(Enemy enemy)
    {
        enemy.gameObject.transform.position = _startPoint.position;
    }

    private void OnActionRelease(Enemy enemy) 
    {
        enemy.gameObject.transform.rotation = Quaternion.identity;
        enemy.ExitedPlatform -= _pool.Release;
        enemy.TargetAchieved -= _pool.Release;
        enemy.gameObject.SetActive(false);
    }
}
