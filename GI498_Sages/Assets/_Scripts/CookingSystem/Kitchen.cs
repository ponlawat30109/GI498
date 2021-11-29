using System;
using System.Collections.Generic;
using _Scripts.CookingSystem.UI;
using _Scripts.InventorySystem;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.ManagerCollection;
using UnityEngine;

namespace _Scripts.CookingSystem
{
    public class Kitchen : MonoBehaviour
    {
        

        [Header("Kitchen Component")]
        [SerializeField] private KitchenUI kitchenUI;
        [SerializeField] private KitchenStorage ingredientStorage;
        [SerializeField] private IngredientSlotUI currentSelectSlot;
        [SerializeField] private StoveStatusUI stoveStatusUI;

        [SerializeField] private int maxSlot;
        [SerializeField] private bool isStackable;
        [SerializeField] private bool isSlotUISelectable;

        [Header("Cooking Processing")] 
        [SerializeField] private float currentStoveTime;
        [SerializeField] private bool isCanCook;
        [SerializeField] private bool isCooking;
        private int _currentCorrectIngredient;
        
        [Header("Kitchen Trash")]
        private bool _isTrashFull;
        private int _currentIngredientRemoveCount = 0;
        private int maxIngredientRemoveable = 4;

        private void Start()
        {
            _isTrashFull = false;
            _currentIngredientRemoveCount = 0;
        }

