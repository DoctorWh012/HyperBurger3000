using UnityEngine;

public class Stackable : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public Rigidbody rb;
    [SerializeField] public GrabbableObject grabbableObject;
    [SerializeField] public BoxCollider triggerCol;
    [SerializeField] public BoxCollider col;
    [SerializeField] public Transform stackingPoint;

    private void Start()
    {
        if (!this.CompareTag("Plate")) triggerCol.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled) return;
        if (!other.CompareTag("Ingredient") || other.gameObject.layer == this.gameObject.layer) return;

        Destroy(triggerCol);
        StackIngredient(other);
        this.enabled = false;
    }

    private void StackIngredient(Collider other)
    {
        other.gameObject.layer = LayerMask.NameToLayer("Stacked");
        print($"Trying To Stack {other.name} On {triggerCol.name}");
        Stackable ingredient = other.GetComponent<Stackable>();

        // Disabling rb
        Destroy(ingredient.grabbableObject);
        Destroy(ingredient.rb);

        ingredient.triggerCol.enabled = true;
        ingredient.transform.parent = stackingPoint;
        ingredient.transform.localPosition = Vector3.zero;
        ingredient.transform.localRotation = Quaternion.identity;
    }
}
