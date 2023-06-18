using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableObject : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public Rigidbody rb;
    [SerializeField] public LineRenderer lineRenderer;

    [Header("Settings")]
    [SerializeField] private float maxImpulse;

    [HideInInspector] public Transform target;
    private bool grabbed = false;
    private float moveSpeed = 8;

    private void Start()
    {
        lineRenderer.enabled = false;
        rb.AddForce(Random.Range(0, maxImpulse), Random.Range(0, maxImpulse), Random.Range(0, maxImpulse), ForceMode.Impulse);
    }

    private void Update()
    {
        if (!grabbed) return;
        lineRenderer.SetPosition(0, rb.position);
        lineRenderer.SetPosition(1, target.position);
        rb.position = Vector3.Lerp(rb.position, target.position, Time.deltaTime * moveSpeed);
    }

    public void Grab(Transform targetPos)
    {
        grabbed = true;
        target = targetPos;
        lineRenderer.enabled = true;
        rb.useGravity = false;
    }

    public void Release()
    {
        grabbed = false;
        lineRenderer.enabled = false;
        target = null;
        rb.useGravity = true;
    }
}
