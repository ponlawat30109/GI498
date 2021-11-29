using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPCScript
{
    public class NPCManager : MonoBehaviour
    {
        [SerializeField] public bool onTest;
        private static NPCManager instance;
        public static NPCManager Instance
        {
            get => instance;
        }

        private static NPCController orderingNpc;

        private static FoodObject order;
        public static FoodObject Order
        {
            get => order;
        }

        [SerializeField] private TVChangeImage tv;

        public static bool haveOrder;

        [SerializeField] private RankSystem playerRankHolder;
        private static List<FoodObject> foodList;

        private static int numberOfNpcInMap;
        private static int totalNumberOfOrder;

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
            }

            instance = this;
        }

        private void Start()
        {
            onTest = false;
            foodList = playerRankHolder.FoodList;
        }

        public static void RandomFood(NPCController npc)
        {
            if (foodList.Count > 0)
            {
                orderingNpc = npc;
                var foodListRange = foodList.Count;
                var foodNumber = Random.Range(0, foodListRange);
                order = foodList[foodNumber];
                haveOrder = true;
                _Scripts.ManagerCollection.Manager.Instance.playerManager.PSHandler().JustPutInFood(order);
                Debug.Log("order " + order.itemName);
            }
        }

        public static void CompleteOrder(int xp)
        {
            if (order != null && orderingNpc != null)
            {
                order = null;
                haveOrder = false;
                DataCarrier.AddExp(xp);
                orderingNpc.GetOrder();
                orderingNpc = null;
            }
            else
            {
                if(order != null)
                    Debug.Log("(NPC Manager) Complete Order: order Bug Null");
                else
                    Debug.Log("(NPC Manager) Complete Order: orderingNpc Bug Null");
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
            }
        }

    }

    
}