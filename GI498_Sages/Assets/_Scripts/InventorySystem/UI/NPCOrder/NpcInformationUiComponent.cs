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
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private TMP_Text npcNameText;
        [SerializeField] private TMP_Text npcMedicText;

        public void InitComponent(string npcName,string medicDetail)
        {
            npcImage.sprite = defaultSprite;
            npcNameText.text = npcName;
            npcMedicText.text = medicDetail;
        }
    }
}
