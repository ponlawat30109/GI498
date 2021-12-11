using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Inventory_System;
using UnityEngine;
using FoodUtility = _Scripts.InventorySystem.FoodUtility;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
    
    [Serializable]
    public struct IngredientStruct
    {
        public IngredientObject ingredientObject;
        public int ingredientQuantity;

        public void AddQuantity(int value)
        {
            ingredientQuantity += value;
        }
        
        public void SetQuantity(int value)
        {
            ingredientQuantity = value;
        }
    }
    
    public GameObject cookedPrefab;
    public List<IngredientStruct> ingredients;
    public float cookingTime;
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
        isLowSodium = FoodUtility.IsLowSodium(GetTotalSodiumSummary());
    }
    public void ResetFoodObject()
    {
        // Implement Food
        isCooked = false;
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Get Ingredient of Food
    /// </summary>
    /// <returns></returns>
    public List<IngredientStruct> GetIngredient()
    {
        return ingredients;
    }
    
    public float GetTotalCholesterolSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.cholesterol > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.cholesterol * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }

    public float GetTotalCarbohydrateSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.carbohydrate > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.carbohydrate * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalSugarsSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.sugars > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.sugars * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalFiberSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.fiber > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.fiber * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalProteinsSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.proteins > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.proteins * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }

    public float GetTotalFatSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.fat > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.fat * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalSaturatedfatSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.saturatedfat > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.saturatedfat * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalWaterSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.water > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.water * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalPotassiumSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.potassium > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.potassium * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalSodiumSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.sodium > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.sodium * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalCalciumSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.calcium > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.calcium * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalPhosphorusSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.phosphorus > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.phosphorus * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalMagnesiumSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.magnesium > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.magnesium * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalZincSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.zinc > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.zinc * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalIronSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.iron > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.iron * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalManganeseSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.manganese > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.manganese * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalCopperSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.copper > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.copper * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalSeleniumSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.selenium > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.selenium * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalVitaminB1Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminB1 > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminB1 * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalVitaminB2Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminB2 > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminB2 * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalVitaminB3Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminB3 > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminB3 * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalVitaminB5Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminB5 > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminB5 * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalVitaminB6Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminB6 > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminB6 * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalVitaminB7Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminB7 > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminB7 * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalVitaminB9Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminB9 > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminB9 * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalVitaminB12Summary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminB12 > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminB12 * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalVitaminCSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminC > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminC * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalVitaminASummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminA > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminA * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalTotalVitaminDSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminD > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminD * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
    public float GetTotalVitaminESummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminE > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminE * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }

    public float GetTotalVitaminKSummary()
    {
        var summary = 0f;
        
        for (int i = 0; i < ingredients.Count; i++)
        {
            if (ingredients[i].ingredientObject.nutrition.vitaminK > 0)
            {
                summary += ingredients[i].ingredientObject.nutrition.vitaminK * ingredients[i].ingredientQuantity;
            }
        }

        return summary / ingredients.Count;
    }
}
