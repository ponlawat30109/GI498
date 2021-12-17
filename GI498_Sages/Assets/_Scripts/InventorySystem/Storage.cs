using System;
using System.Collections.Generic;
using _Scripts.InteractSystem.Interface;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.InventorySystem.UI;
using UnityEngine;

namespace _Scripts.InventorySystem
{
    public class Storage : MonoBehaviour,IInteractableObject
    {
        [SerializeField] private StorageObject storageObject; //ScriptableObject
        [SerializeField] private StorageUI storageUI; //UI_Set in Game for each storage
        [SerializeField] public List<ItemSlotUI> currentSelectSlotList = new List<ItemSlotUI>(); //Auto set when player select slot

        [SerializeField] private int maxSlot;
        [SerializeField] private bool isStackable;
        [SerializeField] private bool isSlotUISelectable;

        public event Action OnInteracted;

        private void Start()
        {
            storageUI.gameObject.SetActive(false);
            storageObject.InitializeStorageObject(maxSlot, isStackable);
        }

        public StorageObject GetStorageObject()
        {
            return storageObject;
        }

        public StorageUI GetStorageUI()
        {
            return storageUI;
        }

        public void Interacted()
        {
            storageUI.JustOpen();
            storageUI.gameObject.SetActive(true);
        }

        public void CloseUI()
        {
            storageUI.gameObject.SetActive(false);
        }

        public bool IsUIOpen()
        {
            return storageUI.gameObject.activeSelf;
        }
        
        public bool IsSlotUISelectable
        {
            get => isSlotUISelectable;
            set => isSlotUISelectable = value;
        }

        //Put Into Storage
        public void PutIn(StorageObject a,StorageObject b, ItemObject item)
        {
            storageObject.PutIn(a, b, item);
            Debug.Log($"[Storage.cs] Put {item.itemName} from {b.storageType} to {a.storageType}.");
        }

        //Take Out to other Storage
        public void TakeOut(StorageObject a, StorageObject b, ItemObject item)
        {
            storageObject.TakeOut(a, b, item);
            Debug.Log($"[Storage.cs] Take {item.itemName} from {a.storageType} to {b.storageType}.");
        }

        
        // Slot UI
        public void AddSelectSlot(ItemSlotUI slotToSelect)
        {
            currentSelectSlotList.Add(slotToSelect);
        }

        public void DeSelectSlot(ItemSlotUI slotToDeSelect)
        {
            for (int i = 0; i < currentSelectSlotList.Count; i++)
            {
                if (currentSelectSlotList[i] != null)
                {
                    if (currentSelectSlotList[i] == slotToDeSelect)
                    {
                        currentSelectSlotList.RemoveAt(i);
                        Debug.Log("Deselect Slot");
                    }
                }
            }
            
        }

        public List<ItemSlotUI> GetSelectSlotList()
        {
            return currentSelectSlotList;
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