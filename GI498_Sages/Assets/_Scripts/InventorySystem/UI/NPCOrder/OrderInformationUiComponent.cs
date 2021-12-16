using System;
using System.Collections.Generic;
using _Scripts.CookingSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.InventorySystem.UI.NPCOrder
{
    public class OrderInformationUiComponent : MonoBehaviour
    {
        [SerializeField] private Image orderImage;
        [SerializeField] private TMP_Text foodNameText;
        [SerializeField] private TMP_Text foodDescriptionText;
        [SerializeField] private GameObject ingredientInfoPrefab;
        [SerializeField] private Transform ingredientListContainer;

        [SerializeField] private List<IngredientInfoComponent> ingredientInfoComponentsList = new List<IngredientInfoComponent>();

        public void InitComponent(FoodObject orderRecipe)
        {
            orderImage.sprite = orderRecipe.itemIcon;
            foodNameText.text = orderRecipe.itemName;
            foodDescriptionText.text = orderRecipe.description;

            CreateIngredientInfoComponentList(orderRecipe);
        }

        public void CreateIngredientInfoComponentList(FoodObject orderRecipe)
        {
            if (orderRecipe.ingredients.Count > 0)
            {
                foreach (var ingredient in orderRecipe.ingredients)
                {
                    if (ingredient != null)
                    {
                        var newObj = Instantiate(ingredientInfoPrefab, ingredientListContainer);
                        var newComponent = newObj.GetComponent<IngredientInfoComponent>();
                        newComponent.InitComponent(ingredient.itemIcon,ingredient.itemName);

                        ingredientInfoComponentsList.Add(newComponent);
                    }
                }
            }
            
            if (orderRecipe.specialIngredients.Count > 0)
            {
                foreach (var ingredient in orderRecipe.specialIngredients)
                {
                    if (ingredient != null)
                    {
                        var newObj = Instantiate(ingredientInfoPrefab, ingredientListContainer);
                        var newComponent = newObj.GetComponent<IngredientInfoComponent>();
                        newComponent.InitComponent(ingredient.itemIcon,ingredient.itemName);
                        
                        ingredientInfoComponentsList.Add(newComponent);
                    }
                }
            }
        }

        private void ClearComponent()
        {
            foreach (var component in ingredientInfoComponentsList)
            {
                Destroy(component.gameObject);
            }
            
            ingredientInfoComponentsList.Clear();
        }
        
        public void OnDisable()
        {
            if (ingredientInfoComponentsList.Count > 0)
            {
                ClearComponent();
            }
            
        }

        private void OnApplicationQuit()
        {
            if (ingredientInfoComponentsList.Count > 0)
            {
                ClearComponent();
            }
        }
    }
}
