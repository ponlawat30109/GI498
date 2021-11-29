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

        [Header("Recipe Component")] 
        [SerializeField] private FoodObject recipeItem;
        [SerializeField] private Image recipeImage;
        [SerializeField] private Sprite defaultIcon;
        [SerializeField] private TMP_Text recipeNameText;
        [SerializeField] private TMP_Text recipeDescText;
        [SerializeField] private TMP_Text recipeStatusText;
        
        public List<IngredientSlotUI> slotList = new List<IngredientSlotUI>();

        private float currentTime = 0;
        private float refreshInterval = 1;

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        void Start()
        {
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
                
                currentTime++;
            }
        }

        public void InitRecipe(FoodObject item)
        {
            recipeItem = item;
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
            
            CreateIngredientSlotUI(); // Re-create -- Implement later...
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
            var slot = parent.GetStorageObject().GetStorageSlot();
        
            for (int i = 0; i < slot.Count; i++)
            {
                if (slot[i].quantity > 0)
                {
                    var newSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
                    var componentSlot = newSlot.gameObject.GetComponent<IngredientSlotUI>();
                    componentSlot.InitializeItem(slot[i].item, 
                        this, // storage
                        storageSlotInformation, //
                        parent.IsOneOfIngredient(slot[i].item)
                        );
                    slotList.Add(componentSlot);
                }
                else
                {
                    parent.GetStorageObject().GetStorageSlot().RemoveAt(i);
                }
            }
        }
    }
}
