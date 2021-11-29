using _Scripts.InventorySystem.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

namespace _Scripts.CookingSystem.UI
{
    public class IngredientSlotUI : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] private IngredientObject item;
        [SerializeField] private Image itemImage;
        [SerializeField] private Image itemBackground;
        [SerializeField] private Image itemFrameImage;
        [SerializeField] private Sprite selectUiSprite;
        [SerializeField] private Sprite deselectUiSprite;
        [SerializeField] private Sprite correctIngredientSprite;
        [SerializeField] private Sprite notCorrectIngredientSprite;
        [SerializeField] private Sprite defualtBackgroundSprite;
        [SerializeField] private KitchenUI parentStorageUI;
        
        [SerializeField] private GameObject infoPrefab;
        [SerializeField] private Transform storageInfoTransform;
        [SerializeField] private StorageInformationUI informationUI;

        private bool _isOneOfIngredientRecipe = false;
        
        public void InitializeItem(IngredientObject toInitItem,KitchenUI parentStorage,Transform transformStorageInfo,bool _isOneOfIngredientRecipe)
        {
            item = toInitItem;
            itemImage.sprite = toInitItem.itemIcon;
        
            parentStorageUI = parentStorage;
            storageInfoTransform = transformStorageInfo;
            this._isOneOfIngredientRecipe = _isOneOfIngredientRecipe;
            transform.SetParent(parentStorageUI.GetStorageSlotTransform());
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

            if (_isOneOfIngredientRecipe)
            {
                itemBackground.sprite = correctIngredientSprite;
            }
            else if(_isOneOfIngredientRecipe == false)
            {
                itemBackground.sprite = notCorrectIngredientSprite;
            }
            else
            {
                itemBackground.sprite = defualtBackgroundSprite;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (parentStorageUI.GetParent().IsSlotUISelectable)
            {
                // Original Item Slot UI
                if (parentStorageUI.GetParent().GetSelectSlot() == this)
                {
                    itemFrameImage.sprite = deselectUiSprite;
                    parentStorageUI.GetParent().DeSelectSlot();
                }
                else
                {
                    parentStorageUI.GetParent().SetSelectSlot(this);
                }
            }
        }
    
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (parentStorageUI.GetParent().IsSlotUISelectable)
            {
                var newInfo = Instantiate(infoPrefab, Vector3.zero, Quaternion.identity);
                informationUI = newInfo.gameObject.GetComponent<StorageInformationUI>();
                informationUI.InitializeInformation(item.itemName, item.description, item.itemIcon);
                informationUI.transform.SetParent(storageInfoTransform.transform);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            informationUI.Clear();
        }
    }
}