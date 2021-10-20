using _Scripts.InventorySystem;
using _Scripts.InventorySystem.Player;
using UnityEngine;

namespace _Scripts.ManagerCollection
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerStorageHandler playerStorageHandler;

        public PlayerStorageHandler PSHandler()
        {
            return playerStorageHandler;
        }
    }
}
