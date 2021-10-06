using System;
using _Scripts.Interact_System.Interface;
using _Scripts.InteractSystem.Interface;
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

        public void CollectTo(ContainerObject whereToPutIn)
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