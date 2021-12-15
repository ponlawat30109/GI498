using System.Collections;
using System.Collections.Generic;
using _Scripts.ManagerCollection;
using _Scripts.NPCSctipts;
using UnityEngine;

namespace NPCScript
{
    /// <summary>
    /// Important Function:
    /// -- [!!]Complete Order >> To tell NPC(in Ordering) to go home
    /// -- [!!]SetRemainingOrder / AddRemainingOrder >> If not set NPC not come
    /// -- SetReleaseTime / SetReleaseNoTime >> set how to automatic release NPC
    /// -- ReleaseNpc >> release NPC immediately
    /// (* [!!] = need to use)
    /// </summary>
    public class NPCManager : MonoBehaviour
    {
        [SerializeField] public bool onTest;
        [SerializeField] private TVChangeImage tv;
        [SerializeField] private RankSystem playerRankHolder;
        [SerializeField] private AudioManager.Track orderSound;
        [SerializeField] private AudioManager.Track completeOrderSound;
        [SerializeField] private List<LevelStandard> levelList;

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
        public FoodObject Order { get => order; }
        
        private List<FoodObject> foodList;

        private LevelStandard levelStandard;
        public LevelStandard LevelStandard { get => levelStandard; }

        public int numberOfNpcInQueue { get; private set; }
        public int orderRemaining {get; private set;}
        #endregion

        private System.Random rnd = new System.Random();

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
            }

            instance = this;
            orderRemaining = 0;
            numberOfNpcInQueue = 0;
            isFreeRelease = false;
        }

        private void Start()
        {
            //this section is just for test
            if (onTest == true)
            {
                Debug.Log("NPCManager: Start Test");
                SetRemainingOrder(5);
                SetReleaseNoTime(true);
            }
            //End Test

            onTest = false;
            foodList = playerRankHolder.FoodList;
            Debug.Assert(tv != null, "NPCManager: tv is null");
        }

        public void CompleteOrder()
        {
            if(AudioManager.Instance != null)
                AudioManager.Instance.PlaySfx(completeOrderSound);

            if (order != null && orderingNpc != null)
            {
                order = null;
                orderingNpc.GetOrder();
                orderingNpc = null;
                if(tv != null)
                    tv.SetDefaultImg();
                orderRemaining--;
                numberOfNpcInQueue--;
            }

            else if(onTest && orderingNpc != null)
            {
                orderingNpc.GetOrder();
                orderingNpc = null;
                orderRemaining--;
                numberOfNpcInQueue--;
            }

            if (onTest)
            {
                Debug.Log("orderRemaining " + orderRemaining);
                Debug.Log("numberOfNpcInQueue " + numberOfNpcInQueue);
            }

            if (Manager.Instance.storageManager != null)
            {
                Manager.Instance.storageManager.ClearIngredientQuantity();
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
            StopAllCoroutines();
            StartCoroutine(StartReleaseCountDown());
        }

        ///<summary>
        /// if _isFreeRelease is true this manager will auto ReleaseNPC with no Countdown
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
            if (isFreeRelease == true)
                ReleaseNpc();
        }

        public void ReleaseNpc()
        {
            if (orderRemaining > numberOfNpcInQueue && waitForReleaseNpc != null)
            {
                numberOfNpcInQueue++;
                waitForReleaseNpc.ReleaseToQueue();
                waitForReleaseNpc = null;
            }

        }

        private IEnumerator StartReleaseCountDown()
        {
            WaitForSeconds nextRelease = new WaitForSeconds(releaseLoopTime);
            WaitForSeconds oneSecond = new WaitForSeconds(1f);
            yield return nextRelease;
            while (orderRemaining > numberOfNpcInQueue)
            {
                if (waitForReleaseNpc != null)
                {
                    ReleaseNpc();
                    yield return nextRelease;
                }
                else
                {
                    yield return oneSecond;
                }
            }
            Debug.Log("orderRemaining " + orderRemaining);
        }

        public void RandomFood(NPCController npc)
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySfx(orderSound);

            if (foodList.Count > 0)
            {
                orderingNpc = npc;
                var foodListRange = foodList.Count;
                var foodNumber = Random.Range(0, foodListRange);
                order = foodList[foodNumber];
                if (_Scripts.ManagerCollection.Manager.Instance != null)
                {
                    _Scripts.ManagerCollection.Manager.Instance.playerManager.PSHandler().JustPutInFood(order);
                }

                if(levelList.Count > 0)
                {
                    levelStandard = levelList[rnd.Next(levelList.Count)];
                }
                else
                {
                    levelStandard = null;
                }

                if(tv != null)
                    tv.TVChangeSprite(order.itemIcon);
            }

            if(onTest)
            {
                orderingNpc = npc;
            }
        }

        public void DefineFoodList(NpcInformation npcInfo)
        {
            switch (npcInfo.GetMedic())
            {
                case NpcInformation.NpcPatientType.Normal:
                {
                    
                    break;
                }
                
                case NpcInformation.NpcPatientType.KidneyDisease:
                {
                    
                    break;
                }
                
                case NpcInformation.NpcPatientType.Diabetes:
                {
                    
                    break;
                }
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
                    CompleteOrder();
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