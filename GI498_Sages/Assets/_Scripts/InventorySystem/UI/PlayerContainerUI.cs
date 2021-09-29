using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.InventorySystem.UI
{
    public class PlayerContainerUI : MonoBehaviour
    {
        public ItemHoldingUI slotPrefab;

        void Start()
        {
            UpdateHoldingUI();
        }

        public void UpdateHoldingUI()
        {
            var i = Manager.Instance.playerManager.inventoryHandler.holdingItem;

            if (i == null)
            {
                return;
            }
            
            slotPrefab.InitializeItem(i,this.gameObject);
        }
    }
}