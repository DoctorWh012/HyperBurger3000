using UnityEngine;
using System.Collections.Generic;

public class CookInator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource audioSource;

    private List<FoodState> foodStates = new List<FoodState>();


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ingredient")) return;
        foodStates.Add(other.GetComponent<FoodState>());
        audioSource.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Ingredient")) return;
        for (int i = 0; i < foodStates.Count; i++)
        {
            foodStates[i].cookTime += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ingredient")) return;
        foodStates.Remove(other.GetComponent<FoodState>());
        if (foodStates.Count == 0) audioSource.Stop();
    }
}
