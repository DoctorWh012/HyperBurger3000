using UnityEngine;

public class FallThroughGroundFix : MonoBehaviour
{
    // Hopefully This Is Not Needed :)
    [Header("Components")]
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInParent<Rigidbody>().position = respawnPoint.position;
    }
}
