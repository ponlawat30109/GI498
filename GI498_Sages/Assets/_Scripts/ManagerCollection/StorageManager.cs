using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.InventorySystem;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.InventorySystem.UI;
using _Scripts.NPCSctipts;
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

        [Header("Recipe Manager")]
        [SerializeField] public RecipeStorageManager recipeStorageManager;
        
        [Space]
        [Header("Storage Collection")]
        public List<StorageCollection> storageCollections;

        [Space]
        [Space]
        /* For Backdoor only
            - use for clear things and debug...
         */
        [Header("All Ingredient and Recipe (For Clear Quantity)")]
        [SerializeField] private List<IngredientObject> ingredientCollections;
        [SerializeField] private List<IngredientObject> specialIngredientCollections;


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

        public void ClearRecipeCollection()
        {
            foreach (var recipe in recipeStorageManager.GetRecipeCollectionByType(NpcInformation.NpcPatientType.Normal).recipeList)
            {
                if (recipe.isCooked)
                {
                    recipe.ResetFoodObject();
                }
            }
            
            foreach (var recipe in recipeStorageManager.GetRecipeCollectionByType(NpcInformation.NpcPatientType.KidneyDiseaseBF).recipeList)
            {
                if (recipe.isCooked)
                {
                    recipe.ResetFoodObject();
                }
            }
            
            foreach (var recipe in recipeStorageManager.GetRecipeCollectionByType(NpcInformation.NpcPatientType.KidneyDiseaseAF).recipeList)
            {
                if (recipe.isCooked)
                {
                    recipe.ResetFoodObject();
                }
            }
        }
        
        private void OnApplicationQuit()
        {
            ClearRecipeCollection();
            ClearIngredientQuantity();
        }
    }
}