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
    public List<IngredientObject> specialIngredients;
    public float cookingTime;
    public bool isLowSodium;
    public bool isCooked;
    
    private void Awake()
    {
        type = ItemType.Food;
        isLowSodium = false;
        isCooked = false;
    }

    public void Init(string _itemName,Sprite _itemIcon,GameObject prefab,GameObject _cookedPrefab,List<IngredientObject> normalIngredient,List<IngredientObject> speicalIngredient,float _cookingTime)
    {
        base.itemName = _itemName;
        base.itemIcon = _itemIcon;
        ingamePrefab = prefab;
        cookedPrefab = _cookedPrefab;
        ingredients = normalIngredient;
        specialIngredients = speicalIngredient;
        cookingTime = _cookingTime;
    }

    public FoodObject CreateInstance(string _itemName,Sprite _itemIcon,GameObject prefab,GameObject _cookedPrefab,List<IngredientObject> normalIngredient,List<IngredientObject> speicalIngredient,float _cookingTime)
    {
        var data = ScriptableObject.CreateInstance<FoodObject>();
        data.Init(_itemName,_itemIcon,prefab,_cookedPrefab,normalIngredient,speicalIngredient,_cookingTime);
        data.isCooked = false;
        
        return data;
    }
    
    public bool IsHaveSpecialIngredients()
    {
        if (specialIngredients.Count > 0)
        {
            return true;
        }
        
        return false;
    }
    
    public void CheckNutrition()
    {
        isLowSodium = FoodUtility.IsLowSodium(GetTotalSodiumSummary());
    }
    public void ResetFoodObject()
    {
        // Implement Food
        isCooked = false;
    }

    // New Method for Test

    public IngredientObject GetIngredientByName(IngredientObject toFindIngredeint)
    {
        if (toFindIngredeint.isSpecialIngredient == false)
        {
            foreach (var ingredient in ingredients)
            {
                if (ingredient == toFindIngredeint)
                {
                    return ingredient;
                }
            }
        }
        else
        {
            foreach (var ingredient in specialIngredients)
            {
                if (ingredient == toFindIngredeint)
                {
                    return ingredient;
                }
            }
        }
        
        return null;
    }
    
    public void AddIngredient(IngredientObject itemToAdd)
    {
        // If found item In side
        if (GetIngredientByName(itemToAdd) != null)
        {
            //Debug.Log("While add ingredient is found same ingredient.");
            // Add quantity
            AddIngredientQuantityByItemName(itemToAdd);
        }
        else
        {
            // Create new one
            // Find Where to create new ingredient
            if (itemToAdd.isSpecialIngredient == false)
            {
                IngredientObject newItem = new IngredientObject();
                newItem = itemToAdd;
                newItem.quantity += 1;
        
                ingredients.Add(newItem);
            }
            else
            {
                IngredientObject newItem = new IngredientObject();
                newItem = itemToAdd;
                newItem.quantity += 1;
        
                specialIngredients.Add(newItem);
            }
        }
        
    }

    public void RemoveIngredient(IngredientObject itemToRemove)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i] != null)
            {
                if (ingredients[i].itemName == itemToRemove.itemName)
                {
                    ingredients.RemoveAt(i);
                }
            }
        }
        
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i] != null)
            {
                if (specialIngredients[i].itemName == itemToRemove.itemName)
                {
                    specialIngredients.RemoveAt(i);
                }
            }
        }
    }
    
    public void AddIngredientQuantityByItemName(IngredientObject itemToAdd)
    {
        var item = GetIngredientByName(itemToAdd);
        item.AddQuantity();
        
        Debug.Log($"Update quantity {itemToAdd} to {item.quantity}");
        
    }

    public void SubIngredientQuantityByItemName(IngredientObject itemToRemove)
    {
        // Normal
        if (itemToRemove.isSpecialIngredient == false)
        {
            foreach (var ingredient in ingredients)
            {
                if (ingredient.itemName == itemToRemove.itemName)
                {
                    if (ingredient.quantity - 1 <= 0)
                    {
                        RemoveIngredient(ingredient);
                    }
                    else
                    {
                        ingredient.SubQuantity();
                    }
                }
            }

        }
        else
        {
            // Special
            foreach (var ingredient in specialIngredients)
            {
                if (ingredient.itemName == itemToRemove.itemName)
                {
                    if (ingredient.quantity - 1 <= 0)
                    {
                        RemoveIngredient(ingredient);
                    }
                    else
                    {
                        ingredient.SubQuantity();
                    }
                }
            }
        }
    }

    public FoodObject TakeOutCookedFood()
    {
        return this;
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Get Ingredient of Food
    /// </summary>
    /// <returns></returns>
    public List<IngredientObject> GetIngredient()
    {
        return ingredients;
    }
    
    public List<IngredientObject> GetSpecialIngredient()
    {
        return specialIngredients;
    }
    
    public float GetTotalCholesterolSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.cholesterol > 0)
            {
                summary += ingredients[i].nutrition.cholesterol * ingredients[i].quantity;
            }
        }
        
        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.cholesterol > 0)
            {
                summary += specialIngredients[i].nutrition.cholesterol * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }

    public float GetTotalCarbohydrateSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.carbohydrate > 0)
            {
                summary += ingredients[i].nutrition.carbohydrate * ingredients[i].quantity;
            }
        }
        
        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.carbohydrate > 0)
            {
                summary += specialIngredients[i].nutrition.carbohydrate * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalSugarsSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.sugars > 0)
            {
                summary += ingredients[i].nutrition.sugars * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.sugars > 0)
            {
                summary += specialIngredients[i].nutrition.sugars * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalFiberSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.fiber > 0)
            {
                summary += ingredients[i].nutrition.fiber * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.fiber > 0)
            {
                summary += specialIngredients[i].nutrition.fiber * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalProteinsSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.proteins > 0)
            {
                summary += ingredients[i].nutrition.proteins * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.proteins > 0)
            {
                summary += specialIngredients[i].nutrition.proteins * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }

    public float GetTotalFatSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.fat > 0)
            {
                summary += ingredients[i].nutrition.fat * ingredients[i].quantity;
            }
        }
        
        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.fat > 0)
            {
                summary += specialIngredients[i].nutrition.fat * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalSaturatedfatSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.saturatedfat > 0)
            {
                summary += ingredients[i].nutrition.saturatedfat * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.saturatedfat > 0)
            {
                summary += specialIngredients[i].nutrition.saturatedfat * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalWaterSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.water > 0)
            {
                summary += ingredients[i].nutrition.water * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.water > 0)
            {
                summary += specialIngredients[i].nutrition.water * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalPotassiumSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.potassium > 0)
            {
                summary += ingredients[i].nutrition.potassium * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.potassium > 0)
            {
                summary += specialIngredients[i].nutrition.potassium * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalSodiumSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.sodium > 0)
            {
                summary += ingredients[i].nutrition.sodium * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.sodium > 0)
            {
                summary += specialIngredients[i].nutrition.sodium * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalCalciumSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.calcium > 0)
            {
                summary += ingredients[i].nutrition.calcium * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.calcium > 0)
            {
                summary += specialIngredients[i].nutrition.calcium * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalPhosphorusSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.phosphorus > 0)
            {
                summary += ingredients[i].nutrition.phosphorus * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.phosphorus > 0)
            {
                summary += specialIngredients[i].nutrition.phosphorus * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalMagnesiumSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.magnesium > 0)
            {
                summary += ingredients[i].nutrition.magnesium * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.magnesium > 0)
            {
                summary += specialIngredients[i].nutrition.magnesium * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalZincSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.zinc > 0)
            {
                summary += ingredients[i].nutrition.zinc * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.zinc > 0)
            {
                summary += specialIngredients[i].nutrition.zinc * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalIronSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.iron > 0)
            {
                summary += ingredients[i].nutrition.iron * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.iron > 0)
            {
                summary += specialIngredients[i].nutrition.iron * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalManganeseSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.manganese > 0)
            {
                summary += ingredients[i].nutrition.manganese * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.manganese > 0)
            {
                summary += specialIngredients[i].nutrition.manganese * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalCopperSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.copper > 0)
            {
                summary += ingredients[i].nutrition.copper * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.copper > 0)
            {
                summary += specialIngredients[i].nutrition.copper * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalSeleniumSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.selenium > 0)
            {
                summary += ingredients[i].nutrition.selenium * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.selenium > 0)
            {
                summary += specialIngredients[i].nutrition.selenium * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalVitaminB1Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminB1 > 0)
            {
                summary += ingredients[i].nutrition.vitaminB1 * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminB1 > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminB1 * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalVitaminB2Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminB2 > 0)
            {
                summary += ingredients[i].nutrition.vitaminB2 * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminB2 > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminB2 * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalVitaminB3Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminB3 > 0)
            {
                summary += ingredients[i].nutrition.vitaminB3 * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminB3 > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminB3 * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalVitaminB5Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminB5 > 0)
            {
                summary += ingredients[i].nutrition.vitaminB5 * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminB5 > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminB5 * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalVitaminB6Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminB6 > 0)
            {
                summary += ingredients[i].nutrition.vitaminB6 * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminB6 > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminB6 * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalVitaminB7Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminB7 > 0)
            {
                summary += ingredients[i].nutrition.vitaminB7 * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminB7 > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminB7 * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalVitaminB9Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminB9 > 0)
            {
                summary += ingredients[i].nutrition.vitaminB9 * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminB9 > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminB9 * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalVitaminB12Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminB12 > 0)
            {
                summary += ingredients[i].nutrition.vitaminB12 * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminB12 > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminB12 * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    public float GetTotalVitaminCSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminC > 0)
            {
                summary += ingredients[i].nutrition.vitaminC * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminC > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminC * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    
    public float GetTotalVitaminASummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminA > 0)
            {
                summary += ingredients[i].nutrition.vitaminA * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminA > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminA * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    
    public float GetTotalTotalVitaminDSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminD > 0)
            {
                summary += ingredients[i].nutrition.vitaminD * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminD > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminD * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
    
    public float GetTotalVitaminESummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminE > 0)
            {
                summary += ingredients[i].nutrition.vitaminE * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminE > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminE * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }

    public float GetTotalVitaminKSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].nutrition.vitaminK > 0)
            {
                summary += ingredients[i].nutrition.vitaminK * ingredients[i].quantity;
            }
        }

        // Special Ingredient
        for (int i = 0; i < specialIngredients.Count; i++)
        {
            if (specialIngredients[i].nutrition.vitaminK > 0)
            {
                summary += specialIngredients[i].nutrition.vitaminK * specialIngredients[i].quantity;
            }
        }

        return summary / (ingredients.Count + specialIngredients.Count);
    }
}
