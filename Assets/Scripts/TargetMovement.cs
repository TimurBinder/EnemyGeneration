using System.Collections;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Platform _platformArea;

    private Vector3 _target;
    private float _step = 0.05f;

    public float Speed => _speed * _step;

    private void Start()
    {
        _target = GetTarget();
        transform.LookAt(_target);
        StartCoroutine(MoveStep());
    }

    private IEnumerator MoveStep()
    {
        while (enabled)
        {
            if (Vector3.Distance(transform.position, _target) < 0.1f)
            {
                _target = GetTarget();
                transform.LookAt(_target);
            }

            transform.Translate(Vector3.forward * Time.deltaTime * Speed);

            yield return null;
        }
    }

    private Vector3 GetTarget()
    {
        float minPositionX = _platformArea.transform.position.x;
        float minPositionZ = _platformArea.transform.position.z;
        float maxPositionX = _platformArea.Collider.bounds.extents.x;
        float maxPositionz = _platformArea.Collider.bounds.extents.z;
        float positionX = Random.Range(minPositionX, maxPositionX);
        float positionZ = Random.Range(minPositionZ, maxPositionz);
        float positionY = _platformArea.transform.position.y;

        return new Vector3(positionX, positionY, positionZ);
    }
}
