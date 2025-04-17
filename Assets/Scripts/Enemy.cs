using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public event UnityAction<Enemy> ExitedPlatform;

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
            ExitedPlatform.Invoke(this);
    }
}
