using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCScript
{
    public class StandPoint : MonoBehaviour
    {
        [SerializeField] private GameObject customerPref;
        public bool isSpawnPoint = false; //default
        public bool isReleasePoint = false; //default //Use for release NPC to Queue Order
        public bool isOrderPoint = false;
        public StandPoint nextPoint;

        private void Start()
        {
            if (isSpawnPoint) //true
            {
                var npc = Instantiate(customerPref, transform.position, transform.rotation);
                var npcCtrl = npc.GetComponent<NPCController>();
                npcCtrl.GetInitTarget(this);
            }
        }
    }
}
