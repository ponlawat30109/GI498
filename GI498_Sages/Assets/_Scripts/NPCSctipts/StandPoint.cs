using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NPCScript
{
    public class StandPoint : MonoBehaviour
    {
        [SerializeField] private GameObject customerPref;
        public bool isSpawnPoint = false; //default
        public bool isReleasePoint = false; //default //Use for release NPC to Queue Order
        public bool isOrderPoint = false;
        public StandPoint nextPoint;


        void Start()
        {
            StartCoroutine(SpawnNpc(2));
           
        }

        IEnumerator SpawnNpc(int time)
        {
            yield return new WaitForSeconds(time);
            // Spawn
            if (isSpawnPoint) //true
            {
                var npc = Instantiate(customerPref, transform.position, transform.rotation);
                var npcCtrl = npc.GetComponent<NPCController>();
                npcCtrl.GetInitTarget(this);
                
                
            }
        }
    }
}