using System;
using NPCScript;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.InventorySystem.UI.NPCOrder
{
    public class NpcInformationUiComponent : MonoBehaviour
    {
        [SerializeField] private Image npcImage;
        [SerializeField] private TMP_Text npcNameText;
        [SerializeField] private TMP_Text npcMedicText;

        public void InitComponent(Sprite npcSprite, string npcName,string medicDetail)
        {
            npcImage.sprite = npcSprite;
            npcNameText.text = npcName;
            npcMedicText.text = medicDetail;
        }
    }
}
