using System;
using System.Collections.Generic;
using _Scripts.CookingSystem;
using _Scripts.InventorySystem.Interface;
using _Scripts.InventorySystem.ScriptableObjects.Storage;
using _Scripts.ManagerCollection;
using UnityEngine;

namespace _Scripts.InventorySystem.Player
{
    public class PlayerStorageHandler : MonoBehaviour
    {
        [Header("Input Key")]
        [SerializeField] private KeyCode openKitchenKey;
        [SerializeField] private KeyCode openStorageKey;
        [SerializeField] private KeyCode collectItemKey;
        
        [Header("Data")]
        public Storage storage; // Player Storage
        public ItemObject currentHoldItemObject;
        public FoodObject currentHoldFoodObject;
        
        [Header("Model")]
        public Transform holdingPosition; // Position of Model On Player Hand
        public GameObject currentHoldItemModel; // Model to Show On Player Hand
        [SerializeField] private int childCount;
        
        [Header("Storage Object")]
        public Storage toInteractStorageObject;
        [SerializeField] private Storage _nearestStorageObject;
        [SerializeField] private List<Storage> _collideStorageObjects = new List<Storage>();
        
        [Header("Storage Object")]
        public Kitchen toInteractKitchenObject;
        [SerializeField] private Kitchen _nearestKitchenObject;
        [SerializeField] private List<Kitchen> _collideKitchenObjects = new List<Kitchen>();
        
        [Header("Item Object")]
        public MiniStorage toInteractItemObject;
        [SerializeField] private MiniStorage _nearestItemObject;
        [SerializeField] private List<MiniStorage> _collideItemObjects = new List<MiniStorage>();

        private bool _canInteractToCollectItem;
        private bool _canInteractToOpenStorage;
        private bool _canInteractToOpenKitchen;

        private bool _justPressCollectItem;
        private float _currentCollectItemTimeCount = 0;
        private float _collectItemInterval = 1;

        private bool _justPressOpenStorage;
        private float _currentOpenStorageTimeCount = 0;
        private readonly float _openStorageInterval = 1.0f;
        
        private bool _justPressOpenKitchen;
        private float _currentOpenKitchenTimeCount = 0;
        private readonly float _openKitchenInterval = 1.0f;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private void Start()
        {
            storage = Manager.Instance.storageManager.GetStorageByType(StorageObject.StorageTypeEnum.Player);
            
            _justPressCollectItem = false;
            _justPressOpenStorage = false;
        }

