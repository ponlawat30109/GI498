using UnityEngine;

namespace _Scripts
{
    public class Manager : MonoBehaviour
    {
        [Header("Manager")]
        [SerializeField] public ContainerManager containerManager;

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