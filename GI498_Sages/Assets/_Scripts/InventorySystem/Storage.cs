using System;
using _Scripts.InteractSystem.Interface;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.InventorySystem.UI;
using UnityEngine;

namespace _Scripts.InventorySystem
{
    public class Storage : MonoBehaviour,IInteractableObject,ITakeAble
    {
        [SerializeField] private StorageObject inventory;
        [SerializeField] private StorageUI storageUI;
        [SerializeField] private ItemSlotUI currentSelectSlot;

        [SerializeField] private bool isSlotUISelectable;
        
        public event Action OnInteracted;

        private void Start()
        {
            storageUI.gameObject.SetActive(false);
        }

        public StorageObject GetInventory()
        {
            return inventory;
        }

        public void Interacted()
        {
            storageUI.gameObject.SetActive(true);
        }

        public void CloseUI()
        {
            storageUI.gameObject.SetActive(false);
        }

        public bool IsSlotUISelectable
        {
            get => isSlotUISelectable;
            set => isSlotUISelectable = value;
        }

        //Take Into other Storage
        public void TakeIn(StorageObject otherStorage, StorageObject playerStorage, ItemObject takeInItem)
        {
            if (playerStorage.HasItem(takeInItem) && otherStorage.HasFreeSpace()) 
            {
                otherStorage.AddItem(takeInItem);
                playerStorage.RemoveItem(takeInItem);
            }
            else
            {
                //Debug session
                var msg = "";
                if (!playerStorage.HasItem(takeInItem))
                {
                    msg += $"Don't have {takeInItem} in Player Container. ";
                }

                if (!otherStorage.HasFreeSpace())
                {
                    msg += "Other Container don't have free space";
                }
                
                Debug.Log(msg);
            }
        }

        //Take Out from other Storage
        public void TakeOut(StorageObject playerStorage, StorageObject oldStorage, ItemObject takeOutItem)
        {
            // If able to Take Item from Old and able to Add New Item to player.
            if (oldStorage.HasItem(takeOutItem) && playerStorage.HasFreeSpace()) 
            {
                playerStorage.AddItem(takeOutItem);
                oldStorage.RemoveItem(takeOutItem);
            }
            else
            {
                //Debug session
                var msg = "";
                if (!oldStorage.HasItem(takeOutItem))
                {
                    msg += $"Don't have {takeOutItem} in Old Container. ";
                }

                if (!playerStorage.HasFreeSpace())
                {
                    msg += "Player don't have free space";
                }
                
                Debug.Log(msg);
            }
        }

        
        // Slot UI
        public void SetSelectSlot(ItemSlotUI itemSlotUI)
        {
            currentSelectSlot = itemSlotUI;
        }

        public ItemSlotUI GetSelectSlot()
        {
            return currentSelectSlot;
        }

        // Player out of collider range
        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                storageUI.gameObject.SetActive(false);
            }
        }
    }
}