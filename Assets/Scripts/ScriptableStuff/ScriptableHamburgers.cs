using UnityEngine;
using System;

[Serializable]
public enum Ingredients
{
    TopBun,
    Meat,
    Fries,
    Cheese,
    Tomato,
    What,
    Ratao,
    LowerBun,
}

[CreateAssetMenu(fileName = "ScriptableHamburgers", menuName = "HyperBurguer3000/ScriptableHamburgers", order = 0)]
public class ScriptableHamburgers : ScriptableObject
{
    [Header("Hamburger")]
    public string hamburgerName;
    public Ingredients[] hamburgerRecipe = new Ingredients[5];
}