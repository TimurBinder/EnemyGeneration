using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private TargetPoint _target;
    private Collider _targetCollider;

    public event UnityAction<Enemy> ExitedPlatform;
    public event UnityAction<Enemy> TargetAchieved;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider == _targetCollider)
            TargetAchieved.Invoke(this);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
            ExitedPlatform.Invoke(this);
    }

    public void SetTarget(TargetPoint target)
    {
        _target = target;
        transform.LookAt(_target.transform);
        _targetCollider = _target.GetComponent<Collider>();
    }
}
