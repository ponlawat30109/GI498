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

            // Check Trash
            /*if (_currentIngredientRemoveCount >= maxIngredientRemoveable && _isTrashFull == false)
            {
                // Kitchen Trash full need to throw trash...
                Debug.Log($"Kitchen trash is full...");
                _isTrashFull = true;
            }*/
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
                return;
            }
            
            // Check UI
            if (stoveTempRecipe.isCooked)
            {
                // Show Finish Cooked UI
                stoveStatusUI.SetCurrentStatus(StoveStatusUI.StatusEnum.Finish);
            }
            else if (IsCanCook() == false) // If current Ingredient < need ingredient
            {
                // Show Waiting UI
                stoveStatusUI.SetCurrentStatus(StoveStatusUI.StatusEnum.Wait);
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
            /*var count = 0;

            foreach (var slot in ingredientStorage.GetRecipe().ingredients)
            {
                if (slot.quantity > 0)
                {
                    count += slot.quantity;
                }
            }
            
            foreach (var slot in ingredientStorage.GetRecipe().specialIngredients)
            {
                if (slot.quantity > 0)
                {
                    count += slot.quantity;
                }
            }*/
            
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

            /*// Normal
            // Loop All Temp Ingredient
            foreach (var ingredient in stoveTempRecipe.ingredients)
            {
                var recipeIng = ingredientStorage.GetRecipe().ingredients;
                // Loop All Recipe Ingredient
                for (int i = 0; i < recipeIng.Count; i++)
                {
                    // If the same Ingredient
                    if (recipeIng[i] == ingredient)
                    {
                        // If the same quantity.
                        if (recipeIng[i].quantity == ingredient.quantity)
                        {
                            correctCount += 1;
                        }
                    }
                }
            }
            // Special
            foreach (var ingredient in stoveTempRecipe.specialIngredients)
            {
                var recipeIng = ingredientStorage.GetRecipe().specialIngredients;
                for (int i = 0; i < recipeIng.Count; i++)
                {
                    // If the same Ingredient
                    if (recipeIng[i] == ingredient)
                    {
                        // If the same quantity.
                        if (recipeIng[i].quantity == ingredient.quantity)
                        {
                            correctCount += 1;
                        }
                    }
                }
            }*/

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
            
            /*for (int i = 0; i < totalNeed; i++)
            {
                
            }*/
            
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
            
            var newTempRecipe = original.CreateInstance(itemName,itemIcon,prefab,cookedPrefab,normalIngredient,speicalIngredient,cookingTime);

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

            // Comment Code Below locate to KitchenStorage Take out case Recipe is Cooked
            //var psHandler = Manager.Instance.playerManager.PSHandler();
            //psHandler.JustPutInFood(recipeItem);

        }

        private bool IsCanCook()
        {

            bool result = false;

            /*var totalQuantity = 0;

            // Note : Check from Original not temp
            foreach (var ingredient in ingredientStorage.GetRecipe().ingredients)
            {
                if (ingredient != null)
                {
                    totalQuantity += ingredient.quantity;
                }
            }
            
            // Special Ingredient
            foreach (var ingredient in ingredientStorage.GetRecipe().specialIngredients)
            {
                if (ingredient != null)
                {
                    totalQuantity += ingredient.quantity;
                }
            }

            // If currentCorrect ingredient >= total need quantity
            // or currentCorrect >= All of ingredients need
            if (currentCorrectIngredient >= totalQuantity)/* || currentCorrectIngredient >=
                ingredientStorage.GetRecipe().ingredients.Count + // Normal
                ingredientStorage.GetRecipe().specialIngredients.Count) // Special#1#
            {
                result = true;
            }*/

            if (GetCurrentCorrectIngredient() >= GetRecipeTotalIngredientQuantity())
            {
                result = true;
                //Debug.Log($"{GetCurrentCorrectIngredient()} / {GetRecipeTotalIngredientQuantity()} To be can process.");
            }
            
            return result;
        }

        public void CheckWhenIngredientAdd(ItemObject itemToCheck)
        {
            var item = (IngredientObject) itemToCheck;
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
                    ingredientStorage.InitializeStorageObject(ingredientStorage.GetRecipe().ingredients.Count+ingredientStorage.GetRecipe().specialIngredients.Count,true);
                    kitchenUI.InitRecipe(ingredientStorage.GetRecipe(), stoveTempRecipe);
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
            
            if (psHandler.IsHoldingItem() == false)
            {
                // Remove Item In Stove
                RemoveAtSelectSlot();
                
                // Give Trash Bag
                psHandler.JustPutIn(ingredientStorage.GetTrashItem());
            }
            else
            {
                Manager.Instance.notifyManager.CreateNotify("Holding somethings...","Your are holding something please put it down first.");
            }
        }
        
        public void RemoveAtSelectSlot()
        {
            var itemToRemove = (IngredientObject) currentSelectSlot.GetItem();
            // Remove Item at stove
            stoveTempRecipe.SubIngredientQuantityByItemName(itemToRemove);
            
            // Remove Slot
            kitchenUI.UpdateUI();
        }

        /*public bool IsTrashFull()
        {
            //return _isTrashFull;
        }*/
        
        /*public void AddTrashCount()
        {
            //_currentIngredientRemoveCount += 1;
        }*/
        
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

