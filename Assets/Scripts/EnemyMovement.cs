using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _step = 0.05f;

    private void OnEnable()
    {
        gameObject.GetComponent<Animator>().SetFloat("Speed", _step * _speed);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _step * _speed);
    }
}
