using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Inventory_System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
    public GameObject cookedPrefab;
    public List<IngredientObject> ingredients;
    public bool isLowSodium;
    public bool isCooked;
    
    private void Awake()
    {
        type = ItemType.Ingredient;
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
}
