using UnityEngine;

public class CookInator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource audioSource;

    private FoodState foodState;
    private bool compatibleFoodOnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ingredient")) return;
        compatibleFoodOnTrigger = true;
        foodState = other.GetComponent<FoodState>();
        audioSource.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!compatibleFoodOnTrigger || !other.CompareTag("Ingredient")) return;
        foodState.cookTime += Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!compatibleFoodOnTrigger || !other.CompareTag("Ingredient")) return;
        foodState = null;
        compatibleFoodOnTrigger = false;
        audioSource.Stop();
    }
}
