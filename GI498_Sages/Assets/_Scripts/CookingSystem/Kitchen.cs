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
            isCanCook = false;
            isCooking = false;
            _isTrashFull = false;
            _currentIngredientRemoveCount = 0;
        }

        private void Update()
        {
            
            if (ingredientStorage.GetSlotCount() > 0) // If ingredient that in Storage > 0 mean have some ingredient.
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
            if (ingredientStorage.GetRecipe() != null)
            {
                if (isCanCook && ingredientStorage.GetRecipe().isCooked == false)
                {
                    Cooking();
                }
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
            
            var psHandler = Manager.Instance.playerManager.PSHandler();

            //Debug.Log("Interacted with Kitchen stove.");
            
            // Old one...
            /*
            if (psHandler.IsHoldingItem() && ingredientStorage.GetRecipe() == null) // If holding and stove is null
            {
                if (psHandler.currentHoldFoodObject != null && psHandler.currentHoldItemObject == null) // If hold FoodObject
                {
                    if (ingredientStorage.GetRecipe() == null) // If holding food and stove is null
                    {
                        // Add Place Recipe
                        Debug.Log("Add recipe to stove");
                        var item = psHandler.currentHoldFoodObject;
                        PlaceRecipeToStove(item); // + place "item" to stove
                        psHandler.JustTakeOut(item); // - Take Item Out of player hand
                    }
                    else // If holding food but stove is recipe inside
                    {
                        // Do nothing becuase hand is holding FoodObject.
                    }
                }
                // If holding and stove is null and that holding is not food object
                else if (psHandler.currentHoldFoodObject == null && psHandler.currentHoldItemObject != null) // Not FoodObject
                {
                    // Do nothing because that is not add Recipe yet.
                }
            }
            else if(psHandler.IsHoldingItem() && ingredientStorage.GetRecipe() != null) // If holding and stove is not null
            {
                if (psHandler.currentHoldFoodObject != null && psHandler.currentHoldItemObject == null) // If hold FoodObject
                {
                    // Do nothing... recipe inside already inside but.
                }
                else if (psHandler.currentHoldFoodObject == null && psHandler.currentHoldItemObject != null) // Not FoodObject
                {
                    // If holding and stove is not null and that holding is ingredient but
                    if (ingredientStorage.GetRecipe().isCooked) // If food already cooked
                    {
                        // Do nothing can not take because holding ingredient.
                    }
                    else // If food not cook yet
                    {
                        // OpenUI to add ingredient
                        InteractOpenUI();
                    }
                }
            }
            else if (psHandler.IsHoldingItem() == false && ingredientStorage.GetRecipe() != null) // If not holding and stove is not null
            {
                if (isCooking == false) // Check case that player take recipe out while cooking....
                {
                    if (ingredientStorage.GetRecipe().isCooked) // If hand able to hold and food is cook
                    {
                        // Take cooked food
                        ingredientStorage.TakeOutCookedFood();
                        _currentCorrectIngredient = 0;
                    }
                    else
                    {
                        // Take out recipe
                        //ingredientStorage.TakeOutRecipe(); // Same as above but ez to read code...
                        
                        InteractOpenUI();
                    }
                }
            }
            else if(psHandler.IsHoldingItem() == false && ingredientStorage.GetRecipe() == null) //  If not holding and stove also null
            {
                // Do nothing just in case...
                Debug.Log("No Item On Hand and No Recipe at Stove");
            }
            */
            
            /////////////////////////////////////////////////////////////////////////////////////////////////

            // 1 ) If Recipe Inside
            /*
                - เปิดหน้าต่างไม่ว่ายังไงก็ตาม
                - ถ้ามือไม่ว่างหรือถือของจะเปิด KitchenUI
            */
            if (ingredientStorage.GetRecipe() != null)
            {
                // If Holding
                if (psHandler.IsHoldingItem())
                {
                    // If Holding is Food Object (Recipe)
                    if (psHandler.currentHoldFoodObject != null && psHandler.currentHoldItemObject == null)
                    {
                        // Do nothings...
                        // or
                        InteractOpenUI();
                    }
                    // If Holding is Ingredient
                    else if(psHandler.currentHoldFoodObject == null && psHandler.currentHoldItemObject != null)
                    {
                        InteractOpenUI();
                    }
                    
                }
                // If Not Holding
                else
                {
                    // If not cooking
                    if (isCooking == false)
                    {
                        // If food is Cooked
                        if (ingredientStorage.GetRecipe().isCooked)
                        {
                            // Give Cooked Food
                            ingredientStorage.TakeOutCookedFood();
                            _currentCorrectIngredient = 0;
                        }
                        else
                        {
                            // So If stove is have Recipe and Player Not Holding and Not in cooking
                            // Open KitchenUI
                            InteractOpenUI();
                        }
                    }
                    else
                    {
                        //Notification "Food is cooking inside !!"
                        Manager.Instance.notifyManager.CreateNotify("Hot Pot!", "Your food is cooking.");
                    }
                    
                }
            }
            
            // 2 ) If Recipe is not inside
            /*
                - ถ้ามือว่างหรือถืออย่างอื่นที่ไม่ใช่ Recipe ไม่ทำอะไร
                - ถ้ามี Recipe ใส่ Recipe
            */
            else
            {
                // If Holding somethings...
                if (psHandler.IsHoldingItem())
                {
                    //If holding Food Object (Recipe)
                    if (psHandler.currentHoldFoodObject != null && psHandler.currentHoldItemObject == null)
                    {
                        // Add Recipe to stove
                        //Debug.Log("Add recipe to stove");
                        var item = psHandler.currentHoldFoodObject;
                        PlaceRecipeToStove(item); // + place "item" to stove
                        psHandler.JustTakeOut(item); // - Take Item Out of player hand
                    }
                    // If holding Ingredient Object
                    else if (psHandler.currentHoldFoodObject == null && psHandler.currentHoldItemObject != null)
                    {
                        // Do nothing...
                    }
                }
                else
                {
                    // Not holding and because stove is empty not recipe inside.
                    // So do nothing...
                }
            }
        }

        public void InteractOpenUI()
        {
            kitchenUI.gameObject.SetActive(true);
        }

        // Cooking things..
        private void Cooking()
        {
            isCooking = true;
            
            if (currentStoveTime <= ingredientStorage.GetRecipe().cookingTime)
            {
                currentStoveTime += Time.deltaTime;
            }
            else
            {
                if (currentStoveTime >= ingredientStorage.GetRecipe().cookingTime)
                {
                    TakeFinishCookedItem();
                    isCooking = false;
                    isCanCook = true;
                }
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
            bool result = _currentCorrectIngredient == ingredientStorage.GetRecipe().ingredients.Count;

            return result;
        }

        public void CheckWhenIngredientAdd(ItemObject itemToCheck)
        {
            if (IsOneOfIngredient((IngredientObject)itemToCheck))
            {
                _currentCorrectIngredient += 1;
            }
            else
            {
                _currentCorrectIngredient -= 1;
            }
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
                if (ingredientStorage.GetRecipe() == null) // If recipe slot is null (double check)
                {
                    // Set it
                    ingredientStorage.SetRecipe(itemObject);
                    ingredientStorage.InitializeStorageObject(ingredientStorage.GetRecipe().ingredients.Count,true);
                    kitchenUI.InitRecipe(ingredientStorage.GetRecipe());
                }
            }
            else
            {
                Manager.Instance.notifyManager.CreateNotify("Awww Hot!Hot!","Food is cooking.");
                //Debug.Log("Food is cooking can not do anything to stove.");
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

        public void OnApplicationQuit()
        {
            ingredientStorage.SetRecipe(null);
            _currentCorrectIngredient = 0;
            ingredientStorage.GetStorageSlot().Clear();
        }
    }
}
