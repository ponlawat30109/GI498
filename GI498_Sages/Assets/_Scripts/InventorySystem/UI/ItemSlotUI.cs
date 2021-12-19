using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.InventorySystem.UI
{
    public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private ItemObject item;
        [SerializeField] private Image itemImage;
        [SerializeField] private Image itemFrameImage;
        [SerializeField] private Sprite selectUiSprite;
        [SerializeField] private Sprite deselectUiSprite;
        [SerializeField] private StorageUI parentStorageUI;
        [SerializeField] private TMP_Text nameText;

        [SerializeField] private GameObject infoPrefab;
        [SerializeField] private Transform storageInfoTransform;
        [SerializeField] private StorageInformationUI informationUI;

        public void InitializeItem(ItemObject toInitItem, StorageUI parentStorage, Transform transformStorageInfo)
        {
            item = toInitItem;
            itemImage.sprite = toInitItem.itemIcon;
            nameText.text = toInitItem.itemName;

            parentStorageUI = parentStorage;
            storageInfoTransform = transformStorageInfo;

            //transform.SetParent(parentStorageUI.GetStorageSlotTransform());
        }

        public ItemObject GetItem()
        {
            return item;
        }

        private void Update()
        {
            if (parentStorageUI.GetParent() != null)
            {
                if (IsInCurrentSelect(this))
                {
                    itemFrameImage.sprite = selectUiSprite;
                }
                else
                {
                    itemFrameImage.sprite = deselectUiSprite;
                }
            }
        }

        public bool IsInCurrentSelect(ItemSlotUI toCheckSlot)
        {
            var isFound = false;
            var selectSlotList = parentStorageUI.GetParent().GetSelectSlotList();
                
            for (int i = 0; i < selectSlotList.Count; i++)
            {
                if (selectSlotList[i] == toCheckSlot)
                {
                    isFound = true;
                }
            }

            return isFound;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (parentStorageUI.GetParent().IsSlotUISelectable)
            {
                // If This Current Select
                if (IsInCurrentSelect(this))
                {
                    // DeSelect
                    itemFrameImage.sprite = deselectUiSprite;
                    parentStorageUI.GetParent().DeSelectSlot(this);

                }
                else
                {
                    // Select This
                    parentStorageUI.GetParent().AddSelectSlot(this);
                    parentStorageUI.ChangeObjInfo(item.itemName, item.description, item.itemIcon);
                }
            }
        }
    }
}
