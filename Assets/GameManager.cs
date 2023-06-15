using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ScriptableHamburgers[] hamburgers;

    private void Start()
    {
        GetNewRecipe();
    }

    private void GetNewRecipe()
    {
        ScriptableHamburgers newHamburger = hamburgers[Random.Range(0, hamburgers.Length)];
        TVDisplay.Instance.UpdateDisplayRecipe(newHamburger);
    }
}
