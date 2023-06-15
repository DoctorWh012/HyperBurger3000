using UnityEngine;

public class FoodState : MonoBehaviour
{
    public enum FoodStates { Raw, Cooked, Burnt }

    [Header("Components")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] public FoodStates foodState;

    [Header("Settings")]
    [SerializeField] private float cookedTime;
    [SerializeField] private Material cookedMat;

    [SerializeField] private float burntTime;
    [SerializeField] private Material burntMat;

    [HideInInspector] public float cookTime;

    private void Update()
    {
        if (cookTime >= cookedTime && cookTime < burntTime && foodState != FoodStates.Cooked)
        {
            meshRenderer.material = cookedMat;
            foodState = FoodStates.Cooked;
        }
        else if (cookTime > burntTime && foodState != FoodStates.Burnt)
        {
            meshRenderer.material = burntMat;
            foodState = FoodStates.Burnt;
        }
    }
}
