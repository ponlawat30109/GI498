using System;
using _Scripts.InteractSystem.Interface;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.ManagerCollection;
using UnityEngine;

namespace _Scripts.InventorySystem
{
    public class MiniStorage : MonoBehaviour,IInteractableObject
    {
        [SerializeField] private int miniStorageId;
        
        [Header("Data")]
        [SerializeField] private StorageObject storageObject;
        public ItemObject currentHoldItemObject;

        [SerializeField] private int maxSlot;
        [SerializeField] private bool isStackable;
        //[SerializeField] private bool isSlotUISelectable;
        
        [Header("Model")]
        public Transform holdingPosition; // Position of Model On Player Hand
        public GameObject currentHoldItemModel; // Model to Show On Player Hand
        
        public event Action OnInteracted;

        private void Start()
        {
            storageObject.InitializeStorageObject(maxSlot, isStackable);
            
            if (storageObject.GetSlotCount() < Manager.Instance.storageManager.miniStorageCollections.Count)
            {
                for (int i = 0; i < Manager.Instance.storageManager.miniStorageCollections.Count; i++)
                {
                    storageObject.GetStorageSlot().Add(null);
                }
            }
        }
        
        private void Update()
        {
            if (currentHoldItemObject == null)
            {
                if (storageObject.GetSlotCount() > 0)
                {
                    if (storageObject.GetStorageSlot()[miniStorageId] != null)
                    {
                        if (storageObject.IsSlotIndexHasItem(miniStorageId))
                        {
                            SetCurrentItem();
                        }
                    }
                }
            }

            if (currentHoldItemModel.transform.childCount < 1)
            {
                if (IsCurrentItemNotNull())
                {
                    SetModel();
                }
                else
                {
                    ClearModel();
                }
            }
        }

        public void Interacted()
        {
            /* Note
            // - If have Item on Player Hand and This Mini Storage 'Have' Free Space
            // -- Then : Put Item to Mini Storage
            // - Else If have Item on Player Hand and This Mini Storage 'Do not' have Free Space
            // -- Then : Do nothings...
            // - Else If do not have Item on Player Hand and This Mini Storage 'Have' Free Space
            // -- Then : Do nothings...
            // - Else If do not have Item on Player Hand and This Mini Storage 'Do not' have Free Space (have item)
            // -- Then : Take Item from  Mini Storage
            */
            
            var psHandler = Manager.Instance.playerManager.PSHandler();
            var psStorage = psHandler.storage.GetStorageObject();

            if (psHandler.IsHoldingItem()) // Have Item on Player Hand
            {
                // Note
                // True -> Have Item => Don't have free space.
                // False -> Don't have Item => Have free space.
                if (storageObject.IsSlotIndexHasItem(miniStorageId) == false) // Have free space
                {
                    // Put Item to Mini Storage
                    // Pre Slot
                    storageObject.GetStorageSlot()[miniStorageId].item = psHandler.currentHoldItemObject;
                    storageObject.GetStorageSlot()[miniStorageId].quantity = 1;
                    psHandler.TakeOut(psStorage, storageObject, psHandler.currentHoldItemObject);
                    // ^ TakeOut from     ^   and put in    ^      ->   ^-Item

                    
                }
            }
            else // 'Do not' have Item on Player Hand
            {
                if (storageObject.IsSlotIndexHasItem(miniStorageId) == true) // 'Do not' have Free Space (have item)
                {
                    // Take Item from Mini Storage
                    // Clear Slot
                    storageObject.GetStorageSlot()[miniStorageId].item = null;
                    storageObject.GetStorageSlot()[miniStorageId].quantity = 0;
                    psHandler.PutIn(psStorage,storageObject,storageObject.GetItemFromSlotIndex(miniStorageId));
                    // ^ Put into        ^   and take out  ^      ->     ^-Item
                }
            }
            
            /* Old
            var psHandler = Manager.Instance.playerManager.PSHandler();
            var playerStorage = psHandler.storage.GetStorageObject();
           
            // If this Storage Have Free Space mean player Interact to Put Item && Player Have Item
            if (storageObject.HasFreeSpace() && psHandler.IsHoldingItem())
            {
                // Take Out Item from Player Hand to This
                var itemToTake = psHandler.currentHoldItemObject;
                Manager.Instance.playerManager.PSHandler().TakeOut(playerStorage,storageObject,itemToTake);
            }
            else if(playerStorage.HasFreeSpace() && storageObject.IsSlotIndexHasItem(miniStorageId))
            {
                // Take Out This Item to Player Hand.
                GetItem(playerStorage);
                ClearHolding();
            }*/
        }

        public void SetMiniStorageId(int value)
        {
            miniStorageId = value;
        }

        public StorageObject GetStorageObject()
        {
            return storageObject;
        }
        
        /// <summary>
        /// Meaning of return...
        /// <para>True mean currentHoldItemObject is Not Null</para>
        /// <para>False mean currentHoldItemObject is Null</para>
        /// </summary>
        /// <returns></returns>
        public bool IsCurrentItemNotNull()
        {
            return currentHoldItemObject != null;
        }
        
        private void SetCurrentItem()
        {
            if (storageObject.IsSlotIndexHasItem(miniStorageId))
            {
                currentHoldItemObject = storageObject.GetItemFromSlotIndex(miniStorageId);
            }
        }

        private void SetModel()
        {
            var newProp = Instantiate(currentHoldItemObject.ingamePrefab, holdingPosition);
            newProp.transform.SetParent(currentHoldItemModel.transform);
            //newProp.transform.localPosition = Vector3.zero;
            //newProp.transform.rotation = Quaternion.identity;
            
        }

        private void ClearHolding()
        {
            currentHoldItemObject = null;
        }

        private void ClearModel()
        {
            int childs = currentHoldItemModel.transform.childCount;
            
            for (int i = childs - 1; i > 0; i--)
            {
                Destroy(currentHoldItemModel.transform.GetChild(i).gameObject);
            }
            
            //Destroy(currentHoldItemModel.child);
        }
        
        private void GetItem(StorageObject output)
        {
            //Take out A to B
            storageObject.TakeOut(storageObject, output, currentHoldItemObject);
        }
    }
}