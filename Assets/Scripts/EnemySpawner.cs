using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private GameObject[] _startPoints;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;
    [SerializeField] private int _repeatRate;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => CreateEnemy(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => DestroyEnemy(obj),
            collectionCheck: true,
            maxSize: _maxSize,
            defaultCapacity: _defaultCapacity
        );
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetEnemy), 0f, _repeatRate);
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_prefab);
        enemy.ExitPlatform += _pool.Release;
        return enemy;
    }

    private void DestroyEnemy(Enemy enemy)
    {
        enemy.ExitPlatform -= _pool.Release;
        Destroy(enemy);
    }

    private void GetEnemy()
    {
        _pool.Get();
    }

    private void ActionOnGet(Enemy enemy)
    {
        GameObject randomStartPoint = _startPoints[Random.Range(0, _startPoints.Length)];
        enemy.gameObject.transform.position = randomStartPoint.transform.position;
        float maxRotationAngle = 180f;
        enemy.gameObject.transform.rotation = Quaternion.Euler(0, Random.Range(-maxRotationAngle, maxRotationAngle), 0);
        enemy.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Enemy enemy) 
    {
        enemy.gameObject.transform.rotation = Quaternion.identity;
        enemy.gameObject.SetActive(false);
    }
}
