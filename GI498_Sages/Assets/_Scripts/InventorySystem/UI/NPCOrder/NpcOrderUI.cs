using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.InventorySystem.UI.NPCOrder
{
    public class NpcOrderUI : MonoBehaviour
    {
        [SerializeField] private NpcInformationUiComponent npcInformationUiComponent;
        [SerializeField] private OrderInformationUiComponent orderInformationUiComponent;

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
            
        }
        
    }
}
