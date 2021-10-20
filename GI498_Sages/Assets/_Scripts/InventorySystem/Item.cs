using System;
using _Scripts.InteractSystem.Interface;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using UnityEngine;

namespace _Scripts.InventorySystem
{
    public class Item : MonoBehaviour,ICollectableObject
    {
        [SerializeField] private ItemObject item;
        [SerializeField] private Canvas interactableCanvas;
        public event Action OnCollected;

        public ItemObject GetItem()
        {
            return item;
        }
        
        public void CollectTo(StorageObject whereToPutIn)
        {
            whereToPutIn.AddItem(item);
        }

        private void OnTriggerEnter(Collider other)
        {
            interactableCanvas.gameObject.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            interactableCanvas.gameObject.SetActive(false);
        }
    }
}