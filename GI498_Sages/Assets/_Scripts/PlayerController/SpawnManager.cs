using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;

namespace _Scripts.ManagerCollection
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Player Model")]
        [SerializeField] GameObject playerPrefabs;

        [Header("Spawn point")]
        [SerializeField] Transform spawnpoint;

        [Header("Camera")]
        [SerializeField] CinemachineVirtualCamera _vcam;

        private GameObject player;

        void Awake()
        {
            // _vcam = GetComponent<CinemachineVirtualCamera>();
            // UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("KitchenAssembly"));
        }

        async void Start()
        {
            // UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("KitchenAssembly"));
            await Task.Delay(System.TimeSpan.FromSeconds(0.1));
            player = (GameObject)Instantiate(playerPrefabs, spawnpoint.transform.position, Quaternion.identity);

            Debug.Log($"Camera FoV : {_vcam.m_Lens.FieldOfView}");

            Transform followTarget = player.transform;
            _vcam.Follow = followTarget;
            _vcam.LookAt = followTarget;
        }

        // void Update()
        // {

        // }

        private void OnDestroy()
        {
            Destroy(player.gameObject);
        }
    }
}

