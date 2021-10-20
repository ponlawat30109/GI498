using UnityEngine;

namespace _Scripts.ManagerCollection
{
    public class Manager : MonoBehaviour
    {
        [Header("Manager")]
        [SerializeField] public StorageManager storageManager;

        [SerializeField] public PlayerManager playerManager;
        
        public static Manager Instance{ set; get;} // Instance

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
            // DontDestroyOnLoad(this);
        }
    }
}