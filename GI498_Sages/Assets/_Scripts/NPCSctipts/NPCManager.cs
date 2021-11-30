using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCScript
{
    public class NPCManager : MonoBehaviour
    {
        [SerializeField] public bool onTest;
        [SerializeField] private TVChangeImage tv;
        [SerializeField] private RankSystem playerRankHolder;

        #region NPCLoop
        private float releaseLoopTime = 0;
        private bool isFreeRelease;
        #endregion

        #region
        private static NPCManager instance;
        public static NPCManager Instance
        {
            get => instance;
        }
        private NPCController waitForReleaseNpc;
        private NPCController orderingNpc;

        private FoodObject order;
        public FoodObject Order
        {
            get => order;
        }
        private List<FoodObject> foodList;
        public int numberOfNpcInMap { get; private set; }
        public int orderRemaining {get; private set;}
        #endregion

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
            }

            instance = this;
            orderRemaining = 0;
            isFreeRelease = false;
        }

        private void Start()
        {
            onTest = false;
            foodList = playerRankHolder.FoodList;
        }

        public void CompleteOrder(int xp)
        {
            if (order != null && orderingNpc != null)
            {
                order = null;
                if (onTest == false)
                    DataCarrier.AddExp(xp);
                orderingNpc.GetOrder();
                orderingNpc = null;
                tv.SetDefaultImg();
            }
        }

        public void SetRemainingOrder(int _orderRemaining)
        {
            orderRemaining = _orderRemaining;
        }
        public void AddRemainingOrder(int number)
        {
            orderRemaining += number;
        }

        ///<summary>
        /// if releaseNow is true: First NPC will be release immediately
        /// </summary>
        public void SetReleaseTime(float _releaseLoopTime, bool releaseNow)
        {
            releaseLoopTime = _releaseLoopTime;
            if (releaseNow == true) ReleaseNpc();
        }

        ///<summary>
        /// if _isFreeRelease is false you have to use [ReleaseNPC()] by yourself
        /// </summary>
        public void SetReleaseNoTime(bool _isFreeRelease)
        {
            releaseLoopTime = 0;
            isFreeRelease = _isFreeRelease;
            if(isFreeRelease == true) ReleaseNpc();

            StopAllCoroutines();
            StartCoroutine(StartReleaseCountDown());
        }

        public void SetWaitingNpc(NPCController npc)
        {
            waitForReleaseNpc = npc;
        }

        public void ReleaseNpc()
        {
            if (orderRemaining > 0 && waitForReleaseNpc != null)
            {
                waitForReleaseNpc.ReleaseToQueue();
                waitForReleaseNpc = null;
            }
        }

        private IEnumerator StartReleaseCountDown()
        {
            WaitForSeconds nextRelease = new WaitForSeconds(releaseLoopTime);
            yield return nextRelease;
            while (orderRemaining > 0)
            {
                if(waitForReleaseNpc != null)
                {
                    ReleaseNpc();
                    yield return nextRelease;
                }
                else
                {
                    yield return null;
                }
            }
        }

        public void RandomFood(NPCController npc)
        {
            if (foodList.Count > 0)
            {
                orderingNpc = npc;
                var foodListRange = foodList.Count;
                var foodNumber = Random.Range(0, foodListRange);
                order = foodList[foodNumber];
                if(_Scripts.ManagerCollection.Manager.Instance != null)
                    _Scripts.ManagerCollection.Manager.Instance.playerManager.PSHandler().JustPutInFood(order);
                tv.TVChangeSprite(order.itemIcon);
            }
        }

        private void OnApplicationQuit()
        {
            if (playerRankHolder != null)
                playerRankHolder.ClearList();
        }

        private void OnGUI()
        {
            if(onTest)
            {
                if(GUILayout.Button("Complete Order"))
                {
                    CompleteOrder(10);
                }
                if(GUILayout.Button("ReleaseNPC"))
                {
                    if (waitForReleaseNpc != null)
                        ReleaseNpc();
                    else
                    {
                        Debug.Log("No NPC to Release");
                    }
                }
                if(GUILayout.Button("SetRemainingOrder"))
                {
                    SetRemainingOrder(5);
                    Debug.Log("SetRemainingOrder(5)");
                }
                if (GUILayout.Button("SetReleaseTime"))
                {
                    SetReleaseTime(5f, true);
                    Debug.Log("SetReleaseTime | _releaseLoopTime " + 5f + " releaseNow " + true);
                }
                if(GUILayout.Button("SetReleaseNoTime(true)"))
                {
                    SetReleaseNoTime(true);
                }
                if(GUILayout.Button("RemainingOrder"))
                {
                    Debug.Log("RemainingOrder " + orderRemaining);
                }
            }
        }

    }

    
}