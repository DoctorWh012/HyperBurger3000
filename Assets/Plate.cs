using UnityEngine;

public class Plate : MonoBehaviour
{
    // Player Can Stack More Than 5 Ingredients, But Here i'll Save Only The 5 Firsts As All Hamburgers Only Have 5 Ingredients
    // Also If The Player Stacks More Than 5 The Hamburger Will Count As Incorrect On Delivery
    public Ingredients[] stackedIngredients = new Ingredients[5];
    public int stackedIngredientsQnt = 0;

    public void AddToStack(Ingredients ingredient)
    {
        if (stackedIngredientsQnt < 5) stackedIngredients[stackedIngredientsQnt] = ingredient;
        stackedIngredientsQnt++;
    }
}
