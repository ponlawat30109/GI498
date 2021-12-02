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
