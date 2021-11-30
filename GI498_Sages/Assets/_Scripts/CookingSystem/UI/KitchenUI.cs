using System.Collections.Generic;
using _Scripts.InventorySystem.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.CookingSystem.UI
{
    public class KitchenUI : MonoBehaviour
    {
        [SerializeField] private Kitchen parent;
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private Transform storageSlot;
        [SerializeField] private Transform storageSlotInformation;
        
        public List<IngredientSlotUI> slotList = new List<IngredientSlotUI>();
        
        [Header("Recipe Component")] 
        [SerializeField] private FoodObject recipeItem;
        [SerializeField] private Image recipeImage;
        [SerializeField] private Sprite defaultIcon;
        [SerializeField] private TMP_Text recipeNameText;
        [SerializeField] private TMP_Text recipeDescText;
        [SerializeField] private TMP_Text recipeStatusText;
        [Header("Ingredient Info")]
        [SerializeField] private GameObject ingredientInfoComponentPrefab;
        [SerializeField] private Transform recipeIngredientInfoContainer;

        [SerializeField] private List<IngredientInfoComponent> ingredientInfoList = new List<IngredientInfoComponent>();
        
        [SerializeField] private float currentTime = 0;
        private float refreshInterval = .2f;
        private bool _isSomeChange;
        private FoodObject _tempItem;

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        void Start()
        {
            _tempItem = null;
            _isSomeChange = false;
            CreateIngredientSlotUI();
        }
        
        void Update()
        {
            if (currentTime < refreshInterval)
            {
                if (currentTime >= refreshInterval)
                {
                    UpdateUI();
                    currentTime = 0;
                }
                
                currentTime += Time.deltaTime;
            }
            else
            {
                if (currentTime >= refreshInterval)
                {
                    UpdateUI();
                    currentTime = 0;
                }
            }

            if (_tempItem != recipeItem)
            {
                _isSomeChange = true;
                _tempItem = recipeItem;
            }
        }

        public void InitRecipe(FoodObject item)
        {
            recipeItem = item;
            _isSomeChange = true;
            UpdateUI();
        }
        
        public void UpdateUI()
        {
            if (_isSomeChange)
            {
                // Update
                if (recipeItem != null)
                {
                    UpdateRecipeUI();
                }
                else
                {
                    SetDefualtRecipeUI();
                }
            
                CreateIngredientSlotUI(); // Re-create
            }
        }

        public void UpdateRecipeUI()
        {
            recipeImage.sprite = recipeItem.itemIcon;
            recipeNameText.text = recipeItem.itemName;
            recipeDescText.text = recipeItem.description;
            
            if (recipeItem.isCooked)
            {
                recipeStatusText.text = "(Cooked)";
            }
            else
            {
                recipeStatusText.text = "(Need to process)";
            }

            CreateIngredientInfoList();
        }

        public void SetDefualtRecipeUI()
        {
            recipeImage.sprite = defaultIcon;
            recipeNameText.text = "???";
            recipeDescText.text = "Please Add Recipe First";
            
            if (recipeItem.isCooked)
            {
                recipeStatusText.text = "(???)";
            }
            else
            {
                recipeStatusText.text = "(???)";
            }

            RemoveIngredientInfoList();
        }

        void CreateIngredientInfoList()
        {
            if (ingredientInfoList.Count > 0)
            {
                RemoveIngredientInfoList();
            }
            
            foreach (var ingredient in recipeItem.ingredients)
            {
                var newComponentInfo = Instantiate(ingredientInfoComponentPrefab,recipeIngredientInfoContainer);
                var newCastComponent = newComponentInfo.GetComponent<IngredientInfoComponent>();
                newCastComponent.InitComponent(ingredient.itemIcon,ingredient.itemName);
                ingredientInfoList.Add(newCastComponent);
            }
        }
        
        void RemoveIngredientInfoList()
        {
            foreach (var component in ingredientInfoList)
            {
                Destroy(component.gameObject);
            }
            
            ingredientInfoList.Clear();
        }
    
        public Kitchen GetParent()
        {
            return parent;
        }

        public void RemoveSlotByItem(IngredientObject targetItem)
        {
            var index = 1;
            slotList.RemoveAt(index);
        }

        public Transform GetStorageSlotTransform()
        {
            return storageSlot;
        }

        public void CreateIngredientSlotUI()
        {
            /*var slot = parent.GetStorageObject().GetStorageSlot();

            if (slotList.Count > 0)
            {
                for (int i = 0; i < slotList.Count; i++)
                {
                    Destroy(slotList[i].gameObject);
                    slotList.Clear();
                }
            }
            
            for (int i = 0; i < slot.Count; i++)
            {
                if (slot[i].quantity > 0)
                {
                    var newSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
                    var componentSlot = newSlot.gameObject.GetComponent<IngredientSlotUI>();
                    componentSlot.InitializeItem(slot[i].item, this, storageSlotInformation,
                        parent.IsOneOfIngredient(slot[i].item));
                    slotList.Add(componentSlot);
                }
                else
                {
                    parent.GetStorageObject().GetStorageSlot().RemoveAt(i);
                }
            }*/

            var storageSlots = parent.GetStorageObject().GetStorageSlot();

            if (storageSlots.Count > 0) // If have ingredient add.
            {
                for (int i = 0; i < storageSlots.Count; i++)
                {
                    if (CheckSlotList(storageSlots[i].item) == false) //If item not same in slotList
                    {
                        var newSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
                        var componentSlot = newSlot.gameObject.GetComponent<IngredientSlotUI>(); 
                        componentSlot.InitializeItem(storageSlots[i].item, this, storageSlotInformation,
                            parent.IsOneOfIngredient(storageSlots[i].item));
                
                        slotList.Add(componentSlot);
                    }
                }
            }
        }

        public bool CheckSlotList(IngredientObject itemToCheck)
        {
            var result = false;
            
            foreach (var slot in slotList)
            {
                // If slot.item == ingredient in storage
                if (slot.GetItem() == itemToCheck)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
