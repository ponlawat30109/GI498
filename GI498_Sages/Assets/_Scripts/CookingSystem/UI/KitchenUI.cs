using System;
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
        //private bool _isSomeChange;
        //private FoodObject _tempItem;

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        void Start()
        {
            //_tempItem = null;
            //_isSomeChange = false;
            CreateIngredientSlotUI();
        }
        
        void Update()
        {
            if (currentTime < refreshInterval)
            {
                currentTime += Time.deltaTime;
            }
            else if(currentTime >= refreshInterval)
            {
                UpdateUI();
                currentTime = 0;
            }

            if (recipeItem == null)
            {
                foreach (var slot in slotList)
                {
                    Destroy(slot.gameObject);
                }
                
                slotList.Clear();
            }
        }

        public void InitRecipe(FoodObject item)
        {
            recipeItem = item;
           
            UpdateUI();
        }
        
        public void UpdateUI()
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

            for (int i = 0; i < recipeItem.ingredients.Count; i++)
            {
                if (recipeItem.ingredients[i].ingredientObject != null)
                {
                    var newGameObject = Instantiate(ingredientInfoComponentPrefab, recipeIngredientInfoContainer);
                    var newInfo = newGameObject.GetComponent<IngredientInfoComponent>();
                    newInfo.InitComponent(recipeItem.ingredients[i].ingredientObject.itemIcon, recipeItem.ingredients[i].ingredientObject.itemName);
                    
                    ingredientInfoList.Add(newInfo);
                }
                else
                {
                    Debug.Log($"[{recipeItem.itemName}] missing {recipeItem.ingredients[i]}.");
                }
            }
            
            // Special Ingredient
            for (int i = 0; i < recipeItem.specialIngredients.Count; i++)
            {
                if (recipeItem.specialIngredients[i].ingredientObject != null)
                {
                    var newGameObject = Instantiate(ingredientInfoComponentPrefab, recipeIngredientInfoContainer);
                    var newInfo = newGameObject.GetComponent<IngredientInfoComponent>();
                    newInfo.InitComponent(recipeItem.specialIngredients[i].ingredientObject.itemIcon, recipeItem.specialIngredients[i].ingredientObject.itemName);
                    
                    ingredientInfoList.Add(newInfo);
                }
                else
                {
                    Debug.Log($"[{recipeItem.itemName}] missing {recipeItem.specialIngredients[i]}.");
                }
            }
            
            /*foreach (var ingredient in recipeItem.ingredients)
            {
                var newComponentInfo = Instantiate(ingredientInfoComponentPrefab, recipeIngredientInfoContainer);
                var newCastComponent = newComponentInfo.GetComponent<IngredientInfoComponent>();
                newCastComponent.InitComponent(ingredient.itemIcon, ingredient.itemName);
                ingredientInfoList.Add(newCastComponent);
            }*/
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
            var storageSlots = parent.GetStorageObject().GetStorageSlot();

            if (slotList.Count > 0)
            {
                ClearSlotList();
            }
            
            if (storageSlots.Count > 0) // If have ingredient add.
            {
                for (int i = 0; i < storageSlots.Count; i++)
                {
                    if (CheckSlotList(storageSlots[i].item) == false) //If item not same in slotList
                    {
                        var newSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
                        var componentSlot = newSlot.gameObject.GetComponent<IngredientSlotUI>(); 
                        componentSlot.InitializeItem(storageSlots[i].item
                            , this
                            , storageSlotInformation,
                            parent.IsOneOfIngredient(storageSlots[i].item)
                            ,storageSlots[i].quantity);
                
                        slotList.Add(componentSlot);
                    }
                }
            }
        }

        public void ClearSlotList()
        {
            foreach (var slot in slotList)
            {
                Destroy(slot.gameObject);
            }
                            
            slotList.Clear();
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

        private void OnApplicationQuit()
        {
            if (ingredientInfoList.Count > 0)
            {
                RemoveIngredientInfoList();
            }

            if (slotList.Count > 0)
            {
                slotList.Clear();
            }
            
            recipeItem = null;
        }
    }
}
