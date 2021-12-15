using System;
using _Scripts.NPCSctipts;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.InventorySystem.UI.NPCOrder
{
    public class NpcOrderUI : MonoBehaviour
    {
        [SerializeField] private NpcInformationUiComponent npcInformationUiComponent;
        [SerializeField] private OrderInformationUiComponent orderInformationUiComponent;

        [SerializeField] private Button acceptButton;
        [SerializeField] private Button closeButton;

        private bool isTakeOrder = false;

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

        public void InitOrderUI()
        {
            if (NPCScript.NPCManager.Instance != null)
            {
                var npcManager = NPCScript.NPCManager.Instance;
                var npcInfo = new NpcInformation();
                var npc = npcInfo.RandomInfo();
                var order = npcManager.Order;
                
                if (order != null)// && npc != null)
                {
                    npcInformationUiComponent.InitComponent(npc.GetRandomName(),npc.GetMedicString());
                    orderInformationUiComponent.InitComponent(order);
                    this.gameObject.SetActive(false);
                    isTakeOrder = true;
                }
            }
            else
            {
                Debug.Log("NPC Manager is null.");
            }
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
            InitOrderUI();
        }
    }
}
