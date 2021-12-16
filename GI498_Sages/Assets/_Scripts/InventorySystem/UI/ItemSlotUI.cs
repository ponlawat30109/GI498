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
            if (parentStorageUI.GetParent().GetSelectSlot() == this)
            {
                itemFrameImage.sprite = selectUiSprite;
            }
            else
            {
                itemFrameImage.sprite = deselectUiSprite;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (parentStorageUI.GetParent().IsSlotUISelectable)
            {
                if (parentStorageUI.GetParent().GetSelectSlot() == this)
                {
                    itemFrameImage.sprite = deselectUiSprite;
                    parentStorageUI.GetParent().DeSelectSlot();

                }
                else
                {
                    parentStorageUI.GetParent().SetSelectSlot(this);
                    parentStorageUI.ChangeObjInfo(item.itemName, item.description, item.itemIcon);
                }
            }
        }
    }
}
