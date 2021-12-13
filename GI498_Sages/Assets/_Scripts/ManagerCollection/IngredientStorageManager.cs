using System;
using System.Collections.Generic;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using UnityEngine;

namespace _Scripts.ManagerCollection
{
    public class IngredientStorageManager : MonoBehaviour
    {
        [Serializable]
        public struct IngredientRank
        {
            public int rankIndex;
            [SerializeField] private string comment;
            public List<IngredientObject> ingredientList;
        }

        [SerializeField] private StorageManager parent;

        [Space]
        [Header("Ingredient Collection")]
        [SerializeField] private List<IngredientObject> inVegetableStorageIngredientList = new List<IngredientObject>();
        [SerializeField] private List<IngredientObject> inMeatStorageIngredientList = new List<IngredientObject>();
        [SerializeField] private List<IngredientObject> inShelfStorageIngredientList = new List<IngredientObject>();
        [SerializeField] private List<IngredientObject> inShelfStorageSpecialIngredientList = new List<IngredientObject>();

        [SerializeField] private int currentRank;
        [Space]
        [Header("Rank of Ingredient")]
        [SerializeField] private List<IngredientRank> ingredientRankList = new List<IngredientRank>();

        [Space]
        [Space]
        private float _currentCheckTime = 0;
        [SerializeField] private float checkRankInterval = 2;

        private void Start()
        {
            _currentCheckTime = 0;
        }

        private void Update()
        {

            if (_currentCheckTime < checkRankInterval)
            {
                _currentCheckTime += Time.deltaTime;
            }
            else if (_currentCheckTime >= checkRankInterval)
            {
                DefineCurrentRank();
                // AssignIngredientByRank();

                // Reset
                _currentCheckTime = 0;
            }
        }

        public void DefineCurrentRank()
        {
            if (RankManager.Instance != null)
                currentRank = RankManager.Instance.GetCurrentRank();
        }

        public void AssignIngredientByRank()
        {
            if (IsIngredientNull())
            {
                Debug.Log("[IngredientStorageManager.cs] Found Null in Ingredient list.");
                return;
            }

            AssignIngredientOfList();
        }

        public void AssignIngredientOfList()
        {
            // Assign Ingredient to Same Rank, and Same Storage
            // So If Ingredient RankIndex == currentRank
            // - And That Ingredient == Storage to assign (listToProcess)

            /*for (int i = 0; i < ingredientRankList.Count; i++)
            {
                // If Ingredient Rank I == Current Rank ... If That List is the Correct Rank
                if (currentRank > ingredientRankList[i].rankIndex)
                {
                    // Loop Ingredient of That Rank.
                    for (int j = 0; j < ingredientRankList[i].ingredientList.Count; j++)
                    {
                        // Loop Ingredient all of Ingredient of List of Ingredient Different Storage Name
                        for (int k = 0; k < listToProcess.Count; k++)
                        {
                            // If Ingredient J in Ingredient of Rank I == Ingredient Different Storage Name at K
                            if (ingredientRankList[i].ingredientList[j] == listToProcess[k])
                            {
                                // Assign To Storage
                                var s = Manager.Instance.storageManager.GetStorageByName(listToProcessName);
                                var target = s.storage.GetStorageObject();
                                
                                target.AddItem(listToProcess[k]);
                            }
                        }
                    }
                }
            }*/

            // Short variable of Ingredient Collection
            var a = inVegetableStorageIngredientList;
            var aTargetName = StorageManager.StorageCollection.StorageName.Fridge;
            var aTarget = parent.GetStorageByName(aTargetName).GetStorageObject();

            var b = inMeatStorageIngredientList;
            var bTargetName = StorageManager.StorageCollection.StorageName.FridgeMeat;
            var bTarget = parent.GetStorageByName(bTargetName).GetStorageObject();

            var c = inShelfStorageIngredientList;
            var cTargetName = StorageManager.StorageCollection.StorageName.ShelfNormal;
            var cTarget = parent.GetStorageByName(cTargetName).GetStorageObject();

            var d = inShelfStorageSpecialIngredientList;
            var dTargetName = StorageManager.StorageCollection.StorageName.ShelfSpecial;
            var dTarget = parent.GetStorageByName(dTargetName).GetStorageObject();

            // Ingredient Collection
            foreach (var item in a)
            {
                if (IsInRankIngredient(item))
                {
                    AddItemToStorage(aTarget, item);
                }
            }

            foreach (var item in b)
            {
                if (IsInRankIngredient(item))
                {
                    AddItemToStorage(bTarget, item);
                }
            }

            foreach (var item in c)
            {
                if (IsInRankIngredient(item))
                {
                    AddItemToStorage(cTarget, item);
                }
            }

            foreach (var item in d)
            {
                if (IsInRankIngredient(item))
                {
                    AddItemToStorage(dTarget, item);
                }
            }
        }

