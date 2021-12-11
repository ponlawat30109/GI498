using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Inventory_System;
using UnityEngine;
using FoodUtility = _Scripts.InventorySystem.FoodUtility;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
    public GameObject cookedPrefab;
    public List<IngredientObject> ingredients;
    public float cookingTime;
    public int foodQuality;
    public bool isLowSodium;
    public bool isCooked;
    
    private void Awake()
    {
        type = ItemType.Food;
        isLowSodium = false;
        isCooked = false;
    }

    public void CheckNutrition()
    {
        var sodiumSummary = 0f;

        foreach (var ingredient in ingredients)
        {
            if (ingredient.nutrition.sodium > 0)
            {
                sodiumSummary += ingredient.nutrition.sodium;
            }
            
        }

        isLowSodium = FoodUtility.IsLowSodium(sodiumSummary);
    }

    public void SetFoodQuality(int value)
    {
        foodQuality = value;
    }

    public void FoodQualityDrop()
    {
        foodQuality -= 1;
    }

    private void Init(List<IngredientObject> foodIngredients, float cookTime)
    {
        ingredients = foodIngredients;
        cookingTime = cookTime;
    }
    
    public static FoodObject CreateInstance(List<IngredientObject> foodIngredients, float cookTime)
    {
        var data = ScriptableObject.CreateInstance<FoodObject>();
        data.Init(foodIngredients,cookTime);
        return data;
    }

    public void ResetFoodObject()
    {
        // Implement Food
        isCooked = false;
    }
}
