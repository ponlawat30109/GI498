using System;
using _Scripts.InteractSystem.Interface;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using UnityEngine;

namespace _Scripts.InventorySystem
{
    public class Item : MonoBehaviour, ICollectableObject
    {
        [SerializeField] private ItemObject item;
        [SerializeField] private Canvas interactableCanvas;
        public event Action OnCollected;

        public ItemObject GetItem()
        {
            return item;
        }
<<<<<<< HEAD

        public void CollectTo(ContainerObject whereToPutIn)
=======
        
        public void CollectTo(StorageObject whereToPutIn)
>>>>>>> InventoryDebug
        {
            whereToPutIn.AddItem(item);
        }

        private void OnTriggerEnter(Collider other)
        {
            interactableCanvas.gameObject.SetActive(true);
        }

        // private void OnTriggerStay(Collider other)
        // {
        // if (other.gameObject.tag == "Player")
        // {
        // interactableCanvas.gameObject.SetActive(true);
        // }
        // }

        private void OnTriggerExit(Collider other)
        {
            interactableCanvas.gameObject.SetActive(false);
        }
    }
}