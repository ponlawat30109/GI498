using System;
using System.Collections.Generic;
using _Scripts.InventorySystem;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.ManagerCollection;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace _Scripts.CookingSystem
{
    
    [CreateAssetMenu(fileName = "New Kitchen Storage", menuName = "Inventory System/Kitchen Storage")]
    public class KitchenStorage : ScriptableObject
    {
        public StorageObject.StorageTypeEnum storageType;
        [SerializeField] private List<StorageIngredientSlot> storageSlots = new List<StorageIngredientSlot>();
        [SerializeField] private FoodObject recipeItemSlot; //Recipe
        [SerializeField] private ItemObject trashItemObject;

        private int maxSlot;
        private bool isStorageStackable;
        
        public void InitializeStorageObject(int _maxSlot,bool isStackable)
        {
            maxSlot = _maxSlot;
            isStorageStackable = isStackable;
        }

        public void SetRecipe(FoodObject foodObject)
        {
            recipeItemSlot = foodObject;
        }

        public FoodObject GetRecipe()
        {
            return recipeItemSlot;
        }

        public ItemObject GetTrashItem()
        {
            return trashItemObject;
        }
        
        public bool AddItem(IngredientObject itemToAdd)
        {
            bool successful = false;
        
            if (HasFreeSpace() == true)
            {
                if (isStorageStackable)
                {
                    if (HasIngredient(itemToAdd))
                    {
                        var index = storageSlots.FindIndex(x => x.item.Equals(itemToAdd));
                        storageSlots[index].AddAmount(1);
                        successful = true;
                    }
                    else
                    {
                        if (HasFreeSpace())
                        {
                            storageSlots.Add(new StorageIngredientSlot(itemToAdd, 1));
                            successful = true;
                        }
                    }
                }
                else
                {
                    if (HasFreeSpace())
                    {
                        storageSlots.Add(new StorageIngredientSlot(itemToAdd, 1));
                        successful = true;
                    }
                    else
                    {
                        successful = false;
                    }
                }
            }
            
            return successful;
        }

        public void UpdateStorageSlot()
        {
            for (int i = 0; i < storageSlots.Count; i++)
            {
                if (storageSlots[i].quantity <= 0)
                {
                    storageSlots.RemoveAt(i);
                }
            }
        }

        public bool RemoveItem(IngredientObject itemToRemove)
        {
            var successful = false;
        
            if (HasIngredient(itemToRemove))
            {
                var index = storageSlots.FindIndex(x => x.item.Equals(itemToRemove));
                
                if (storageSlots[index].quantity > 0)
                {
                    storageSlots[index].SubAmount(1);
                    if (storageSlots[index].quantity - 1 <= 0)
                    {
                        UpdateStorageSlot();
                    }
                }
                else
                {
                    UpdateStorageSlot();
                }
                
                successful = true;
            }
            else
            {
                Debug.Log($"Item {itemToRemove.itemName} can not remove (do not have item).");
            }

            return successful;
        }

        public bool HasIngredient(IngredientObject itemToCheck)
        {
            var hasItem = false;
        
            for (int i = 0; i < storageSlots.Count; i++)
            {
                if (storageSlots[i].item == itemToCheck) // If has item
                {
                    hasItem = true;
                    break;
                }
            }

            return hasItem;
        }

        public bool HasFreeSpace()
        {
            var hasFreespace = false;
            
            //Condition

            if (IsUnlimitedSlot())
            {
                
                hasFreespace = true;
            }
            else
            {
                if (GetSlotCount() < maxSlot)
                {
                    hasFreespace = true;
                }
                else
                {
                    hasFreespace = false;
                }
            }
        
            //Debug.Log($"Has Free Space of {this.storageType.ToString()} is : {hasFreespace}");
            return hasFreespace;
        }

        public bool HasAnyItem()
        {
            if (storageSlots.Count < 0)
            {
                // Empty
                return false;
            }
            
            var foundItem = false;

            for (int i = 0; i < storageSlots.Count; i++)
            {
                if (storageSlots[i].item != null)
                {
                    foundItem = true;
                }
            }
            
            return foundItem;
        }

        public bool IsSlotIndexHasItem(int index)
        {
            //Debug.Log($"Looking at index {index}");
            var result = false; //GetItemFromSlotIndex(index) != null;

            if (storageSlots.Count > 0)
            {
                if (storageSlots[index] != null)
                {
                    result = true;
                }
            }
            
            return result;
        }
        
        public bool IsUnlimitedSlot()
        {
            if (maxSlot <= 0)
            {
                return true;
            }

            return false;
        }

        public int GetSlotCount()
        {
            return storageSlots.Count;
        }

        public List<StorageIngredientSlot> GetStorageSlot()
        {
            return storageSlots;
        }
        
        public IngredientObject GetItemFromSlotIndex(int index)
        {
            return storageSlots[index].item;
        }

        public void PutIn(IngredientObject itemOnPlayerHand)
        {
            // TODO: Put Ingredient in to this storageSlots and Take out Item from Player Hand
            // TODO: Must Check this "HasFreeSpace" and Player "Really have Item"
            // Note : Player Hand is
            var psHandler = Manager.Instance.playerManager.PSHandler();

            if (HasFreeSpace() && psHandler.IsHoldingItem())  // Check this storage have free space and player hand is holding somethings.
            {
                if (psHandler.currentHoldItemObject.type == ItemType.Ingredient)
                {
                    AddItem(itemOnPlayerHand);
                    psHandler.JustTakeOut(itemOnPlayerHand);
                }
                else
                {
                    Debug.Log("[KitchenStorage.cs] Player try to add other things not ingredient");
                }
            }
            else
            {
                if (HasFreeSpace() == false)
                {
                    Debug.Log("[KitchenStorage.cs] Storage dont have free space.");
                }

                if (psHandler.IsHoldingItem() == false)
                {
                    Debug.Log("[KitchenStorage.cs] Item on player hand is null.");
                }
            }
        }

        public void TakeOutCookedFood()
        {
            var psHandler = Manager.Instance.playerManager.PSHandler();
            psHandler.JustPutInFood(recipeItemSlot); // Add Food to Player
            ClearStove();
        }

        public void TakeOutRecipe()
        {
            var psHandler = Manager.Instance.playerManager.PSHandler();
            psHandler.JustPutInFood(recipeItemSlot); // Add Recipe to Player
            ClearStove();
        }

        public void ClearStove()
        {
            SetRecipe(null);
            storageSlots.Clear();
        }
    }
    
    [Serializable]
    public class StorageIngredientSlot
    {
        public IngredientObject item;
        public int quantity;

        public StorageIngredientSlot(IngredientObject _item, int _quantity)
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