        private void Update()
        {
            // If ingredient that in Storage > 0 mean have some item.
            if (ingredientStorage.GetSlotCount() > 0)
            {
                // Check is can cook
                if (IsCanCook() && ingredientStorage.GetRecipe().isCooked == false)
                {
                    // Cooking UI
                    isCanCook = true;
                }
                else
                {
                    // TODO : Change UI color or something...
                    if (ingredientStorage.GetRecipe().isCooked)
                    {
                        // Show Finish Cooked UI
                        stoveStatusUI.SetCurrentStatus(StoveStatusUI.StatusEnum.Finish);
                    }
                    else if (ingredientStorage.GetSlotCount() < ingredientStorage.GetRecipe().ingredients.Count && ingredientStorage.GetRecipe().isCooked == false) // If current Ingredient < need ingredient
                    {
                        // Show Waiting UI
                        stoveStatusUI.SetCurrentStatus(StoveStatusUI.StatusEnum.Wait);
                    }
                    else if (_isTrashFull)
                    {
                        // Show Trash UI
                        stoveStatusUI.SetCurrentStatus(StoveStatusUI.StatusEnum.Trash);
                    }
                    else
                    {
                        // ...
                    }
                }
            }
            
            // Auto Cooking Process
            if (isCanCook && ingredientStorage.GetRecipe().isCooked == false)
            {
                Cooking();
            }

            // Check Trash
            if (_currentIngredientRemoveCount >= maxIngredientRemoveable && _isTrashFull == false)
            {
                // Kitchen Trash full need to throw trash...
                Debug.Log($"Kitchen trash is full...");
                _isTrashFull = true;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public void Interacted()
        {
            if (ingredientStorage.GetRecipe() != null) // If stove have Recipe
            {
                if (ingredientStorage.GetRecipe().isCooked == false)  // Recipe un cooked
                {
                    kitchenUI.gameObject.SetActive(true);
                }
                else // Recipe is cooked
                {
                    InteractStoveElseCase();
                }
            }
            else // If stove don not have Recipe
            {
                InteractStoveElseCase();
            }
        }
        
        public void InteractStoveElseCase()
        {
            var psHandler = Manager.Instance.playerManager.PSHandler();

            if (ingredientStorage.GetRecipe() == null)
            {
                if (psHandler.IsHoldingItem())
                {
                    var item = psHandler.currentHoldFoodObject;
                    PlaceRecipeToStove(item); // + place "item" to stove
                    psHandler.JustTakeOut(item); // - Take Item Out of player hand
                }
                else if(psHandler.IsHoldingItem() == false && ingredientStorage.GetRecipe() == null)
                {
                    Debug.Log("No Item On Hand and No Recipe at Stove");
                }
            }
            else
            {
                // !!! Player hand need free space before do below method
                TakeRecipeFromStove();
            }
        }

        // Cooking things..
        private void Cooking()
        {
            isCooking = true;
            
            if (currentStoveTime < ingredientStorage.GetRecipe().cookingTime)
            {
                if (currentStoveTime >= ingredientStorage.GetRecipe().cookingTime)
                {
                    isCooking = false;
                    TakeFinishCookedItem();
                }

                currentStoveTime += Time.deltaTime;
            }
        }
        
        private void TakeFinishCookedItem() // Using this method by Auto cooking process
        {
            var recipeItem = ingredientStorage.GetRecipe();
            
            recipeItem.isCooked = true;
            recipeItem.CheckNutrition();

            // Comment Code Below locate to KitchenStorage Take out case Recipe is Cooked
            //var psHandler = Manager.Instance.playerManager.PSHandler();
            //psHandler.JustPutInFood(recipeItem);

        }

        private bool IsCanCook()
        {
            var result = false;

            foreach (var slot in ingredientStorage.GetStorageSlot())
            {
                if (IsOneOfIngredient(slot.item))
                {
                    _currentCorrectIngredient += 1;
                }
            }

            if (_currentCorrectIngredient == ingredientStorage.GetRecipe().ingredients.Count)
            {
                result = true;
            }

            return result;
        }

        public bool IsOneOfIngredient(IngredientObject ingredient)
        {
            if (ingredientStorage.GetRecipe().ingredients.Contains(ingredient))
            {
                return true;
            }

            return false;
        }

        // Storage things...
        private void PlaceRecipeToStove(FoodObject itemObject)
        {
            if (isCooking == false) // Check case that player take recipe out while cooking....
            {
                if (ingredientStorage.GetRecipe() == null) // If recipe slot is null
                {
                    // Set it
                    ingredientStorage.SetRecipe(itemObject);
                    ingredientStorage.InitializeStorageObject(ingredientStorage.GetRecipe().ingredients.Count,true);
                    kitchenUI.InitRecipe(ingredientStorage.GetRecipe());
                }
            }
            else
            {
                Debug.Log("Food is cooking can not do anything to stove.");
            }
        }

        private void TakeRecipeFromStove()
        {
            if (isCooking == false) // Check case that player take recipe out while cooking....
            {
                if (ingredientStorage.GetRecipe() != null)
                {
                    if (ingredientStorage.GetRecipe().isCooked) // If it cooked return cooked item
                    {
                        ingredientStorage.TakeOutCookedFood();
    
                    }
                    else // Return recipe
                    {
                        ingredientStorage.TakeOutRecipe(); // Same as above but ez to read code...
                    }
                }
            }
        }
        
        // Storage UI Things...

        public bool IsUIOpen()
        {
            return kitchenUI.gameObject.activeSelf;
        }
        
        public void GrabTrash()
        {
            var psHandler = Manager.Instance.playerManager.PSHandler();
            
            if (_isTrashFull && psHandler.IsHoldingItem() == false)
            {
                psHandler.JustPutIn(ingredientStorage.GetTrashItem());
                _currentIngredientRemoveCount = 0;
                _isTrashFull = false;
            }
        }
        
        public void RemoveAtSelectSlot()
        {
            var itemToRemove = (IngredientObject) currentSelectSlot.GetItem();
            // Remove Item
            ingredientStorage.RemoveItem(itemToRemove);
            // Trash Count
            AddTrashCount();
            // Remove Slot
            kitchenUI.UpdateUI();
        }

        public bool IsTrashFull()
        {
            return _isTrashFull;
        }
        
        public void AddTrashCount()
        {
            _currentIngredientRemoveCount += 1;
        }
        
        public void CloseUI()
        {
            kitchenUI.gameObject.SetActive(false);
        }
        
        public KitchenStorage GetStorageObject()
        {
            return ingredientStorage;
        }
        
        // Slot UI
        public bool IsSlotUISelectable
        {
            get => isSlotUISelectable;
            set => isSlotUISelectable = value;
        }
        
        public void SetSelectSlot(IngredientSlotUI itemFridgeSlotUI)
        {
            currentSelectSlot = itemFridgeSlotUI;
        }

        public void DeSelectSlot()
        {
            SetSelectSlot(null);
            Debug.Log("Deselect Slot");
        }

        public IngredientSlotUI GetSelectSlot()
        {
            return currentSelectSlot;
        }

        // Player out of collider range
        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                kitchenUI.gameObject.SetActive(false);
            }
        }
    }
}
