using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.InventorySystem.UI
{
    public class ItemSlotUI : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
    {
        
    
        [SerializeField] public ItemObject item;
        [SerializeField] private Image itemImage;
        [SerializeField] private Image itemFrameImage;
        [SerializeField] private Sprite selectUiSprite;
        [SerializeField] private Sprite deselectUiSprite;
        [SerializeField] private StorageUI parentStorageUI;
        
        [SerializeField] private GameObject infoPrefab;
        [SerializeField] private Transform storageInfoTransform;
        [SerializeField] private StorageInformationUI informationUI;
    
        public void InitializeItem(ItemObject toInitItem,StorageUI parentStorage,Transform transformContainerInfo)
        {
            item = toInitItem;
            itemImage.sprite = toInitItem.itemIcon;
        
            parentStorageUI = parentStorage;
            storageInfoTransform = transformContainerInfo;

            transform.SetParent(parentStorageUI.GetStorageSlotTransform());
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
                parentStorageUI.GetParent().SetSelectSlot(this);
            }
        }
    
        public void OnPointerEnter(PointerEventData eventData)
        {
            //if (parentStorageUI.GetParent().IsSlotUISelectable)
            //{
                var newInfo = Instantiate(infoPrefab, Vector3.zero, Quaternion.identity);
                informationUI = newInfo.gameObject.GetComponent<StorageInformationUI>();
                informationUI.InitializeInformation(item.itemName, item.description, item.itemIcon);
                informationUI.transform.SetParent(storageInfoTransform.transform);
            //}
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            informationUI.Clear();
        }

   
    }
}
