using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableObject : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rb;

    [HideInInspector] public Transform target;
    private bool grabbed = false;
    private float moveSpeed = 8;

    private void FixedUpdate()
    {
        if (!grabbed) return;
        rb.position = Vector3.Lerp(rb.position, target.position, Time.deltaTime * moveSpeed);
    }

    public void Grab(Transform targetPos)
    {
        grabbed = true;
        target = targetPos;
        rb.useGravity = false;
    }

    public void Release()
    {
        grabbed = false;
        target = null;
        rb.useGravity = true;
    }
}