/*
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
        [SerializeField] private int currentCorrectIngredient;
        
        //[Header("Kitchen Trash")]
        private bool _isWrongIngredientFound;
        //private int _currentIngredientRemoveCount = 0;
        //private int maxIngredientRemoveable = 4;

        private void Start()
        {
            isCanCook = false;
            isCooking = false;
            //_isTrashFull = false;
            //_currentIngredientRemoveCount = 0;
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
            else
            {
                isCanCook = false;
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
            /*if (_currentIngredientRemoveCount >= maxIngredientRemoveable && _isTrashFull == false)
            {
                // Kitchen Trash full need to throw trash...
                Debug.Log($"Kitchen trash is full...");
                _isTrashFull = true;
            }#1#
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
            #1#
            
            /////////////////////////////////////////////////////////////////////////////////////////////////

            // 1 ) If Recipe Inside
            /*
                - เปิดหน้าต่างไม่ว่ายังไงก็ตาม
                - ถ้ามือไม่ว่างหรือถือของจะเปิด KitchenUI
            #1#
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
                            currentCorrectIngredient = 0;
                            ingredientStorage.GetStorageSlot().Clear();
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
            #1#
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
            isCanCook = false;
            currentStoveTime = 0;

            // Comment Code Below locate to KitchenStorage Take out case Recipe is Cooked
            //var psHandler = Manager.Instance.playerManager.PSHandler();
            //psHandler.JustPutInFood(recipeItem);

        }

        private bool IsCanCook()
        {
            //bool result = _currentCorrectIngredient == ingredientStorage.GetRecipe().ingredients.Count;
            
            bool result = false;

            var totalQuantity = 0;

            foreach (var ingredient in ingredientStorage.GetRecipe().ingredients)
            {
                if (ingredient != null)
                {
                    totalQuantity += ingredient.quantity;
                }
            }
            
            // Special Ingredient
            foreach (var ingredient in ingredientStorage.GetRecipe().specialIngredients)
            {
                if (ingredient != null)
                {
                    totalQuantity += ingredient.quantity;
                }
            }

            if (currentCorrectIngredient >= totalQuantity || currentCorrectIngredient >=
                ingredientStorage.GetRecipe().ingredients.Count + // Normal
                ingredientStorage.GetRecipe().specialIngredients.Count) // Special
            {
                result = true;
            }

            return result;
        }

        public void CheckWhenIngredientAdd(ItemObject itemToCheck)
        {
            if (IsOneOfIngredient((IngredientObject) itemToCheck))
            {
                for (int i = 0; i < ingredientStorage.GetSlotCount(); i++)
                {
                    if (ingredientStorage.GetStorageSlot()[i].item.itemName != itemToCheck.itemName)
                    {
                        currentCorrectIngredient += 1;
                    }
                }
            }
            else
            {
                currentCorrectIngredient -= 1;
            }
        }

        public bool IsOneOfIngredient(IngredientObject ingredient)
        {
            var isFound = false;

            for (int i = 0; i < ingredientStorage.GetRecipe().ingredients.Count; i++)
            {
                if (ingredientStorage.GetRecipe().ingredients[i] == ingredient)
                {
                    isFound = true;
                }
            }
            
            // Special Ingredient
            for (int i = 0; i < ingredientStorage.GetRecipe().specialIngredients.Count; i++)
            {
                if (ingredientStorage.GetRecipe().specialIngredients[i] == ingredient)
                {
                    isFound = true;
                }
            }
            
            /*if (ingredientStorage.GetRecipe().ingredients.Contains(ingredient))
            {
                return true;
            }#1#

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
                    ingredientStorage.InitializeStorageObject(ingredientStorage.GetRecipe().ingredients.Count+ingredientStorage.GetRecipe().specialIngredients.Count,true);
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
            
            if (psHandler.IsHoldingItem() == false)
            {
                psHandler.JustPutIn(ingredientStorage.GetTrashItem());
                ingredientStorage.GetStorageSlot().Clear();
                kitchenUI.ClearSlotList();
                
                //ingredientStorage.ClearStove();
                //_currentIngredientRemoveCount = 0;
                //_isTrashFull = false;
            }
            else
            {
                Manager.Instance.notifyManager.CreateNotify("Holding somethings...","Your are holding something please put it down first.");
            }
        }
        
        public void RemoveAtSelectSlot()
        {
            var itemToRemove = (IngredientObject) currentSelectSlot.GetItem();
            // Remove Item
            ingredientStorage.RemoveItem(itemToRemove);
            // Trash Count
            //AddTrashCount();
            // Remove Slot
            kitchenUI.UpdateUI();
        }

        /*public bool IsTrashFull()
        {
            //return _isTrashFull;
        }#1#
        
        /*public void AddTrashCount()
        {
            //_currentIngredientRemoveCount += 1;
        }#1#
        
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
            currentCorrectIngredient = 0;
            ingredientStorage.GetStorageSlot().Clear();
        }
    }
}
*/
