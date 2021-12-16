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
        [SerializeField] private FoodObject stoveTempRecipe;
        [SerializeField] private List<IngredientObject> ingredientStructs; // for Debug
        [SerializeField] private List<IngredientObject> specialIngredientStructs;  // for Debug
        [SerializeField] private float currentStoveTime;
        [SerializeField] private bool isCanProcess;
        [SerializeField] private bool isCooking;
        [SerializeField] private int currentCorrectIngredient;
        [SerializeField] private int totalNeedIngredient;

        //[Header("Kitchen Trash")]
        private bool _isWrongIngredientFound;
        //private int _currentIngredientRemoveCount = 0;
        //private int maxIngredientRemoveable = 4;

        private void Start()
        {
            isCanProcess = false;
            isCooking = false;
            //_isTrashFull = false;
            //_currentIngredientRemoveCount = 0;
        }

        private void Update()
        {

            if (stoveTempRecipe != null)
            {
                ingredientStructs = stoveTempRecipe.ingredients;
                specialIngredientStructs = stoveTempRecipe.specialIngredients;
            }

            // Auto Cooking Process
            if (stoveTempRecipe != null)
            {
                if (isCanProcess)
                {
                    Cooking();
                }
            }

            CheckCanProcess();
            CheckStatusUI();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////

        public void CheckCanProcess()
        {
            if (stoveTempRecipe == null)
            {
                return;
            }

            if (currentCorrectIngredient > 0)
            {
                var pass = IsCanCook();

                //Debug.Log($"Current stove status IsCanCook:{pass}, IsCooked:{stoveTempRecipe.isCooked}");

                if (pass && stoveTempRecipe.isCooked == false)
                {
                    isCanProcess = true;
                }
                else
                {
                    isCanProcess = false;
                }
            }
            else
            {
                isCanProcess = false;
            }
        }

        public void CheckStatusUI()
        {
            if (stoveTempRecipe == null)
            {
                stoveStatusUI.SetCurrentStatus(StoveStatusUI.StatusEnum.Wait);
                return;
            }
            else
            {
                // Check UI
                if (stoveTempRecipe.isCooked)
                {
                    // Show Finish Cooked UI
                    stoveStatusUI.SetCurrentStatus(StoveStatusUI.StatusEnum.Finish);
                }
                else if (IsCanCook() == false) // If current Ingredient < need ingredient
                {
                    // Show Waiting UI
                    // stoveStatusUI.SetCurrentStatus(StoveStatusUI.StatusEnum.Wait);
                    stoveStatusUI.SetCurrentStatus(StoveStatusUI.StatusEnum.Cooking);
                }
                else if (IsCanCook())
                {
                    // Show Cooking UI
                    stoveStatusUI.SetCurrentStatus(StoveStatusUI.StatusEnum.Cooking);
                }
                else if (_isWrongIngredientFound)
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

        public FoodObject GetStoveTempRecipe()
        {
            return stoveTempRecipe;
        }

        public void AddStoveTempItem(IngredientObject item)
        {
            stoveTempRecipe.AddIngredient(item);
            ingredientStructs = stoveTempRecipe.ingredients;
            specialIngredientStructs = stoveTempRecipe.specialIngredients;
        }

        public int GetRecipeTotalIngredientQuantity()
        {
            var totalNeed = 0;

            for (int i = 0; i < ingredientStorage.GetRecipe().ingredients.Count; i++)
            {
                if (ingredientStorage.GetRecipe().ingredients[i] != null)
                {
                    totalNeed++; //ingredientStorage.GetRecipe().ingredients[i].quantity;
                }
            }

            for (int i = 0; i < ingredientStorage.GetRecipe().specialIngredients.Count; i++)
            {
                if (ingredientStorage.GetRecipe().specialIngredients[i] != null)
                {
                    totalNeed++; //= ingredientStorage.GetRecipe().specialIngredients[i].quantity;
                }
            }

            totalNeedIngredient = totalNeed;
            return totalNeed;
        }

        public int GetCurrentCorrectIngredient()
        {
            var correctCount = 0;

            for (int j = 0; j < stoveTempRecipe.ingredients.Count; j++)
            {
                // If ingredient is one of need ingredient
                if (IsOneOfIngredient(stoveTempRecipe.ingredients[j]))
                {
                    correctCount += 1;
                }
            }

            for (int j = 0; j < stoveTempRecipe.specialIngredients.Count; j++)
            {
                if (IsOneOfIngredient(stoveTempRecipe.specialIngredients[j]))
                {
                    correctCount += 1;
                }
            }

            return correctCount;
        }

        public void CreateTempRecipe(FoodObject original)
        {
            //FoodObject newTempRecipe = ScriptableObject.CreateInstance("FoodObject") as FoodObject;

            string itemName = original.itemName;
            Sprite itemIcon = original.itemIcon;
            GameObject prefab = original.ingamePrefab;
            GameObject cookedPrefab = original.cookedPrefab;
            List<IngredientObject> normalIngredient = new List<IngredientObject>();
            List<IngredientObject> speicalIngredient = new List<IngredientObject>();
            float cookingTime = original.cookingTime;

            var newTempRecipe = original.CreateInstance(itemName, itemIcon, prefab, cookedPrefab, normalIngredient, speicalIngredient, cookingTime);

            stoveTempRecipe = newTempRecipe;
        }

        public void ClearTempRecipe()
        {
            stoveTempRecipe = null;
        }

        public void Interacted()
        {

            var psHandler = Manager.Instance.playerManager.PSHandler();

            //Debug.Log("Interacted with Kitchen stove.");

            /////////////////////////////////////////////////////////////////////////////////////////////////

            // 1 ) If Recipe Inside
            /*
                - เปิดหน้าต่างไม่ว่ายังไงก็ตาม
                - ถ้ามือไม่ว่างหรือถือของจะเปิด KitchenUI
            */
            if (stoveTempRecipe != null)
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
                    else if (psHandler.currentHoldFoodObject == null && psHandler.currentHoldItemObject != null)
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
                        if (stoveTempRecipe.isCooked)
                        {
                            // Give Cooked Food
                            psHandler.JustPutInFood(stoveTempRecipe.TakeOutCookedFood()); // Add Food to Player
                            ingredientStorage.TakeOutCookedFood(); // Clear Storage
                            currentCorrectIngredient = 0;
                            ClearTempRecipe();
                            //Manager.Instance.storageManager.ClearIngredientQuantity();
                            kitchenUI.ClearSlotList();

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
                        psHandler.JustTakeOut(item); // - Take Item Out of player hand
                        PlaceRecipeToStove(item); // + place "item" to stove

                    }
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

            if (currentStoveTime <= stoveTempRecipe.cookingTime)
            {
                currentStoveTime += Time.deltaTime;
            }
            else
            {
                if (currentStoveTime >= stoveTempRecipe.cookingTime)
                {
                    TakeFinishCookedItem();
                    isCooking = false;
                    isCanProcess = true;
                }
            }
        }

        private void TakeFinishCookedItem() // Using this method by Auto cooking process
        {
            var recipeItem = stoveTempRecipe;

            recipeItem.isCooked = true;
            recipeItem.CheckNutrition();
            isCanProcess = false;
            currentStoveTime = 0;
            CloseUI();
        }

        private bool IsCanCook()
        {

            bool result = false;

            if (GetCurrentCorrectIngredient() >= GetRecipeTotalIngredientQuantity())
            {
                result = true;
                //Debug.Log($"{GetCurrentCorrectIngredient()} / {GetRecipeTotalIngredientQuantity()} To be can process.");
            }

            return result;
        }

        public void CheckWhenIngredientAdd(ItemObject itemToCheck)
        {
            var item = (IngredientObject)itemToCheck;
            // Is One Of Original Ingredient
            if (IsOneOfIngredient(item))
            {
                /*// Loop Ingredient
                for (int i = 0; i < stoveTempRecipe.ingredients.Count; i++)
                {
                    // Compare Ingredient(itemToCheck) that have in Original or not.
                    if (item.isSpecialIngredient == false)
                    {
                        // Normal Check
                        foreach (var ingredient in ingredientStorage.GetRecipe().ingredients)
                        {
                            // If Ingredient(itemToCheck) equal Ingredient in Original Recipe
                            if (item.itemName == ingredient.itemName)
                            {
                                currentCorrectIngredient += 1;
                            }
                        }
                    }
                    else
                    {
                        // Special Check
                        foreach (var ingredient in ingredientStorage.GetRecipe().specialIngredients)
                        {
                            // If Ingredient(itemToCheck) equal Ingredient in Original Recipe
                            if (item.itemName == ingredient.itemName)
                            {
                                currentCorrectIngredient += 1;
                            }
                        }
                    }
                }*/

                currentCorrectIngredient = GetCurrentCorrectIngredient();
            }
            else
            {
                currentCorrectIngredient -= 1;
            }
        }

        public bool IsOneOfIngredient(IngredientObject ingredient)
        {
            var isFound = false;

            if (ingredient.isSpecialIngredient == false)
            {
                for (int i = 0; i < ingredientStorage.GetRecipe().ingredients.Count; i++)
                {
                    if (ingredientStorage.GetRecipe().ingredients[i] == ingredient)
                    {
                        isFound = true;
                    }
                }
            }
            else
            {
                // Special Ingredient
                for (int i = 0; i < ingredientStorage.GetRecipe().specialIngredients.Count; i++)
                {
                    if (ingredientStorage.GetRecipe().specialIngredients[i] == ingredient)
                    {
                        isFound = true;
                    }
                }
            }

            return isFound;
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
                    CreateTempRecipe(itemObject);
                    ingredientStorage.InitializeStorageObject(ingredientStorage.GetRecipe().ingredients.Count + ingredientStorage.GetRecipe().specialIngredients.Count, true);
                    kitchenUI.InitRecipe(ingredientStorage.GetRecipe(), stoveTempRecipe);
                }
            }
            else
            {
                Manager.Instance.notifyManager.CreateNotify("Awww Hot!Hot!", "Food is cooking.");
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

            if (psHandler.IsHoldingItem() == false)
            {
                // Remove Item In Stove
                RemoveAtSelectSlot();

                // Give Trash Bag
                psHandler.JustPutIn(ingredientStorage.GetTrashItem());
            }
            else
            {
                Manager.Instance.notifyManager.CreateNotify("Holding somethings...", "Your are holding something please put it down first.");
            }
        }

        public void RemoveAtSelectSlot()
        {
            var itemToRemove = (IngredientObject)currentSelectSlot.GetItem();
            // Remove Item at stove
            stoveTempRecipe.SubIngredientQuantityByItemName(itemToRemove);

            // Remove Slot
            kitchenUI.UpdateUI();
        }

        public void CloseUI()
        {
            kitchenUI.gameObject.SetActive(false);
            UIPanelHandler.instance.CloseUIPanel();
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
            ClearTempRecipe();
            currentCorrectIngredient = 0;
            ingredientStorage.GetStorageSlot().Clear();
        }
    }
}

