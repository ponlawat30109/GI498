using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.InventorySystem.Interface;
using _Scripts.ManagerCollection;
using UnityEngine;

namespace _Scripts.InventorySystem.ScriptableObjects.Storage
{
    [CreateAssetMenu(fileName = "New Storage", menuName = "Inventory System/Storage")]
    public class StorageObject : ScriptableObject,ITakeable
    {
        [Serializable] public enum StorageTypeEnum
        {
            Player,
            Fridge,
            CookingPot
        }
        
        public StorageTypeEnum storageType;
        public List<StorageSlot> storageSlots = new List<StorageSlot>();
    
        private int maxSlot;
        private bool isStorageStackable;


        public void InitializeStorageObject(int _maxSlot,bool isStackable)
        {
            maxSlot = _maxSlot;
            isStorageStackable = isStackable;
        }
    
        public bool AddItem(ItemObject itemToAdd)
        {
            bool successful = false;
        
            if (!HasFreeSpace())
            {
                return false;
            }

            if (isStorageStackable)
            {
                if (HasItem(itemToAdd))
                {
                    var index = storageSlots.FindIndex(x => x.item.Equals(itemToAdd));
                    storageSlots[index].AddAmount(1);
                    successful = true;
                }
                else
                {
                    if (HasFreeSpace())
                    {
                        storageSlots.Add(new StorageSlot(itemToAdd, 1));
                        successful = true;
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

        public bool RemoveItem(ItemObject itemToRemove)
        {
            var successful = false;
        
            if (HasItem(itemToRemove))
            {
                var index = storageSlots.FindIndex(x => x.item.Equals(itemToRemove));
                
                if (storageSlots[index].quantity > 0)
                {
                    storageSlots[index].SubAmount(1);
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
        
        public bool HasItem(ItemObject itemToCheck)
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
        
            return hasFreespace;
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

        public ItemObject GetItemFromSlotIndex(int index)
        {
            return storageSlots[index].item;
        }

        public bool PutIn(StorageObject a, StorageObject b, ItemObject item)
        {
            var isSuccess = false;

            if (a.storageType == StorageTypeEnum.Player) // Case Put Item into Player Hand
            {
                var psHandler = Manager.Instance.playerManager.PSHandler();

                if (psHandler.IsHoldingItem() == false && psHandler.playerInventory.GetInventory().HasFreeSpace())
                {
                    psHandler.PutIn(a, b, item);
                    Debug.Log($"[StorageObject.cs] Put {item.itemName} from {b.storageType} to {a.storageType}.");
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            else
            {
                if (a.HasFreeSpace() && b.HasItem(item))
                {
                    a.AddItem(item);
                    b.RemoveItem(item);
                    isSuccess = true;
                }
                else
                {
                    var msg = "";
                    
                    if (a.HasFreeSpace() == false)
                    {
                        msg += $"/{a} Do not have free space./";
                    }

                    if (b.HasItem(item) == false)
                    {
                        msg += $"/{b} Do not have {item.itemName}./";
                    }
                    
                    Debug.Log(msg);
                    isSuccess = false;
                }
            }
            
            return isSuccess;
        }

        public bool TakeOut(StorageObject a, StorageObject b, ItemObject item)
        {
            var isSuccess = false;

            if (a.storageType == StorageTypeEnum.Player) // Case Take from Player Hand
            {
                var psHandler = Manager.Instance.playerManager.PSHandler();

                if (a.HasItem(item) && b.HasFreeSpace())
                {
                    psHandler.TakeOut(a, b, item);
                    Debug.Log($"[StorageObject.cs] Take {item.itemName} from {a.storageType} to {b.storageType}.");
                    isSuccess = true;
                }
                else
                {
                    var msg = "";

                    if (a.HasItem(item) == false)
                    {
                        msg += $"/{a} Do not have {item.itemName}./";
                    }
                    
                    if (b.HasFreeSpace() == false)
                    {
                        msg += $"/{b} Do not have free space./";
                    }

                    Debug.Log(msg);
                    isSuccess = false;
                }
            }
            else
            {
                if (a.HasItem(item) == true && b.HasFreeSpace())
                {
                    a.RemoveItem(item);
                    b.AddItem(item);
                    Debug.Log($"[StorageObject.cs] Take {item.itemName} from {a.storageType} to {b.storageType}.");
                    isSuccess = true;
                }
                else
                {
                    var msg = "";

                    if (a.HasItem(item) == false)
                    {
                        msg += $"/{a} Do not have {item.itemName}./";
                    }
                    
                    if (b.HasFreeSpace() == false)
                    {
                        msg += $"/{b.storageType} Type Storage,({b.GetSlotCount()}/{b.maxSlot}) Do not have free space./";
                    }

                    Debug.Log(msg);
                    isSuccess = false;
                }
            }

            return isSuccess;
        }
    }

    [Serializable]
    public class StorageSlot
    {
        public ItemObject item;
        public int quantity;

        public StorageSlot(ItemObject _item, int _quantity)
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