using UnityEngine;

namespace _Scripts.InventorySystem.UI.NPCOrder
{
    public class NpcOrderUI : MonoBehaviour
    {
        [SerializeField] private NpcInformationUiComponent npcInformationUiComponent;
        [SerializeField] private OrderInformationUiComponent orderInformationUiComponent;

        public void InitOrderUI()
        {
            npcInformationUiComponent.InitComponent();
        }
        
    }
}
