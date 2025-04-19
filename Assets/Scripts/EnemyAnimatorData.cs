using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimatorData : MonoBehaviour
{
    public static readonly int Speed = Animator.StringToHash(nameof(Speed));

    private Animator _animator;
    private EnemyMovement _enemyMovement;

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
