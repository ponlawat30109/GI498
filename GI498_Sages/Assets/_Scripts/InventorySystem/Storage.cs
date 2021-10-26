using System;
using _Scripts.InteractSystem.Interface;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.InventorySystem.UI;
using UnityEngine;

namespace _Scripts.InventorySystem
{
    public class Storage : MonoBehaviour,IInteractableObject
    {
        [SerializeField] private StorageObject storageObject;
        [SerializeField] private StorageUI storageUI;
        [SerializeField] private ItemSlotUI currentSelectSlot;

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
        public void SetSelectSlot(ItemSlotUI itemFridgeSlotUI)
        {
            currentSelectSlot = itemFridgeSlotUI;
        }

        public void DeSelectSlot()
        {
            SetSelectSlot(null);
            Debug.Log("Deselect Slot");
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