using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rank Holder", menuName = "Rank/Rank Holder")]
public class RankSystem : ScriptableObject
{
    [Header ("Rank List")]
    [SerializeField] private List<Rank> rankList = new List<Rank>();
    public List<Rank> RankList
    {
        get
        {
            return rankList;
        }
    }

    [Header ("Base Element")]
    [SerializeField] private List<FoodObject> baseFoodList = new List<FoodObject>();
    public List<FoodObject> BaseFoodList
    {
        get
        {
            return baseFoodList;
        }
    }
    [SerializeField] private List<IngredientObject> baseIngredientList = new List<IngredientObject>();
    public List<IngredientObject> BaseIngerdientList
    {
        get
        {
            return baseIngredientList;
        }
    }

    private List<FoodObject> foodList = new List<FoodObject>(); //Food from player rank
    private List<IngredientObject> ingredientList = new List<IngredientObject>(); //Ingredient from player rank

    public void InitialHolder()
    {
        foodList.Clear();
        ingredientList.Clear();
        foreach (FoodObject food in baseFoodList)
        {
            foodList.Add(food);
        }
        foreach(IngredientObject ingredient in baseIngredientList)
        {
            ingredientList.Add(ingredient);
        }
    }

    public void AddFoodList(FoodObject food)
    {
        foodList.Add(food);
    }

    public void AddIngredientList(IngredientObject ingredient)
    {
        ingredientList.Add(ingredient);
    }
}
