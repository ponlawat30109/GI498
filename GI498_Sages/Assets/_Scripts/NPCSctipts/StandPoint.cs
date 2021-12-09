using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private async void Start()
        {
            await Task.Delay(System.TimeSpan.FromSeconds(0.1));
            if (isSpawnPoint) //true
            {
                var npc = Instantiate(customerPref, transform.position, transform.rotation);
                var npcCtrl = npc.GetComponent<NPCController>();
                npcCtrl.GetInitTarget(this);
            }
        }

    }
}
