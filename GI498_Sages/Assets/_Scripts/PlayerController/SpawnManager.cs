using System.Collections;
using System.Collections.Generic;
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

        // void Awake()
        // {
        //     _vcam = GetComponent<CinemachineVirtualCamera>();
        // }

        void Start()
        {
            GameObject player = (GameObject)Instantiate(playerPrefabs, spawnpoint.transform.position, Quaternion.identity);

            Debug.Log(_vcam.m_Lens.FieldOfView);

            Transform followTarget = player.transform;
            _vcam.Follow = followTarget;
            _vcam.LookAt =followTarget;
        }

        // void Update()
        // {

        // }
    }
}

