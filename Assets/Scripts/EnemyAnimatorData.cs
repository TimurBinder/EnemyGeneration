using UnityEngine;

public class EnemyAnimatorData : MonoBehaviour
{
    private Animator _animator;
    private EnemyMovement _enemyMovement;

    public static readonly int Speed = Animator.StringToHash(nameof(Speed));

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    private void OnEnable()
    {
        _animator.SetFloat(Speed, _enemyMovement.Speed);
    }
}
