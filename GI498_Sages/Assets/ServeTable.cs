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
        NPCScript.NPCManager.Instance.CompleteOrder(99);
        
        Manager.Instance.storageManager.ClearIngredientQuantity();
        
        // Clear Food
        parent.currentHoldFoodObject.isCooked = false;

        // Clear Parent (Mini Storage)
        parent.ClearHolding();
        parent.ClearModel();

        // Clear Table
        isCanTakeFinishFood = false;
    }
}
