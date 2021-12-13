﻿using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.ManagerCollection;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.InventorySystem.UI
{
    public class StorageUIAdditionalButton : MonoBehaviour
    {
        [SerializeField] private Storage parent;
        [SerializeField] private Button putInButton;
        [SerializeField] private Button takeOutButton;
        [SerializeField] private Button closeButton;

        public void Start()
        {
            putInButton.onClick.AddListener(PutInButtonAction);
            takeOutButton.onClick.AddListener(TakeOutButtonAction);
            // closeButton.onClick.AddListener(CloseButtonAction);
        }
        
        // Storage Interact Method
        // Put Item Into A storage from B storage
        // A = this Storage Object (parent)
        // B = player Storage Object (player)
        public void PutInButtonAction()
        {
            var psHandler = Manager.Instance.playerManager.PSHandler();
            var a = parent.GetStorageObject();
            var b = psHandler.storage.GetStorageObject();
            var item = psHandler.currentHoldItemObject;
            
            parent.PutIn(a, b, item);
            Debug.Log($"[Button] Put {item.itemName} from {b.storageType} to {a.storageType}.");
        }

        // Take Out Item from A storage and Add to B storage
        // A = this Storage Object (parent)
        // B = player Storage Object (player)
        public void TakeOutButtonAction()
        {
            var psHandler = Manager.Instance.playerManager.PSHandler();
            
            var a = parent.GetStorageObject();
            var b = psHandler.storage.GetStorageObject();

            if (parent.GetSelectSlot() != null)
            {
                var item = parent.GetSelectSlot().GetItem();
                if (item != null)
                {
                    parent.TakeOut(a, b, item);
                    //Debug.Log($"[Button] Take {item.itemName} from {a.storageType} to {b.storageType}.");
                    parent.CloseUI();
                }
            }
        }
    
        public void CloseButtonAction()
        {
            parent.CloseUI();
        }
    }
}