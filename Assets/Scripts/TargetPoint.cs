using UnityEngine;

[RequireComponent (typeof(Collider))]
public class TargetPoint : MonoBehaviour
{
    public Collider Collider { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<Collider>();
    }
}
