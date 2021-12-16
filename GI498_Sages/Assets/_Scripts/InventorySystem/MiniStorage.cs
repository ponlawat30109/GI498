using System;
using _Scriptable_Object.Items.Scripts;
using _Scripts.InteractSystem.Interface;
using _Scripts.InventorySystem.Player;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.ManagerCollection;
using UnityEngine;

namespace _Scripts.InventorySystem
{
    public class MiniStorage : MonoBehaviour,IInteractableObject
    {
        [Header("Data")]
        public ItemObject currentHoldItemObject;
        public FoodObject currentHoldFoodObject;
        public ToolObject currentHoldToolObject;

        [Header("Model")]
        public Transform holdingPosition; // Position of Model On Player Hand
        public GameObject currentHoldItemModel; // Model to Show On Player Hand
        
        public event Action OnInteracted;

        private void Start()
        {
            ClearHolding();
            ClearModel();
        }

        private void Update()
        {
            if (currentHoldItemModel.transform.childCount < 1)
            {
                if (IsCurrentItemNotNull())
                {
                    SetModel();
                }
            }
            
            if(currentHoldItemModel.transform.childCount > 0)
            {
                if (IsCurrentItemNotNull() == false)
                {
                    ClearModel();
                }
            }
        }

        public void Interacted()
        {
            /* Note
            // - If have Item on Player Hand and This Mini Storage 'Have' Free Space
            // -- Then : Put Item to Mini Storage
            // - Else If have Item on Player Hand and This Mini Storage 'Do not' have Free Space
            // -- Then : Do nothings...
            // - Else If do not have Item on Player Hand and This Mini Storage 'Have' Free Space
            // -- Then : Do nothings...
            // - Else If do not have Item on Player Hand and This Mini Storage 'Do not' have Free Space (have item)
            // -- Then : Take Item from  Mini Storage
            */
            
            var psHandler = Manager.Instance.playerManager.PSHandler();
            //var psStorage = psHandler.storage.GetStorageObject();

            if (psHandler.IsHoldingItem() == true) // Have Item on Player Hand
            {
                // Note
                // True -> Have Item => Don't have free space.
                // False -> Don't have Item => Have free space.
                if (IsCurrentItemNotNull() == false) // Have free space
                {
                    // Put Item to Mini Storage
                    if (psHandler.currentHoldItemModel != null || psHandler.currentHoldFoodObject != null)
                    {
                        switch (psHandler.storage.GetStorageObject().GetItemFromSlotIndex(0).type)
                        {
                            case ItemType.Ingredient:
                            {
                                Debug.Log($"3 {psHandler.currentHoldItemObject.itemName}");
                                PlaceItem(psHandler, psHandler.currentHoldItemObject);
                                break;
                            }

                            case ItemType.Food:
                            {
                                Debug.Log($"3 {psHandler.currentHoldFoodObject.itemName}");
                                PlaceItem(psHandler, psHandler.currentHoldFoodObject);
                                break;
                            }
                            
                            case ItemType.Tool:
                            {
                                Debug.Log($"3 {psHandler.currentHoldItemObject.itemName}");
                                PlaceItem(psHandler, psHandler.currentHoldItemObject);
                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("No Item On Hand Error");
                    }
                }
            }
            else if(psHandler.IsHoldingItem() == false) // 'Do not' have Item on Player Hand
            {
                if (currentHoldItemObject != null || currentHoldFoodObject != null || currentHoldToolObject != null) // 'Do not' have Free Space (have item) *fixed
                {
                    // Take Item from Mini Storage
                    GrabItem(psHandler);
                }
            }
        }

        public void PlaceItem(PlayerStorageHandler ps, ItemObject item)
        {
            if (currentHoldItemObject != null || currentHoldFoodObject != null || currentHoldToolObject != null)
            {
                return;
            }

            if (item.type == ItemType.Ingredient)
            {
                currentHoldItemObject = item;
            }
            else if(item.type == ItemType.Food)
            {
                currentHoldFoodObject = (FoodObject) item;
            }
            else if(item.type == ItemType.Tool)
            {
                currentHoldToolObject = (ToolObject) item;
            }
            
            ps.JustTakeOut(item); // -
        }

        public void GrabItem(PlayerStorageHandler ps)
        {
            if (currentHoldItemObject != null)
            {
                ps.JustPutIn(currentHoldItemObject); // +
            }
            else if (currentHoldFoodObject != null)
            {
                ps.JustPutInFood(currentHoldFoodObject); // +
            }
            else if (currentHoldToolObject != null)
            {
                ps.JustPutIn(currentHoldToolObject); // +
            }

            ClearHolding(); // -
            ClearModel();
        }
        
        //public void SetMiniStorageId(int value)
        //{
        //    miniStorageId = value;
        //}

        /// <summary>
        /// Meaning of return...
        /// <para>True mean currentHoldItemObject is Not Null</para>
        /// <para>False mean currentHoldItemObject is Null</para>
        /// </summary>
        /// <returns></returns>
        public bool IsCurrentItemNotNull()
        {
            return currentHoldItemObject != null || currentHoldFoodObject  != null  || currentHoldToolObject != null ;
        }

        private void SetModel()
        {
            
            if (currentHoldItemObject != null)
            {
                // Set Model
                var newProp = Instantiate(currentHoldItemObject.ingamePrefab, holdingPosition);
                newProp.transform.SetParent(currentHoldItemModel.transform);
                newProp.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (currentHoldFoodObject != null)
            {
                if (currentHoldFoodObject.isCooked == false)
                {
                    // Set Model
                    var newProp = Instantiate(currentHoldFoodObject.ingamePrefab, holdingPosition);
                    newProp.transform.SetParent(currentHoldItemModel.transform);
                    newProp.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    // Set Model
                    var newProp = Instantiate(currentHoldFoodObject.cookedPrefab, holdingPosition);
                    newProp.transform.SetParent(currentHoldItemModel.transform);
                    newProp.transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }
            else if (currentHoldToolObject != null)
            {
                // Set Model
                var newProp = Instantiate(currentHoldToolObject.ingamePrefab, holdingPosition);
                newProp.transform.SetParent(currentHoldItemModel.transform);
                newProp.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                Debug.Log("Unknow what type holding now.");
            }
            
            //newProp.transform.localPosition = Vector3.zero;
            //newProp.transform.rotation = Quaternion.identity;
        }

        public void ClearHolding()
        {
            currentHoldItemObject = null;
            currentHoldFoodObject = null;
            currentHoldToolObject = null;
        }

        public void ClearModel()
        {
            int childs = currentHoldItemModel.transform.childCount;
            
            for (int i = 0; i < childs; i++)
            {
                if (currentHoldItemModel.transform.GetChild(i).gameObject != null)
                {
                    Destroy(currentHoldItemModel.transform.GetChild(i).gameObject);
                }
            }
        }
    }
}