using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.InventorySystem.ScriptableObjects.Storage
{
    [CreateAssetMenu(fileName = "New Container", menuName = "Inventory System/Container")]
    public class StorageObject : ScriptableObject
    {
        [Serializable] enum StorageTypeEnum
        {
            Player,
            Fridge,
            CookingPot
        }
        
        [SerializeField] private StorageTypeEnum storageType;
        public List<ContainerSlot> storage = new List<ContainerSlot>();
    
        [SerializeField] private int maxSlot;
        [SerializeField] private bool isStorageStackable;


        public void InitializeContainerObject(int _maxSlot,bool isStackable)
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
                    var index = storage.FindIndex(x => x.item.Equals(itemToAdd));
                    storage[index].AddAmount(1);
                    successful = true;
                }
                else
                {
                    if (HasFreeSpace())
                    {
                        storage.Add(new ContainerSlot(itemToAdd, 1));
                        successful = true;
                    }
                }
            }

            return successful;
        }

        public bool RemoveItem(ItemObject itemToRemove)
        {
            var successful = false;
        
            if (HasItem(itemToRemove))
            {
                var index = storage.FindIndex(x => x.item.Equals(itemToRemove));
                storage[index].SubAmount(1);
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
        
            for (int i = 0; i < storage.Count; i++)
            {
                if (storage[i].item == itemToCheck) // If has item
                {
                    hasItem = true;
                    break;
                }
            }

            return hasItem;
        }

        public bool HasFreeSpace()
        {
            //Condition
            if (IsLimitedSlot())
            {
                if (GetSlotCount() < maxSlot)
                {
                    return true; //Can add.
                }
                else
                {
                    return false;
                }
            }
        
            return true;
        }
    
        public bool IsLimitedSlot()
        {
            if (maxSlot <= 0)
            {
                return true;
            }

            return false;
        }

        public int GetSlotCount()
        {
            return storage.Count;
        }
    
    }

    [Serializable]
    public class ContainerSlot
    {
        public ItemObject item;
        public int quantity;

        public ContainerSlot(ItemObject _item, int _quantity)
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