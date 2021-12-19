using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

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

        void Start()
        {
            StartCoroutine(SpawnPlayer(1));
        }

        IEnumerator SpawnPlayer(int time)
        {
            yield return new WaitForSeconds(time);
            
            // Spawn
            player = (GameObject)Instantiate(playerPrefabs, spawnpoint.transform.position, Quaternion.identity);
            Debug.Log($"Camera FoV : {_vcam.m_Lens.FieldOfView}");
            Transform followTarget = player.transform;
            _vcam.Follow = followTarget;
            _vcam.LookAt = followTarget;
        }

        
        private void OnDestroy()
        {
            Destroy(player.gameObject);
        }
    }
}