using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.InventorySystem;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.InventorySystem.UI;
using UnityEngine;

namespace _Scripts.ManagerCollection
{
    public class StorageManager : MonoBehaviour
    {

        [Serializable]
        public struct StorageCollection
        {
            public enum StorageName
            {
                Default,
                Player,
                Fridge,
                FridgeMeat,
                ShelfNormal,
                ShelfSpecial
            }
            
            public StorageObject.StorageTypeEnum type;
            public Storage storage;
            public StorageName storageName;
        }
        
        [Header("Ingredient Manager")]
        [SerializeField] public IngredientStorageManager ingredientStorageManager;
        
        [Space]
        [Header("Storage Collection")]
        public List<StorageCollection> storageCollections;
        public List<MiniStorage> miniStorageCollections;

        [Space]
        /* For Backdoor only
            - use for clear things and debug...
         */
        [Header("All Ingredient and Recipe")]
        [SerializeField] private List<IngredientObject> ingredientCollections;
        [SerializeField] private List<IngredientObject> specialIngredientCollections;
        [SerializeField] private List<RecipeSlot> recipeCollections;

        
        ////////////////////////////////////////////////////////////////////////////////////////////

        private void Start()
        {
            ingredientStorageManager.DefineCurrentRank();
            ingredientStorageManager.AssignIngredientByRank();
            
            foreach (var storage in storageCollections)
            {
                if (storage.type == StorageObject.StorageTypeEnum.Storage)
                {
                    for (int i = 0; i < storage.storage.GetStorageObject().GetSlotCount(); i++)
                    {
                        if (storage.storage.GetStorageObject().GetStorageSlot()[i] != null)
                        {
                            storage.storage.GetStorageObject().GetStorageSlot()[i].quantity = 999;
                        }
                    }
                }
            }

            for (int i = 0; i < recipeCollections.Count; i++)
            {
                if (recipeCollections[i] != null)
                {
                    recipeCollections[i].quantity = 999;
                }
            }
        }
        
        /////////////////////////////////////////////////////////////////////////////////////////////

        public Storage GetStorageByType(StorageObject.StorageTypeEnum type)
        {
            var obj = gameObject.GetComponent<Storage>();
            
            foreach (var storage in storageCollections)
            {
                if (storage.type == type)
                {
                    obj = storage.storage;
                }
            }

            return obj;
        }
        
        public Storage GetStorageByName(StorageCollection.StorageName toGetName)
        {
            var obj = gameObject.GetComponent<Storage>();
            
            foreach (var storage in storageCollections)
            {
                if (storage.storageName == toGetName)
                {
                    obj = storage.storage;
                }
            }

            return obj;
        }

        public FoodObject TakeRecipeByIndex(int index)
        {
            recipeCollections[index].quantity -= 1;
            return recipeCollections[index].item;
        }

        public void ClearIngredientQuantity()
        {
            foreach (var ingredient in ingredientCollections)
            {
                ingredient.quantity = 0;
            }
            
            foreach (var ingredient in specialIngredientCollections)
            {
                ingredient.quantity = 0;
            }
        }
        
        private void OnApplicationQuit()
        {
            foreach (var recipe in recipeCollections)
            {
                if (recipe.item.isCooked)
                {
                    recipe.item.ResetFoodObject();
                }
            }

            ClearIngredientQuantity();
        }
    }
    
    [Serializable]
    public class RecipeSlot
    {
        public FoodObject item;
        public int quantity;

        public RecipeSlot(FoodObject _item, int _quantity)
        {
            item = _item;
            quantity = _quantity;
        }

        public void AddAmount(int value)
        {
            quantity += value;
        }

        public void SubAmount(int value)
        {
            if (quantity - value <= 0)
            {
                quantity = 0; // Or Remove slot
                return;
            }
            quantity -= value;
        }
    }
}