using System;
using _Scripts.Interact_System.Interface;
using _Scripts.InteractSystem.Interface;
using UnityEngine;

namespace _Scripts.InventorySystem
{
    public class Container : MonoBehaviour,IInteractableObject,ITakeAble
    {
        [SerializeField] private ContainerObject inventory;
        [SerializeField] private GameObject containerUI;
        [SerializeField] private ItemSlotUI currentSelectSlot;
        
        public event Action OnInteracted;

        private void Start()
        {
            containerUI.gameObject.SetActive(false);
        }

        public ContainerObject GetInventory()
        {
            return inventory;
        }

        public void Interacted()
        {
            containerUI.gameObject.SetActive(true);
        }

        public void CloseUI()
        {
            containerUI.gameObject.SetActive(false);
        }
        
        //Take Into other Container
        public void TakeIn(ContainerObject otherContainer, ContainerObject playerContainer, ItemObject takeInItem)
        {
            if (playerContainer.HasItem(takeInItem) && otherContainer.HasFreeSpace()) 
            {
                otherContainer.AddItem(takeInItem);
                playerContainer.RemoveItem(takeInItem);
            }
            else
            {
                //Debug session
                var msg = "";
                if (!playerContainer.HasItem(takeInItem))
                {
                    msg += $"Don't have {takeInItem} in Player Container. ";
                }

                if (!otherContainer.HasFreeSpace())
                {
                    msg += "Other Container don't have free space";
                }
                
                Debug.Log(msg);
            }
        }

        //Take Out from other Container
        public void TakeOut(ContainerObject playerContainer, ContainerObject oldContainer, ItemObject takeOutItem)
        {
            // If able to Take Item from Old and able to Add New Item to player.
            if (oldContainer.HasItem(takeOutItem) && playerContainer.HasFreeSpace()) 
            {
                playerContainer.AddItem(takeOutItem);
                oldContainer.RemoveItem(takeOutItem);
            }
            else
            {
                //Debug session
                var msg = "";
                if (!oldContainer.HasItem(takeOutItem))
                {
                    msg += $"Don't have {takeOutItem} in Old Container. ";
                }

                if (!playerContainer.HasFreeSpace())
                {
                    msg += "Player don't have free space";
                }
                
                Debug.Log(msg);
            }
        }

        public void SetSelectSlot(ItemSlotUI itemSlotUI)
        {
            currentSelectSlot = itemSlotUI;
        }

        public ItemSlotUI GetSelectSlot()
        {
            return currentSelectSlot;
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                containerUI.gameObject.SetActive(false);
            }
        }
    }
}