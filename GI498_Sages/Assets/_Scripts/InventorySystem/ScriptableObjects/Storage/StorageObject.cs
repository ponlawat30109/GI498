using System;
using System.Collections;
using System.Collections.Generic;
using _Scriptable_Object.Items.Scripts;
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
            Storage,
            Stove,
            StorageNoUI,
            Other
        }
        
        public StorageTypeEnum storageType;
        [SerializeField] private List<StorageSlot> storageSlots = new List<StorageSlot>();
    
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
        
            if (HasFreeSpace() == true)
            {
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
                else
                {
                    if (HasFreeSpace())
                    {
                        storageSlots.Add(new StorageSlot(itemToAdd, 1));
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

        public bool RemoveItem(ItemObject itemToRemove)
        {
            Debug.Log($"Remove {itemToRemove.itemName} from player");
            
            var successful = false;
        
            if (HasItem(itemToRemove))
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

        public List<StorageSlot> GetStorageSlot()
        {
            return storageSlots;
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

                if (psHandler.IsHoldingItem() == false && psHandler.storage.GetStorageObject().HasFreeSpace())
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
                    Debug.Log($"[StorageObject.cs][case == Player] Take {item.itemName} from {a.storageType} to {b.storageType}.");
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
                    Debug.Log($"[StorageObject.cs][case == Else] Take {item.itemName} from {a.storageType} to {b.storageType}.");
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