using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TVDisplay : MonoBehaviour
{
    public static TVDisplay Instance;

    // Order on array matters
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI hamburgerName;
    [SerializeField] private TextMeshProUGUI[] ingredientsNames;
    [SerializeField] private Image[] ingredientsIcons;
    [SerializeField] public TextMeshProUGUI preparedQntTxt;

    [Header("Sprites")]
    [SerializeField] private Sprite topBunImg;
    [SerializeField] private Sprite meatImg;
    [SerializeField] private Sprite friesImg;
    [SerializeField] private Sprite cheeseImg;
    [SerializeField] private Sprite tomatoImg;
    [SerializeField] private Sprite unknownImg;
    [SerializeField] private Sprite rataoImg;
    [SerializeField] private Sprite lowerBunImg;

    private string ingredientName;
    private Sprite ingredientImage;


    private void Awake()
    {
        Instance = this;
    }

    public void UpdateDisplayRecipe(ScriptableHamburgers hamburger)
    {
        hamburgerName.SetText(hamburger.hamburgerName);
        for (int i = 0; i < hamburger.hamburgerRecipe.Length; i++)
        {
            GetIngredientNameAndImage(hamburger.hamburgerRecipe[i]);
            ingredientsNames[i].SetText(ingredientName);
            ingredientsIcons[i].sprite = ingredientImage;
        }
    }

    private void GetIngredientNameAndImage(Ingredients ingredient)
    {
        switch (ingredient)
        {
            case Ingredients.TopBun:
                ingredientName = "Top Bun";
                ingredientImage = topBunImg;
                break;
            case Ingredients.Meat:
                ingredientName = "Meat";
                ingredientImage = meatImg;
                break;
            case Ingredients.Fries:
                ingredientName = "Fries";
                ingredientImage = friesImg;
                break;
            case Ingredients.Cheese:
                ingredientName = "Cheese";
                ingredientImage = cheeseImg;
                break;
            case Ingredients.Tomato:
                ingredientName = "Tomato";
                ingredientImage = tomatoImg;
                break;
            case Ingredients.What:
                ingredientName = "Unknown";
                ingredientImage = unknownImg;
                break;
            case Ingredients.Ratao:
                ingredientName = "Ratao";
                ingredientImage = rataoImg;
                break;
            case Ingredients.LowerBun:
                ingredientName = "Lower Bun";
                ingredientImage = lowerBunImg;
                break;
        }
    }
}
