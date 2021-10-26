using System;
using _Scripts.InteractSystem.Interface;
using _Scripts.InventorySystem.Player;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.ManagerCollection;
using UnityEngine;

namespace _Scripts.InventorySystem
{
    public class MiniStorage : MonoBehaviour,IInteractableObject
    {
        [Header("Data")]
        public ItemObject currentHoldItemObject;

        [Header("Model")]
        public Transform holdingPosition; // Position of Model On Player Hand
        public GameObject currentHoldItemModel; // Model to Show On Player Hand
        
        public event Action OnInteracted;

        private void Update()
        {
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
            //var psStorage = psHandler.storage.GetStorageObject();

            if (psHandler.IsHoldingItem()) // Have Item on Player Hand
            {
                // Note
                // True -> Have Item => Don't have free space.
                // False -> Don't have Item => Have free space.
                if (IsCurrentItemNotNull() == false) // Have free space
                {
                    // Put Item to Mini Storage
                    PlaceItem(psHandler, psHandler.currentHoldItemObject);
                }
            }
            else // 'Do not' have Item on Player Hand
            {
                if (IsCurrentItemNotNull() == true) // 'Do not' have Free Space (have item)
                {
                    // Take Item from Mini Storage
                    TakeItem(psHandler);
                    
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

        public void PlaceItem(PlayerStorageHandler ps,ItemObject item)
        {
            if (currentHoldItemObject != null)
            {
                return;
            }

            currentHoldItemObject = item; // +
            ps.JustTakeOut(ps.storage.GetStorageObject(),item); // -

        }

        public void TakeItem(PlayerStorageHandler ps)
        {
            ps.JustPutIn(ps.storage.GetStorageObject(), currentHoldItemObject); // +
            currentHoldItemObject = null; // -
        }
        
        //public void SetMiniStorageId(int value)
        //{
        //    miniStorageId = value;
        //}

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
        }
    }
}