using System;
using System.Collections.Generic;
using _Scripts.ManagerCollection;
using UnityEngine;

namespace _Scripts.InventorySystem.UI
{
    public class StorageUI : MonoBehaviour
    {
        [SerializeField] private Storage parent;
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform storageSlot;
        [SerializeField] private Transform storageSlotInformation;

        public List<ItemSlotUI> slotList = new List<ItemSlotUI>();
    
        private float currentTime;
        private float refreshInterval = 1;
    
        void Start()
        {
            CreateUI();
        }
        
        void Update()
        {
            if (currentTime < refreshInterval)
            {
                if (currentTime >= refreshInterval)
                {
                    UpdateUI();
                    currentTime = 0;
                }
                
                currentTime++;
            }
        }
        
        public void UpdateUI()
        {
            CreateUI();
        }

        public Storage GetParent()
        {
            return parent;
        }

        public Transform GetStorageSlotTransform()
        {
            return storageSlot;
        }

        public void CreateUI()
        {
            var slot = parent.GetInventory().storage;
        
            for (int i = 0; i < slot.Count; i++)
            {
                var newSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
                var componentSlot = newSlot.gameObject.GetComponent<ItemSlotUI>();
                componentSlot.InitializeItem(slot[i].item,this,storageSlotInformation);
                slotList.Add(componentSlot);
            }
        }
    }
}
