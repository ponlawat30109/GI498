using System;
using _Scripts.InventorySystem.Interface;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.ManagerCollection;
using UnityEngine;

namespace _Scripts.InventorySystem.Player
{
    public class PlayerStorageHandler : MonoBehaviour
    {
        public Storage playerInventory;
        public Transform holdingPosition;
        public GameObject currentHolding;
        public ItemObject holdingItem;

        private void Awake()
        {
            playerInventory = Manager.Instance.storageManager.GetStorageByType(StorageObject.StorageTypeEnum.Player);
        }

        // Put Item Into A storage from B storage
        public void PutIn(StorageObject a, StorageObject b, ItemObject item)
        {
            a.AddItem(item);
            b.RemoveItem(item);
            SetHoldingItem(item);
        }

        // Take Out Item from A storage and Add to B storage
        public void TakeOut(StorageObject a, StorageObject b, ItemObject item)
        {
            a.RemoveItem(item);
            b.AddItem(item);
            ClearHoldingItem();
        }

        public void SetHoldingItem(ItemObject item)
        {
            if (IsHoldingItem() == false) // IsHoldingItem return FALSE
            {
                Debug.Log("Can not hold more than 1 item");
                return;
            }
        
            //Show Food Model
            holdingItem = item;
            currentHolding = Instantiate(holdingItem.ingamePrefab,holdingPosition);
            currentHolding.transform.localPosition = Vector3.zero;
            currentHolding.transform.rotation = Quaternion.identity;
        }

        public void ClearHoldingItem()
        {
            if (IsHoldingItem())
            {
                holdingItem = null;
            }
        }

        public bool IsHoldingItem()
        {
            bool isHolding = holdingItem != null;

            return isHolding;
        }
    }
}