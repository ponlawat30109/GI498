using System;
using System.Collections.Generic;
using _Scripts.InventorySystem.Interface;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.ManagerCollection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _Scripts.InventorySystem.UI
{
    public class StorageUI : MonoBehaviour
    {
        [SerializeField] private Storage parent;
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform storageSlot;
        [SerializeField] private Transform storageSlotInformation;

        [Header("ObjInformation")]
        [SerializeField] private TMP_Text infoNameText;
        [SerializeField] private TMP_Text infoDetailText;
        [SerializeField] private Image infoImage;

        private ItemSlotUI currentSelectSlot;
        public ItemSlotUI CurrentSelectSlot { get => currentSelectSlot; }

        public List<ItemSlotUI> slotList = new List<ItemSlotUI>();

        private float currentTime = 0;
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

        public void ChangeObjInfo(string itemName, string detail, Sprite sprite)
        {
            infoNameText.text = itemName;
            infoDetailText.text = detail;
            infoImage.sprite = sprite;
        }
        public void ClearObjInfo()
        {
            infoNameText.text = string.Empty;
            infoDetailText.text = string.Empty;
            infoImage.sprite = null;
        }

        // Storage UI Method 
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
            ClearObjInfo();
            currentSelectSlot = null;
            var slot = parent.GetStorageObject().GetStorageSlot();
        
            for (int i = 0; i < slot.Count; i++)
            {
                if (slot[i].quantity > 0)
                {
                    var newSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, storageSlot);
                    var componentSlot = newSlot.gameObject.GetComponent<ItemSlotUI>();
                    componentSlot.InitializeItem(slot[i].item,this,storageSlotInformation);
                    slotList.Add(componentSlot);
                    if (currentSelectSlot == null)
                    {
                        currentSelectSlot = componentSlot;
                    }
                }
                else
                {
                    parent.GetStorageObject().GetStorageSlot().RemoveAt(i);
                }
            }

            var layoutSpacer = storageSlot.GetComponent<LayoutGroupSpacer>();
            if(layoutSpacer != null)
            {
                layoutSpacer.LayoutSpacing();
            }
        }
    }
}
