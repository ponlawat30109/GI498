using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.InventorySystem;
using _Scripts.ManagerCollection;
using UnityEngine;

public class ServeTable : MonoBehaviour
{
    [SerializeField] private MiniStorage parent;
    [SerializeField] private bool isCanTakeFinishFood;

    private void Start()
    {
        isCanTakeFinishFood = false;
    }

    private void Update()
    {
        CheckIsCanTake();

        if (isCanTakeFinishFood)
        {
            TakeFinishFood();
        }
    }

    public void CheckIsCanTake()
    {
        if (parent.currentHoldFoodObject != null && parent.currentHoldItemObject == null)
        {
            if (parent.currentHoldFoodObject.isCooked)
            {
                isCanTakeFinishFood = true;
            }
        }
        else
        {
            isCanTakeFinishFood = false;
        }
    }
    
    public void TakeFinishFood()
    {
        // Give Exp to Player or Show Summary UI
        var nutrition = CreateNutrition();
        DIshScoreManager.Instance.Calculate(nutrition);
        NPCScript.NPCManager.Instance.CompleteOrder(1);

        Manager.Instance.storageManager.ClearIngredientQuantity();
        
        // Clear Food
        parent.currentHoldFoodObject.isCooked = false;

        // Clear Parent (Mini Storage)
        parent.ClearHolding();
        parent.ClearModel();

        // Clear Table
        isCanTakeFinishFood = false;
    }

    public Nutrition CreateNutrition()
    {
        var nutrition = new Nutrition();

        nutrition.cholesterol = parent.currentHoldFoodObject.GetTotalCholesterolSummary();
        nutrition.carbohydrate = parent.currentHoldFoodObject.GetTotalCarbohydrateSummary();
        nutrition.sugars = parent.currentHoldFoodObject.GetTotalSugarsSummary();
        nutrition.fiber = parent.currentHoldFoodObject.GetTotalFiberSummary();
        nutrition.proteins = parent.currentHoldFoodObject.GetTotalProteinsSummary();
        nutrition.fat = parent.currentHoldFoodObject.GetTotalFatSummary();
        nutrition.saturatedfat = parent.currentHoldFoodObject.GetTotalSaturatedfatSummary();
        nutrition.water = parent.currentHoldFoodObject.GetTotalWaterSummary();
        nutrition.potassium = parent.currentHoldFoodObject.GetTotalPotassiumSummary();
        nutrition.sodium = parent.currentHoldFoodObject.GetTotalSodiumSummary();
        nutrition.calcium = parent.currentHoldFoodObject.GetTotalCalciumSummary();
        nutrition.phosphorus = parent.currentHoldFoodObject.GetTotalPhosphorusSummary();
        nutrition.magnesium = parent.currentHoldFoodObject.GetTotalMagnesiumSummary();
        nutrition.zinc = parent.currentHoldFoodObject.GetTotalZincSummary();
        nutrition.iron = parent.currentHoldFoodObject.GetTotalIronSummary();
        nutrition.manganese = parent.currentHoldFoodObject.GetTotalManganeseSummary();
        nutrition.copper = parent.currentHoldFoodObject.GetTotalCopperSummary();
        nutrition.selenium = parent.currentHoldFoodObject.GetTotalSeleniumSummary();
        nutrition.vitaminB1 = parent.currentHoldFoodObject.GetTotalVitaminB1Summary();
        nutrition.vitaminB2 = parent.currentHoldFoodObject.GetTotalVitaminB2Summary();
        nutrition.vitaminB3 = parent.currentHoldFoodObject.GetTotalVitaminB3Summary();
        nutrition.vitaminB5 = parent.currentHoldFoodObject.GetTotalVitaminB5Summary();
        nutrition.vitaminB6 = parent.currentHoldFoodObject.GetTotalVitaminB6Summary();
        nutrition.vitaminB7 = parent.currentHoldFoodObject.GetTotalVitaminB7Summary();
        nutrition.vitaminB9 = parent.currentHoldFoodObject.GetTotalVitaminB9Summary();
        nutrition.vitaminB12 = parent.currentHoldFoodObject.GetTotalVitaminB12Summary();
        nutrition.vitaminC = parent.currentHoldFoodObject.GetTotalVitaminCSummary();
        nutrition.vitaminA = parent.currentHoldFoodObject.GetTotalVitaminASummary();
        nutrition.vitaminD = parent.currentHoldFoodObject.GetTotalTotalVitaminDSummary();
        nutrition.vitaminE = parent.currentHoldFoodObject.GetTotalVitaminESummary();
        nutrition.vitaminK = parent.currentHoldFoodObject.GetTotalVitaminKSummary();

        return nutrition;
    }
}
