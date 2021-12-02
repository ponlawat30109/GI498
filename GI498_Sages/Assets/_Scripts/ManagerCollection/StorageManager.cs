using System;
using System.Collections.Generic;
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
            public StorageObject.StorageTypeEnum type;
            public Storage storage;
        }
        
        public List<StorageCollection> storageCollections;

        public List<MiniStorage> miniStorageCollections;

        public List<RecipeSlot> recipeCollections;

        
        ////////////////////////////////////////////////////////////////////////////////////////////

        private void Start()
        {
            var fridgeSlots = GetStorageByType(StorageObject.StorageTypeEnum.Storage).GetStorageObject().GetStorageSlot();
            for (int i = 0; i < fridgeSlots.Count; i++)
            {
                if (fridgeSlots[i] != null)
                {
                    fridgeSlots[i].quantity = 999;
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

        public FoodObject TakeRecipeByIndex(int index)
        {
            recipeCollections[index].quantity -= 1;
            return recipeCollections[index].item;
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