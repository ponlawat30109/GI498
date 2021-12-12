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
        [SerializeField] private TMP_Text quantityText;
        
        [SerializeField] private GameObject slotInfoComponentPrefab;
        [SerializeField] private Transform slotInfoParentTransform;
        [SerializeField] private StorageInformationUI informationUI;
        [SerializeField] private List<IngredientSlotInfoComponent> currentHoverComponentList = new List<IngredientSlotInfoComponent>();

        private bool _isOneOfIngredientRecipe = false;
        [SerializeField] private int _currentQuantity;
        
        public void InitializeItem(IngredientObject toInitItem,KitchenUI parentStorage,Transform transformStorageInfo,bool _isOneOfIngredientRecipe,int quantity)
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
            else if(_isOneOfIngredientRecipe == false)
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
                /*if (currentHoverComponentList.Count > 0)
                {
                    foreach (var component in currentHoverComponentList)
                    {
                        Destroy(component);
                    }
                    
                    currentHoverComponentList.Clear();
                }*/

                /*if (parentStorageUI.GetParent().GetSelectSlot() == this)
                {
                    var newInfo = Instantiate(slotInfoComponentPrefab, slotInfoParentTransform);
                var newComponent = newInfo.gameObject.GetComponent<IngredientSlotInfoComponent>();
                newComponent.Init(item.itemName,GetStringNutrion());
                
                currentHoverComponentList.Add(newComponent);
                }
                */

                // slotInfoComponentPrefab
                // slotInfoParentTransform
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //informationUI.Clear();

            /*foreach (var component in currentHoverComponentList)
            {
                Destroy(component);
            }
            
            currentHoverComponentList.Clear();*/
        }

        /*public String GetStringNutrion()
        {
            var result = "";
            
            var i = item.nutrition;

            if(i.cholesterol> 0){result += $"{nameof(i.cholesterol)} {i.cholesterol} mg\n";}
            if(i.carbohydrate> 0){result += $"{nameof(i.carbohydrate)} {i.carbohydrate} mg\n";}
            if(i.sugars> 0){result += $"{nameof(i.sugars)} {i.sugars} mg\n";}
            if(i.fiber> 0){result += $"{nameof(i.fiber)} {i.fiber} mg\n";}
            if(i.proteins> 0){result += $"{nameof(i.proteins)} {i.proteins} mg\n";}
            if(i.fat> 0){result += $"{nameof(i.fat)} {i.fat} mg\n";}
            if(i.saturatedfat> 0){result += $"{nameof(i.saturatedfat)} {i.saturatedfat} mg\n";}
            if(i.water> 0){result += $"{nameof(i.water)} {i.water} mg\n";}
            if(i.potassium> 0){result += $"{nameof(i.potassium)} {i.potassium} mg\n";}
            if(i.sodium> 0){result += $"{nameof(i.sodium)} {i.sodium} mg\n";}
            if(i.calcium> 0){result += $"{nameof(i.calcium)} {i.calcium} mg\n";}
            if(i.phosphorus> 0){result += $"{nameof(i.phosphorus)} {i.phosphorus} mg\n";}
            if(i.magnesium> 0){result += $"{nameof(i.magnesium)} {i.magnesium} mg\n";}
            if(i.zinc> 0){result += $"{nameof(i.zinc)} {i.zinc} mg\n";}
            if(i.iron> 0){result += $"{nameof(i.iron)} {i.iron} mg\n";}
            if(i.manganese> 0){result += $"{nameof(i.manganese)} {i.manganese} mg\n";}
            if(i.copper> 0){result += $"{nameof(i.copper)} {i.copper} mg\n";}
            if(i.selenium> 0){result += $"{nameof(i.selenium)} {i.selenium} mg\n";}
            if(i.vitaminB1> 0){result += $"{nameof(i.vitaminB1)} {i.vitaminB1} mg\n";}
            if(i.vitaminB2> 0){result += $"{nameof(i.vitaminB2)} {i.vitaminB2} mg\n";}
            if(i.vitaminB3> 0){result += $"{nameof(i.vitaminB3)} {i.vitaminB3} mg\n";}
            if(i.vitaminB5> 0){result += $"{nameof(i.vitaminB5)} {i.vitaminB5} mg\n";}
            if(i.vitaminB6> 0){result += $"{nameof(i.vitaminB6)} {i.vitaminB6} mg\n";}
            if(i.vitaminB7> 0){result += $"{nameof(i.vitaminB7)} {i.vitaminB7} mg\n";}
            if(i.vitaminB9> 0){result += $"{nameof(i.vitaminB9)} {i.vitaminB9} mg\n";}
            if(i.vitaminB12> 0){result += $"{nameof(i.vitaminB12)} {i.vitaminB12} mg\n";}
            if(i.vitaminC> 0){result += $"{nameof(i.vitaminC)} {i.vitaminC} mg\n";}
            if(i.vitaminA> 0){result += $"{nameof(i.vitaminA)} {i.vitaminA} mg\n";}
            if(i.vitaminD> 0){result += $"{nameof(i.vitaminD)} {i.vitaminD} mg\n";}
            if(i.vitaminE> 0){result += $"{nameof(i.vitaminE)} {i.vitaminE} mg\n";}
            if(i.vitaminK> 0){result += $"{nameof(i.vitaminK)} {i.vitaminK} mg\n";}

            return result;
        }*/
    }
}