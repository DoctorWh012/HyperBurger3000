using UnityEngine;

public class FoodInator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Material redGlow;
    [SerializeField] private Material greenGlow;
    [SerializeField] private Transform ingredientSpawnPos;
    [SerializeField] private MeshRenderer indicatorCube;

    [Header("Prefabs")]
    [SerializeField] private GameObject topBun;
    [SerializeField] private GameObject lowerBun;
    [SerializeField] private GameObject cheese;
    [SerializeField] private GameObject tomato;
    [SerializeField] private GameObject meat;
    [SerializeField] private GameObject what;
    [SerializeField] private GameObject ratao;
    [SerializeField] private GameObject fries;

    [Header("Settings")]
    [SerializeField] float waitingTime;

    private bool playerOnTrigger = false;
    private bool ableToOrder = true;

    private void Update()
    {
        if (!playerOnTrigger || !GameManager.Instance.gameOngoing) return;
        if (Input.GetKeyDown(KeyCode.E)) UIManager.Instance.EnableDisableFoodInatorUI(ableToOrder);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        UIManager.Instance.uiBottomMessage.SetText("Press [E] To Access The FoodInator3000 Menu");
        playerOnTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        UIManager.Instance.uiBottomMessage.SetText("");
        playerOnTrigger = false;
    }

    public void OrderItem(int ingredientN)
    {
        if (!ableToOrder || !playerOnTrigger) return;

        Ingredients ingredient = (Ingredients)ingredientN;
        print(ingredient);
        switch (ingredient)
        {
            case Ingredients.TopBun:
                Instantiate(topBun, ingredientSpawnPos.position, Quaternion.identity);
                break;
            case Ingredients.Meat:
                Instantiate(meat, ingredientSpawnPos.position, Quaternion.identity);
                break;
            case Ingredients.Cheese:
                Instantiate(cheese, ingredientSpawnPos.position, Quaternion.identity);
                break;
            case Ingredients.Fries:
                Instantiate(fries, ingredientSpawnPos.position, Quaternion.identity);
                break;
            case Ingredients.Ratao:
                Instantiate(ratao, ingredientSpawnPos.position, Quaternion.identity);
                break;
            case Ingredients.LowerBun:
                Instantiate(lowerBun, ingredientSpawnPos.position, Quaternion.identity);
                break;
            case Ingredients.Tomato:
                Instantiate(tomato, ingredientSpawnPos.position, Quaternion.identity);
                break;
            case Ingredients.What:
                Instantiate(what, ingredientSpawnPos.position, Quaternion.identity);
                break;
        }

        UIManager.Instance.DisableEnableOrderButtons(false);
        ableToOrder = false;
        indicatorCube.material = redGlow;

        Invoke("reenableOrdering", waitingTime);
    }

    private void reenableOrdering()
    {
        ableToOrder = true;
        indicatorCube.material = greenGlow;

        if (playerOnTrigger) UIManager.Instance.DisableEnableOrderButtons(true);
    }
}
