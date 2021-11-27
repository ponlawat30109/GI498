using System;
using System.Collections.Generic;
using _Scripts.InventorySystem;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.ManagerCollection;
using UnityEngine;

namespace _Scripts.CookingSystem
{
    public class Kitchen : MonoBehaviour
    {
        private FoodObject recipeItem;
        private int currentCorrectIngredient;
        
        private KitchenStorage ingredientStorage;

        private void Update()
        {
            // If ingredient that in Storage > 0 mean have some item.
            if (ingredientStorage.GetSlotCount() > 0)
            {
                // Check is can cook
                if (IsCanCook())
                {
                    // TODO : Change UI color or something...
                    // PS. Can Change Model to Cooked Model
                    // - recipeItem.cookedPrefab; <- Prefab. Path -> _ScriptableObject/ItemCollection/Foods (Recipe)
                    
                    // Test Button
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        TakeCooked();
                    }
                    
                    
                }
                else
                {
                    // TODO : Change UI color or something...
                }
            }
        }

        private void TakeCooked()
        {
            // TODO : ... Implement this

            recipeItem.isCooked = true;
            recipeItem.CheckNutrition();
            
            // Example
            if (recipeItem.isLowSodium)
            {
                // somethings.......
            }
            
            // TODO : Put This Recipe to Player Hand [Done] 
            // TODO : Implement PlayerStorageHandler for Later [Done] 
            var psHandler = Manager.Instance.playerManager.PSHandler();
            psHandler.JustPutInFood(recipeItem);

        }

        private bool IsCanCook()
        {
            var result = false;

            foreach (var slot in ingredientStorage.GetStorageSlot())
            {
                if (IsOneOfIngredient(slot.item))
                {
                    currentCorrectIngredient += 1;
                }
            }

            if (currentCorrectIngredient == recipeItem.ingredients.Count)
            {
                result = true;
            }

            return result;
        }

        private bool IsOneOfIngredient(IngredientObject ingredient)
        {
            if (recipeItem.ingredients.Contains(ingredient))
            {
                return true;
            }

            return false;
        }

        private void SetRecipe(FoodObject itemObject)
        {
            if (recipeItem != null)
            {
                recipeItem = itemObject;
                ingredientStorage.InitializeStorageObject(recipeItem.ingredients.Count,true);
            }
            else
            {
                ClearRecipe();
            }
        }

        private void ClearRecipe()
        {
            recipeItem = null;
        }
    }
}