        private void Update()
        {
            OpenStorageKey();
            OpenKitchenKey();
            ItemCollectKey();
            ClearNull();

            if (currentHoldItemObject == null)
            {
                if (storage.GetStorageObject().GetSlotCount() > 0)
                {
                    if (storage.GetStorageObject().IsSlotIndexHasItem(0))
                    {
                        SetHoldingItem();
                    }
                }
            }

            // Model Things
            if (currentHoldItemObject != null && currentHoldItemModel.transform.childCount < 1)
            {
                SetModel();
            }
            
            /*
            if(currentHoldItemObject == null || storage.GetStorageObject().GetSlotCount() < 1 || currentHoldItemModel.transform.childCount > 1)
            */
            if(currentHoldItemObject == null && !storage.GetStorageObject().IsSlotIndexHasItem(0))
            {
                ClearModel();
            }
            
            // Model Things but food!.
            
            if (currentHoldFoodObject != null && currentHoldItemModel.transform.childCount < 1)
            {
                SetFoodModel();
            }
            
            if(currentHoldFoodObject == null && !storage.GetStorageObject().IsSlotIndexHasItem(0))
            {
                ClearModel();
            }

            childCount = currentHoldItemModel.transform.childCount;
            
            
            // Test Session
            if (Input.GetKeyDown(KeyCode.F1))
            {
                var item = Manager.Instance.storageManager.TakeRecipeByIndex(0);
                JustPutInFood(item);
                Debug.Log($"[F1] Give {item.name} Recipe to player.");
            }
            
            if (Input.GetKeyDown(KeyCode.F2))
            {
                var item = Manager.Instance.storageManager.TakeRecipeByIndex(1);
                JustPutInFood(item);
                Debug.Log($"[F2] Give {item.name} Recipe to player.");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private void OpenStorageKey()
        {
            if (_canInteractToOpenStorage)
            {
                SetToInteractStorage();
                
                if (PlayerController.instance._playerInput.Movement.OpenStorage.triggered)
                {
                    if (toInteractStorageObject != null)
                    {
                        if (toInteractStorageObject.IsUIOpen() == false)
                        {
                            toInteractStorageObject.Interacted();
                        }
                    }
                    
                    _justPressOpenStorage = true;
                    // Debug.Log($"Press {openStorageKey.ToString()} Key.");
                }
            }
            else
            {
                if (PlayerController.instance._playerInput.Movement.OpenStorage.triggered)
                {
                    if (toInteractStorageObject != null)
                    {
                        if (toInteractStorageObject.IsUIOpen())
                        {
                            toInteractStorageObject.CloseUI();
                        }
                    }

                    Debug.Log($"Can not Press {openStorageKey.ToString()} Key for now.");
                    /*Debug.Log($"Open again time:{currentOpenStorageTimeCount.ToString()}");
                    Debug.Log($"_justPressOpenStorage : {_justPressOpenStorage.ToString()}");
                    Debug.Log($"canInteractToOpenStorage : {canInteractToOpenStorage.ToString()}");*/
                }
            }

            if (_justPressOpenStorage == true)
            {
                if (_currentOpenStorageTimeCount < _openStorageInterval)
                {
                    _canInteractToOpenStorage = false;
                    _currentOpenStorageTimeCount += Time.deltaTime;
                }
                else if(_currentOpenStorageTimeCount > _openStorageInterval)
                {
                    _currentOpenStorageTimeCount = 0;
                    _justPressOpenStorage = false;
                    Debug.Log($"Set Just Press {openStorageKey.ToString()} Key to False");
                }
            }
            else
            {
                _canInteractToOpenStorage = true;
            }
        }

        private void OpenKitchenKey()
        {
            if (_canInteractToOpenKitchen)
            {
                SetToInteractKitchen();

                if (PlayerController.instance._playerInput.Movement.OpenKitchen.triggered)
                {
                    if (toInteractKitchenObject != null)
                    {
                        if (toInteractKitchenObject.IsUIOpen() == false)
                        {
                            toInteractKitchenObject.Interacted();
                        }
                    }

                    _justPressOpenKitchen = true;
                    // Debug.Log($"Press {openKitchenKey.ToString()} Key.");
                }
            }
            else
            {
                if (PlayerController.instance._playerInput.Movement.OpenKitchen.triggered)
                {
                    if (toInteractKitchenObject != null)
                    {
                        if (toInteractKitchenObject.IsUIOpen())
                        {
                            toInteractKitchenObject.CloseUI();
                        }
                    }

                    Debug.Log($"Can not Press {openKitchenKey.ToString()} Key for now.");
                }
            }

            if (_justPressOpenKitchen == true)
            {
                if (_currentOpenKitchenTimeCount < _openKitchenInterval)
                {
                    _canInteractToOpenKitchen = false;
                    _currentOpenKitchenTimeCount += Time.deltaTime;
                }
                else if(_currentOpenKitchenTimeCount > _openKitchenInterval)
                {
                    _currentOpenKitchenTimeCount = 0;
                    _justPressOpenKitchen = false;
                    Debug.Log($"Set Just Press {openKitchenKey.ToString()} Key to False");
                }
            }
            else
            {
                _canInteractToOpenKitchen = true;
            }
        }
        
        private void ItemCollectKey()
        {
            if (_canInteractToCollectItem)
            {
                SetToInteractItem();
                
                if (PlayerController.instance._playerInput.Movement.PickItem.triggered)
                {
                    toInteractItemObject.Interacted();
                    Debug.Log($"Press {collectItemKey.ToString()} Key.");
                    
                    _justPressCollectItem = true;
                    
                }
            }
            else
            {
                if (Input.GetKeyDown(collectItemKey))
                {
                    Debug.Log($"Can not Press {collectItemKey.ToString()} Key for now.");
                    /*Debug.Log($"Open again time:{_currentCollectItemTimeCount.ToString()}");
                    Debug.Log($"_justPressOpenStorage : {_justPressCollectItem.ToString()}");
                    Debug.Log($"_canInteractToCollectItem : {_canInteractToCollectItem.ToString()}");*/
                }
            }

            if (_justPressCollectItem == true)
            {
                if (_currentCollectItemTimeCount < _collectItemInterval)
                {
                    _canInteractToCollectItem = false;
                    _currentCollectItemTimeCount += Time.deltaTime;
                }
                else if(_currentCollectItemTimeCount > _collectItemInterval)
                {
                    _currentCollectItemTimeCount = 0;
                    _justPressCollectItem = false;
                    //Debug.Log($"Set Just Press {collectItemKey.ToString()} Key to False");
                }
            }
            else
            {
                _canInteractToCollectItem = true;
            }
        }
        
        /// <summary>
        /// Meaning of return value...
        /// <para>True mean Can</para>
        /// <para>False mean Can not</para>
        /// </summary>
        /// <returns></returns>
        private bool IsCanCollectItem()
        {
            /*if (IsHoldingItem() == false && playerInventory.GetInventory().HasFreeSpace())
            {
                result = true;
            }*/
            
            bool result = IsHoldingItem() == false && storage.GetStorageObject().HasFreeSpace();

            return result;
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        // Put Item Into A storage from B storage
        public void PutIn(StorageObject a, StorageObject b, ItemObject item)
        {
            a.AddItem(item);
            b.RemoveItem(item);
            SetHoldingItem();
        }

        // Take Out Item from A storage and Add to B storage
        public void TakeOut(StorageObject a, StorageObject b, ItemObject item)
        {
            a.RemoveItem(item);
            b.AddItem(item);
            ClearHoldingItem();
        }

        public void JustPutIn(ItemObject item)
        {
            //Debug.Log($"Direct Put {item} in {storage.GetStorageObject().storageType.ToString()}");
            storage.GetStorageObject().AddItem(item);
            SetHoldingItem();
        }
        
        public void JustPutInFood(FoodObject item)
        {
            //Debug.Log($"Direct Put {item} in {storage.GetStorageObject().storageType.ToString()}");
            storage.GetStorageObject().AddItem(item);
            SetHoldingFood(item);
        }

        public void JustTakeOut(ItemObject item)
        {
            Debug.Log($"5 {item.itemName}");
            storage.GetStorageObject().RemoveItem(item);
            ClearHoldingItem();
        }

        public void SetHoldingItem()
        {
            if (IsHoldingItem() == false) // IsHoldingItem return FALSE
            {
                if (currentHoldItemObject == null)
                {
                    if (storage.GetStorageObject().IsSlotIndexHasItem(0))
                    {
                        currentHoldItemObject = storage.GetStorageObject().GetItemFromSlotIndex(0);
                    }
                }
                else
                {
                    Debug.Log("Can not hold more than 1 item");
                }
                
                return;
            }

            //Show Food Model
            //currentHoldItemObject = item;
            
        }
        
        public void SetHoldingFood(FoodObject item)
        {
            if (IsHoldingItem() == false) // IsHoldingItem return FALSE
            {
                if (currentHoldItemObject == null)
                {
                    if (storage.GetStorageObject().IsSlotIndexHasItem(0))
                    {
                        ClearModel();
                        currentHoldItemObject = null;
                        currentHoldFoodObject = item;
                    }
                }
                else
                {
                    Debug.Log("Can not hold more than 1 item");
                }
                
                return;
            }

            //Show Food Model
            //currentHoldItemObject = item;
            
        }

        private void SetModel()
        {
            var newProp = Instantiate(currentHoldItemObject.ingamePrefab, holdingPosition);
            Debug.Log($"Set from Item {currentHoldItemObject.itemName}");
            newProp.transform.SetParent(currentHoldItemModel.transform);
            Debug.Log($"Current Holding Model is Model {newProp.name}");
            //newProp.transform.localPosition = Vector3.zero;
            //newProp.transform.rotation = Quaternion.identity;
        }
        
        private void SetFoodModel()
        {
            if (currentHoldFoodObject.isCooked)
            {
                var newProp = Instantiate(currentHoldFoodObject.cookedPrefab, holdingPosition);
                newProp.transform.SetParent(currentHoldItemModel.transform);
            }
            else
            {
                var newProp = Instantiate(currentHoldFoodObject.ingamePrefab, holdingPosition);
                newProp.transform.SetParent(currentHoldItemModel.transform);
            }
            
            
            //newProp.transform.localPosition = Vector3.zero;
            //newProp.transform.rotation = Quaternion.identity;
        }

        public void ClearHoldingItem()
        {
            if (IsHoldingItem() == true)
            {
                ClearModel();
                currentHoldItemObject = null;
                currentHoldFoodObject = null;
            }
            else
            {
                Debug.LogWarning($"Try to clear Item. But No Item on Player Hand. IsHoldingItem:{IsHoldingItem().ToString()} at ClearHoldingItem method.");
            }
        }
        
        private void ClearModel()
        {
            int childs = currentHoldItemModel.transform.childCount;

            for (int i = 0; i < childs; i++)
            {
                Destroy(currentHoldItemModel.transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// Meaning of return value...
        /// <para>True mean "Some Item" on Player Hand</para>
        /// <para>False mean "No Item" on Player Hand</para>
        /// </summary>
        /// <returns></returns>
        public bool IsHoldingItem()
        {
            bool isHolding = currentHoldItemObject != null || currentHoldFoodObject != null;
            /* full condition
             if (currentHoldItemObject != null || currentHoldFoodObject != null)
            {
                isHolding = true;
            }
             */
            
            // If holdingItem == null mean "No Item" on Player Hand
            // Else holdingItem != null mean There are "Some Item" on Player Hand
            return isHolding;
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void SetToInteractStorage()
        {
            if (_collideStorageObjects.Count > 0)
            {
                _nearestStorageObject = FindNearestStorage(_collideStorageObjects);
                toInteractStorageObject = _nearestStorageObject;
            }
            else
            {
                _nearestStorageObject = null;
                toInteractStorageObject = null;
            }
        }

        private Storage FindNearestStorage(List<Storage> storageList)
        {
            Storage objMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            
            foreach (var t in storageList)
            {
                if (t == null)
                {
                    continue;
                }
                
                float dist = Vector3.Distance(t.transform.position, currentPos);
                
                if (dist < minDist)
                {
                    objMin = t;
                    minDist = dist;
                }
            }

            return objMin;
        }
        
        private void SetToInteractKitchen()
        {
            if (_collideKitchenObjects.Count > 0)
            {
                _nearestKitchenObject = FindNearestKitchen(_collideKitchenObjects);
                toInteractKitchenObject = _nearestKitchenObject;
            }
            else
            {
                _nearestKitchenObject = null;
                toInteractKitchenObject = null;
            }
        }
        
        private Kitchen FindNearestKitchen(List<Kitchen> storageList)
        {
            Kitchen objMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            
            foreach (var t in storageList)
            {
                if (t == null)
                {
                    continue;
                }
                
                float dist = Vector3.Distance(t.transform.position, currentPos);
                
                if (dist < minDist)
                {
                    objMin = t;
                    minDist = dist;
                }
            }

            return objMin;
        }

        private void SetToInteractItem()
        {
            if (_collideItemObjects.Count > 0)
            {
                _nearestItemObject = FindNearestItem(_collideItemObjects);
                toInteractItemObject = _nearestItemObject;
            }
            else
            {
                _nearestItemObject = null;
                toInteractItemObject = null;
            }
        }
        
        private MiniStorage FindNearestItem(List<MiniStorage> itemList)
        {
            MiniStorage objMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = transform.position;
            
            foreach (var t in itemList)
            {
                if (t == null)
                {
                    continue;
                }
                
                float dist = Vector3.Distance(t.transform.position, currentPos);
                
                if (dist < minDist)
                {
                    objMin = t;
                    minDist = dist;
                }
            }

            return objMin;
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private void ClearNull()
        {
            if (toInteractItemObject != null)
            {
                if (_collideItemObjects.Count > 0)
                {
                    var isFound = false;
                    
                    for (int i = 0; i < _collideItemObjects.Count; i++)
                    {
                        if (toInteractItemObject == _collideItemObjects[i])
                        {
                            isFound = true;
                        }
                    }

                    if (isFound == false)
                    {
                        _nearestItemObject = null;
                        toInteractItemObject = null;
                    }
                    else
                    {
                        SetToInteractItem();
                    }
                }
            }
            
            if (toInteractStorageObject != null)
            {
                if (_collideStorageObjects.Count > 0)
                {
                    var isFound = false;
                    
                    for (int i = 0; i < _collideStorageObjects.Count; i++)
                    {
                        if (toInteractStorageObject == _collideStorageObjects[i])
                        {
                            isFound = true;
                        }
                    }

                    if (isFound == false)
                    {
                        _nearestStorageObject = null;
                        toInteractStorageObject = null;
                    }
                    else
                    {
                        SetToInteractStorage();
                    }
                }
            }
            
            if (toInteractKitchenObject != null)
            {
                if (_collideKitchenObjects.Count > 0)
                {
                    var isFound = false;
                    
                    for (int i = 0; i < _collideKitchenObjects.Count; i++)
                    {
                        if (toInteractKitchenObject == _collideKitchenObjects[i])
                        {
                            isFound = true;
                        }
                    }

                    if (isFound == false)
                    {
                        _nearestKitchenObject = null;
                        toInteractKitchenObject = null;
                    }
                    else
                    {
                        SetToInteractKitchen();
                    }
                }
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private bool IsInteractStorageObjectsInList(Storage target)
        {
            foreach (var obj in _collideStorageObjects)
            {
                if (target == obj)
                {
                    return true;
                }
            }

            return false;
        }
        
        private bool IsInteractKitchenObjectsInList(Kitchen target)
        {
            foreach (var obj in _collideStorageObjects)
            {
                if (target == obj)
                {
                    return true;
                }
            }

            return false;
        }
        private bool IsInteractItemObjectsInList(MiniStorage target)
        {
            foreach (var obj in _collideItemObjects)
            {
                if (target == obj)
                {
                    return true;
                }
            }

            return false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MiniStorageObject"))
            {
                var interactForAddToList = other.GetComponent<MiniStorage>();
                
                if (IsInteractItemObjectsInList(interactForAddToList) == false)
                {
                    _collideItemObjects.Add(interactForAddToList);
                    SetToInteractItem();
                }
            }

            if (other.CompareTag("StorageObject"))
            {
                var interactForAddToList = other.GetComponent<Storage>();
                
                if (IsInteractStorageObjectsInList(interactForAddToList) == false)
                {
                    _collideStorageObjects.Add(interactForAddToList);
                    SetToInteractStorage();
                }
            }
            
            if (other.CompareTag("KitchenObject"))
            {
                var interactForAddToList = other.GetComponent<Kitchen>();
                
                if (IsInteractKitchenObjectsInList(interactForAddToList) == false)
                {
                    _collideKitchenObjects.Add(interactForAddToList);
                    SetToInteractKitchen();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("MiniStorageObject"))
            {
                var index = _collideItemObjects.FindIndex(x => x.Equals(other.gameObject.GetComponent<MiniStorage>()));
                _collideItemObjects.RemoveAt(index);
                ClearNull();
                SetToInteractItem();
            }

            if (other.CompareTag("StorageObject"))
            {
                var index = _collideStorageObjects.FindIndex(x => x.Equals(other.gameObject.GetComponent<Storage>()));
                _collideStorageObjects.RemoveAt(index);
                ClearNull();
                SetToInteractKitchen();
            }
            
            if (other.CompareTag("KitchenObject"))
            {
                var index = _collideKitchenObjects.FindIndex(x => x.Equals(other.gameObject.GetComponent<Kitchen>()));
                _collideKitchenObjects.RemoveAt(index);
                ClearNull();
                SetToInteractKitchen();
            }
        }

        private void OnApplicationQuit()
        {
            storage.GetStorageObject().GetStorageSlot().Clear();
            Debug.Log("Clear Player Storage.");
        }
    }
}