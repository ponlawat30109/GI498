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
    [SerializeField] private List<IngredientObject> baseIngredientList = new List<IngredientObject>();

    [Header("Rank Element")]
    [SerializeField] private List<FoodObject> foodList = new List<FoodObject>(); //Food from player rank
    public List<FoodObject> FoodList
    {
        get
        {
            if (foodList.Count == 0)
            {
                Debug.Log(name + ": FoodList is null");
                return baseFoodList;
            }
            return foodList;
        }
    }
    [SerializeField] private List<IngredientObject> ingredientList = new List<IngredientObject>(); //Ingredient from player rank
    public List<IngredientObject> IngredientList
    {
        get
        {
            return ingredientList;
        }
    }

    public void InitialHolder()
    {
        if(PlayerPrefs.GetInt("OnSetRankHolder",0) == 1)
        {
            return;
        }
        PlayerPrefs.SetInt("OnSetRankHolder", 1);

        ClearList();
        
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
        if (!foodList.Contains(food))
            foodList.Add(food);
    }

    public void AddIngredientList(IngredientObject ingredient)
    {
        if (!ingredientList.Contains(ingredient))
            ingredientList.Add(ingredient);
    }

    public void ClearList()
    {
        foodList.Clear();
        ingredientList.Clear();
        PlayerPrefs.SetInt("OnSetRankHolder", 0);
    }
}
