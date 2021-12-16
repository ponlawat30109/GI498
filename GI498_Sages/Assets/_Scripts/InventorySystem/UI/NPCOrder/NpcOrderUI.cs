using System;
using _Scripts.NPCSctipts;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace _Scripts.InventorySystem.UI.NPCOrder
{
    public class NpcOrderUI : MonoBehaviour
    {
        public static NpcOrderUI Instance;
       
        
        [SerializeField] private NpcInformationUiComponent npcInformationUiComponent;
        [SerializeField] private OrderInformationUiComponent orderInformationUiComponent;

        [SerializeField] public NpcInformation currentNpcInformation;
        
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button closeButton;

        private bool isTakeOrder;

        private void Awake()
        {
            isTakeOrder = false;
            Instance = this;
        }

        private void Start()
        {
            acceptButton.onClick.AddListener(ButtonTakeAction);
            closeButton.onClick.AddListener(ButtonCloseAction);
        }

        private void Update()
        {
            if (isTakeOrder == true)
            {
                acceptButton.gameObject.SetActive(false);
                closeButton.gameObject.SetActive(true);
            }
            else
            {
                acceptButton.gameObject.SetActive(true);
                closeButton.gameObject.SetActive(false);
            }
        }

        public void InitNpc()
        {
            var npcInfo = new NpcInformation();
            currentNpcInformation = npcInfo.RandomInfo();
            Debug.Log($"[RandomNPC] {currentNpcInformation.GetCurrentName()}, {currentNpcInformation.GetMedic()}");
        }
        
        public void InitOrderUI()
        {
            if (NPCScript.NPCManager.Instance != null)
            {
                var npcManager = NPCScript.NPCManager.Instance;
                var npc = currentNpcInformation;

                if (npc != null)// && npc != null)
                {
                    npcInformationUiComponent.InitComponent(npc.GetCurrentName(),npc.GetMedicString());

                    // Need NPCManager.RandomFood first.
                    
                    if (npcManager.Order != null)
                    {
                        var order = npcManager.Order;
                        orderInformationUiComponent.InitComponent(order);
                    }
                    
                    this.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.Log("NPC Manager is null.");
            }
        }

        public void TakeOrder()
        {
            var npcManager = NPCScript.NPCManager.Instance;
            
            if (_Scripts.ManagerCollection.Manager.Instance != null)
            {
                // Give Player Recipe
                var order = npcManager.Order;
                _Scripts.ManagerCollection.Manager.Instance.playerManager.PSHandler().JustPutInFood(order);
                isTakeOrder = true;
                this.gameObject.SetActive(false);
                Debug.Log($"[NpcOrderUI.cs] IsTakeOrder : {IsTakeOrder()}");
            }
        }

        public bool IsTakeOrder()
        {
            return isTakeOrder;
        }

        public void SetIsTakeOrder(bool value)
        {
            isTakeOrder = value;
        }

        public void OpenUI()
        {
            this.gameObject.SetActive(true);
        }
        
        public void ButtonCloseAction()
        {
            this.gameObject.SetActive(false);
        }
        
        public void ButtonTakeAction()
        {
            TakeOrder();
        }

        private void OnApplicationQuit()
        {
            isTakeOrder = false;
        }
    }
}
