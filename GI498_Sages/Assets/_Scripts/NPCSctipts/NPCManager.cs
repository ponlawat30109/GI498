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

        private NPCController orderingNpc;

        private FoodObject order;
        public FoodObject Order
        {
            get => order;
        }

        [SerializeField] private TVChangeImage tv;

        public bool haveOrder;

        [SerializeField] private RankSystem playerRankHolder;
        private List<FoodObject> foodList;

        private int numberOfNpcInMap;
        private int totalNumberOfOrder;

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

        public void RandomFood(NPCController npc)
        {
            if (foodList.Count > 0)
            {
                orderingNpc = npc;
                var foodListRange = foodList.Count;
                var foodNumber = Random.Range(0, foodListRange);
                order = foodList[foodNumber];
                haveOrder = true;
                if(_Scripts.ManagerCollection.Manager.Instance != null)
                    _Scripts.ManagerCollection.Manager.Instance.playerManager.PSHandler().JustPutInFood(order);
                tv.TVChangeSprite(order.itemIcon);
            }
        }

        public void CompleteOrder(int xp)
        {
            if (order != null && orderingNpc != null)
            {
                order = null;
                haveOrder = false;
                if(onTest == false)
                    DataCarrier.AddExp(xp);
                orderingNpc.GetOrder();
                orderingNpc = null;
                tv.SetDefaultImg();
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