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
        [SerializeField] private FoodObject stoveTemp;
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
        private float refreshInterval = .4f;
        //private bool _isSomeChange;
        private FoodObject _tempRecipeItem;

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
            else if (currentTime >= refreshInterval)
            {
                UpdateUI();
                RemoveEmptySlot();
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

        public void InitRecipe(FoodObject item, FoodObject stoveTemp)
        {
            recipeItem = item;
            this.stoveTemp = stoveTemp;

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

            if (stoveTemp != null)
            {
                CreateIngredientSlotUI();
            }
            else
            {
                UpdateIngredientSlotUI();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// 
        public Kitchen GetParent()
        {
            return parent;
        }

        public Transform GetStorageSlotTransform()
        {
            return storageSlot;
        }

        // For Recipe Informationn
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

            if (recipeItem != _tempRecipeItem)
            {
                CreateIngredientInfoList();
            }
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
                if (recipeItem.ingredients[i] != null)
                {
                    var newGameObject = Instantiate(ingredientInfoComponentPrefab, recipeIngredientInfoContainer);
                    var newInfo = newGameObject.GetComponent<IngredientInfoComponent>();
                    newInfo.InitComponent(recipeItem.ingredients[i].itemIcon, recipeItem.ingredients[i].itemName);

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
                if (recipeItem.specialIngredients[i] != null)
                {
                    var newGameObject = Instantiate(ingredientInfoComponentPrefab, recipeIngredientInfoContainer);
                    var newInfo = newGameObject.GetComponent<IngredientInfoComponent>();
                    newInfo.InitComponent(recipeItem.specialIngredients[i].itemIcon, recipeItem.specialIngredients[i].itemName);

                    ingredientInfoList.Add(newInfo);
                }
                else
                {
                    Debug.Log($"[{recipeItem.itemName}] missing {recipeItem.specialIngredients[i]}.");
                }
            }

            _tempRecipeItem = recipeItem;

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


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        // For Stove Slot UI

        public void CreateIngredientSlotUI()
        {
            if (stoveTemp.ingredients.Count > 0)
            {
                for (int i = 0; i < stoveTemp.ingredients.Count; i++)
                {
                    //If item not in slotList -> Create new one
                    if (IsInSlotList(stoveTemp.ingredients[i]) == false)
                    {
                        var newSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
                        var componentSlot = newSlot.gameObject.GetComponent<IngredientSlotUI>();

                        componentSlot.InitializeItem(
                            stoveTemp.ingredients[i]
                            , this
                            , storageSlotInformation,
                            parent.IsOneOfIngredient(stoveTemp.ingredients[i])
                            , stoveTemp.ingredients[i].quantity);

                        componentSlot.transform.localScale = Vector3.one;
                        slotList.Add(componentSlot);
                    }
                    // Update
                    else
                    {
                        UpdateIngredientSlotUI();
                    }
                }
            }
            
            if (stoveTemp.specialIngredients.Count > 0)
            {
                for (int i = 0; i < stoveTemp.specialIngredients.Count; i++)
                {
                    //If item not in slotList -> Create new one
                    if (IsInSlotList(stoveTemp.specialIngredients[i]) == false)
                    {
                        var newSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
                        var componentSlot = newSlot.gameObject.GetComponent<IngredientSlotUI>();

                        componentSlot.InitializeItem(
                            stoveTemp.specialIngredients[i]
                            , this
                            , storageSlotInformation,
                            parent.IsOneOfIngredient(stoveTemp.specialIngredients[i])
                            , stoveTemp.specialIngredients[i].quantity);

                        componentSlot.transform.localScale = Vector3.one;
                        slotList.Add(componentSlot);
                    }
                    // Update
                    else
                    {
                        UpdateIngredientSlotUI();
                    }
                }
            }
        }

        public void UpdateIngredientSlotUI()
        {
            if (slotList.Count > 0)
            {
                for (int i = 0; i < slotList.Count; i++)
                {
                    var item = (IngredientObject)slotList[i].GetItem();

                    if (slotList[i].GetItem() != null && stoveTemp.GetIngredientByName(item) != null)
                    {
                        slotList[i].SetCurrentQuantity(stoveTemp.GetIngredientByName(item).quantity);
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

        public void RemoveEmptySlot()
        {
            if (slotList.Count > 0)
            {
                for (int i = 0; i < slotList.Count; i++)
                {
                    if (slotList[i] == null)
                    {
                        slotList.RemoveAt(i);
                    }
                }
            }
        }

        public bool IsInSlotList(IngredientObject itemToCheck)
        {
            var result = false;

            foreach (var slot in slotList)
            {
                // If slot.item == ingredient to check
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
