using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCScript
{
    public class StandPoint : MonoBehaviour
    {
        [SerializeField] private GameObject customerPref;
        public bool isAvailable = true;
        public bool isSpawnPoint = false; //default
        public bool isReleasePoint = false; //default //Use for release NPC to Queue Order
        public bool isOrderPoint = false;
        public StandPoint nextPoint;
        [HideInInspector] public NPCController npcOwner;
        private List<GameObject> onPoint = new List<GameObject>();

        private void Start()
        {
            isAvailable = true;
            if (isSpawnPoint) //true
            {
                var npc = Instantiate(customerPref, transform.position, transform.rotation);
                var npcCtrl = npc.GetComponent<NPCController>();
                npcCtrl.GetInitTarget(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var npcCtrl = other.GetComponent<NPCController>();
            if(npcCtrl == null)
            {
                if (npcCtrl != npcOwner)
                {
                    onPoint.Add(other.gameObject);
                    isAvailable = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var npcCtrl = other.GetComponent<NPCController>();
            if (npcCtrl == null)
            {
                if (npcCtrl != npcOwner)
                {
                    onPoint.Remove(other.gameObject);
                    if (onPoint.Count == 0)
                    {
                        isAvailable = true;
                    }
                }
            }
        }
    }
}