        public void AddItemToStorage(StorageObject target, ItemObject item)
        {
            target.AddItem(item);
        }

        public bool IsInRankIngredient(IngredientObject itemToCheck)
        {
            var isFound = false;

            for (int i = 0; i < ingredientRankList.Count; i++)
            {
                if (ingredientRankList[i].rankIndex <= currentRank)
                {
                    for (int j = 0; j < ingredientRankList[i].ingredientList.Count; j++)
                    {
                        // If Item To Check == Item In Ingredient Rank i itemList.
                        if (itemToCheck == ingredientRankList[i].ingredientList[j])
                        {
                            isFound = true;
                        }
                    }
                }
                else if (ingredientRankList[i].rankIndex > currentRank)
                {
                    break;
                }
            }

            return isFound;
        }

        public bool IsIngredientNull()
        {
            var a = inVegetableStorageIngredientList;
            var b = inMeatStorageIngredientList;
            var c = inShelfStorageIngredientList;
            var d = inShelfStorageSpecialIngredientList;

            // Shortcut Check
            if (a.Count < 1 || b.Count < 1 ||
                c.Count < 1)// || d.Count < 1)
            {
                return true;
            }

            // Check Session
            var foundCount = 0;

            // Check A
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] == null)
                {
                    foundCount++;
                }
            }

            // Check B
            for (int i = 0; i < b.Count; i++)
            {
                if (b[i] == null)
                {
                    foundCount++;
                }
            }

            // Check C
            for (int i = 0; i < c.Count; i++)
            {
                if (c[i] == null)
                {
                    foundCount++;
                }
            }

            // Check D
            for (int i = 0; i < d.Count; i++)
            {
                if (d[i] == null)
                {
                    foundCount++;
                }
            }

            // Define Return
            var isFoundNull = false;

            if (foundCount > 0)
            {
                isFoundNull = true;
            }
            else
            {
                isFoundNull = false;
            }

            return isFoundNull;
        }

        private void OnApplicationQuit()
        {

            var aTargetName = StorageManager.StorageCollection.StorageName.Fridge;
            var aTarget = parent.GetStorageByName(aTargetName).GetStorageObject();

            var bTargetName = StorageManager.StorageCollection.StorageName.FridgeMeat;
            var bTarget = parent.GetStorageByName(bTargetName).GetStorageObject();

            var cTargetName = StorageManager.StorageCollection.StorageName.ShelfNormal;
            var cTarget = parent.GetStorageByName(cTargetName).GetStorageObject();

            var dTargetName = StorageManager.StorageCollection.StorageName.ShelfSpecial;
            var dTarget = parent.GetStorageByName(dTargetName).GetStorageObject();

            aTarget.GetStorageSlot().Clear();
            bTarget.GetStorageSlot().Clear();
            cTarget.GetStorageSlot().Clear();
            dTarget.GetStorageSlot().Clear();
        }
    }
}
