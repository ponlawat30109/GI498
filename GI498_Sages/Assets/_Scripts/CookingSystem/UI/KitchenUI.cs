using System.Collections.Generic;
using _Scripts.InventorySystem.UI;
using UnityEngine;

namespace _Scripts.CookingSystem.UI
{
    public class KitchenUI : MonoBehaviour
    {
        [SerializeField] private Kitchen parent;
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform storageSlot;
        [SerializeField] private Transform storageSlotInformation;

        public List<IngredientSlotUI> slotList = new List<IngredientSlotUI>();

        private float currentTime = 0;
        private float refreshInterval = 1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

        void UpdateUI()
        {
            // Update
            CreateUI(); // Re-create -- Implement later...
        }
    
        public Kitchen GetParent()
        {
            return parent;
        }

        public Transform GetStorageSlotTransform()
        {
            return storageSlot;
        }

        public void CreateUI()
        {
            var slot = parent.GetStorageObject().GetStorageSlot();
        
            for (int i = 0; i < slot.Count; i++)
            {
                if (slot[i].quantity > 0)
                {
                    var newSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
                    var componentSlot = newSlot.gameObject.GetComponent<IngredientSlotUI>();
                    componentSlot.InitializeItem(slot[i].item,this,storageSlotInformation,parent.IsOneOfIngredient(slot[i].item));
                    slotList.Add(componentSlot);
                }
                else
                {
                    parent.GetStorageObject().GetStorageSlot().RemoveAt(i);
                }
            }
        }
    }
}
