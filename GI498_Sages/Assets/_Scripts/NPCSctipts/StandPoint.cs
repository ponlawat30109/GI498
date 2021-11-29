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
        public bool isRandomSkinPoint = false; //default
        public bool isOrderPoint = false;
        public StandPoint nextPoint;
        [HideInInspector] public NPCController npcOwner;
        private List<GameObject> onPoint = new List<GameObject>();

        public bool isChechInPoint = false;
        

        private void Start()
        {
            isAvailable = true;
            if (isSpawnPoint) //true
            {
                var npc = Instantiate(customerPref, transform.position, transform.rotation);
                var npcCtrl = npc.GetComponent<NPCController>();
                npcCtrl.GetInitTarget(nextPoint);

                if (isRandomSkinPoint)
                    npcCtrl.modelCom.RandomSkin();
            }

            if(isChechInPoint)
            {

            }
        }

        private void OnTriggerEnter(Collider other)
        {
            onPoint.Add(other.gameObject);

            var npcCtrl = other.GetComponent<NPCController>();
            //var npc = other.GetComponent<ModelScript.ModelComponent>();
            if(npcCtrl != null)
            {
                if (npcCtrl != npcOwner)
                    isAvailable = false;
                else
                {
                    npcCtrl.GetInitTarget(nextPoint);
                    if (isRandomSkinPoint)
                        npcCtrl.modelCom.RandomSkin();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            onPoint.Remove(other.gameObject);
            if (onPoint.Count == 0)
            {
                isAvailable = true;
            }
        }
    }
}
