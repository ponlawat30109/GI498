using System;
using System.Collections.Generic;
using _Scripts.InventorySystem.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

namespace _Scripts.CookingSystem.UI
{
    public class IngredientSlotUI : MonoBehaviour, IPointerClickHandler
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
        [SerializeField] private TMP_Text quantityText;

        [SerializeField] private GameObject slotInfoComponentPrefab;
        [SerializeField] private Transform slotInfoParentTransform;
        [SerializeField] private StorageInformationUI informationUI;
        [SerializeField] private List<IngredientSlotInfoComponent> currentHoverComponentList = new List<IngredientSlotInfoComponent>();

        private bool _isOneOfIngredientRecipe = false;
        [SerializeField] private int _currentQuantity;

        public void InitializeItem(IngredientObject toInitItem, KitchenUI parentStorage, Transform transformStorageInfo, bool _isOneOfIngredientRecipe, int quantity)
        {
            item = toInitItem;
            itemImage.sprite = toInitItem.itemIcon;
            _currentQuantity = quantity;
            UpdateQuantityText();
            parentStorageUI = parentStorage;
            slotInfoParentTransform = transformStorageInfo;
            this._isOneOfIngredientRecipe = _isOneOfIngredientRecipe;
            transform.SetParent(parentStorageUI.GetStorageSlotTransform());
        }


        public void UpdateQuantityText()
        {
            quantityText.text = _currentQuantity.ToString();
        }

        public void SetCurrentQuantity(int value)
        {
            _currentQuantity = value;
        }

        public int GetCurrentQuantity()
        {
            return _currentQuantity;
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
            else if (_isOneOfIngredientRecipe == false)
            {
                itemBackground.sprite = notCorrectIngredientSprite;
            }
            else
            {
                itemBackground.sprite = defualtBackgroundSprite;
            }

            if (item != null)
            {
                SetCurrentQuantity(item.quantity);
                UpdateQuantityText();
            }

            if (_currentQuantity < 1)
            {
                Destroy(this.gameObject);
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
    }
}