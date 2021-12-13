using System;
using _Scripts.InventorySystem;
using _Scripts.InventorySystem.Player;
using UnityEngine;

namespace _Scripts.ManagerCollection
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerStorageHandler playerStorageHandler;

        public void Start()
        {
            
        }

        private void Update()
        {
            if (playerStorageHandler == null)
            {
                CheckHandler();
            }

            if (playerStorageHandler != null)
            {
                if (playerStorageHandler.storage == null)
                {
                    //Manager.Instance.storageManager.SetPlayerStorage();
                    Debug.Log("Player Storage is null");
                }
            }
        }

        private void CheckHandler()
        {
            if (playerStorageHandler == null)
            {
                var psHandlerObjects = GameObject.FindGameObjectsWithTag("psHandler");
                foreach (var psHandler in psHandlerObjects)
                {
                    if (psHandler.GetComponent<PlayerStorageHandler>() != null)
                    {
                        playerStorageHandler = psHandler.GetComponent<PlayerStorageHandler>();
                    }
                }
            }
        }

        public PlayerStorageHandler PSHandler()
        {
            return playerStorageHandler;
        }
    }
}
