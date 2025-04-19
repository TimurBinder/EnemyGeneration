using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private TargetPoint _target;

    public event UnityAction<Enemy> ExitedPlatform;
    public event UnityAction<Enemy> TargetAchieved;

    private void Update()
    {
        transform.LookAt(_target.transform);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider == _target.Collider)
            TargetAchieved?.Invoke(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _target)
            TargetAchieved?.Invoke(this);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
            ExitedPlatform?.Invoke(this);
    }

    public void SetTarget(TargetPoint target)
    {
        _target = target;
    }
}
