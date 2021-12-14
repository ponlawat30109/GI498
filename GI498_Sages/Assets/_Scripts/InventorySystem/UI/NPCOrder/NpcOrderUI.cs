using System;
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

        private void Start()
        {
            acceptButton.onClick.AddListener(ButtonClickAction);
            closeButton.onClick.AddListener(ButtonClickAction);
        }

        public void InitOrderUI()
        {
            if (NPCScript.NPCManager.Instance != null)
            {
                var npcManager = NPCScript.NPCManager.Instance;
                
                //var npc = npcManager.GetNpc();
                //npcInformationUiComponent.InitComponent(npc.GetImage(),npc.GetName(),npc.GetMedicDetail());
                
                var order = npcManager.Order;
                
                if (order != null)
                {
                    orderInformationUiComponent.InitComponent(order);
                }
            }
            else
            {
                Debug.Log("NPC Manager is null.");
            }
        }

        public void ButtonClickAction()
        {
            InitOrderUI();
        }
    }
}
