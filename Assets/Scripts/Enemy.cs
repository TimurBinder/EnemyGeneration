using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public event UnityAction<Enemy> ExitedPlatform;

    private void OnEnable()
    {
        float maxRotationAngle = 180f;
        transform.rotation = Quaternion.Euler(0, Random.Range(-maxRotationAngle, maxRotationAngle), 0);
        gameObject.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
            ExitedPlatform.Invoke(this);
    }
}
