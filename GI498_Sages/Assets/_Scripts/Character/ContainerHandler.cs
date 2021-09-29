using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Interact_System.Interface;
using _Scripts.InteractSystem.Interface;
using _Scripts.Inventory_System;
using _Scripts.InventorySystem;
using UnityEditor;
using UnityEngine;

//For player or other storage object;
public class ContainerHandler : MonoBehaviour
{

    public Transform holdingPosition;
    public GameObject currentHolding;
    public ItemObject holdingItem;
    
    [SerializeField] private Item closestCollectableItem;
    [SerializeField] private Container closestInteractableItem;
        
    public List<Item> collectableObjects = new List<Item>();
    public List<Container> interactableObjects = new List<Container>();

    private float interval = 1;
    private float currentTime = 0;

    private void Update()
    {
        if (currentTime < interval)
        {
            if (currentTime >= interval)
            {
                ClearNull();
                currentTime = 0;
            }
            
            currentTime += Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            FindClosestCollectableItem(collectableObjects);
            if (closestCollectableItem != null)
            {
                var target = closestCollectableItem.gameObject.GetComponent<ICollectableObject>();
                var inventory = Manager.Instance.playerManager.inventory;
                target?.CollectTo(inventory);
                SetHoldingItem(closestCollectableItem);
                Destroy(closestCollectableItem.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FindClosestInteractableItem(interactableObjects);
            if (closestInteractableItem != null)
            {
                var target = closestInteractableItem.gameObject.GetComponent<IInteractableObject>();
                target?.Interacted();
            }
        }
    }

    private void SetHoldingItem(Item item)
    {
        if (currentHolding != null)
        {
            Debug.Log("Can not hold more than 1 item");
            return;
            //ClearHoldingItemModel();
        }
        
        //Show Food Model
        holdingItem = item.GetItem();
        currentHolding = Instantiate(holdingItem.ingamePrefab,holdingPosition);
        currentHolding.transform.localPosition = Vector3.zero;
        currentHolding.transform.rotation = Quaternion.identity;
    }

    private void PlaceItem()
    {
        
    }
    
    private void ClearHoldingItemModel()
    {
        currentHolding = null;
    }

    private void FindClosestCollectableItem(List<Item> toFindList)
    {
        Item objMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
            
        foreach (var t in toFindList)
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

        closestCollectableItem = objMin;
    }
    
    private void FindClosestInteractableItem(List<Container> toFindList)
    {
        Container objMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
            
        foreach (var t in toFindList)
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

        closestInteractableItem = objMin;
    }
    
    private bool IsCollectObjectsInList(Item target)
    {
        foreach (var obj in collectableObjects)
        {
            if (target == obj)
            {
                return true;
            }
        }

        return false;
    }
        
    private bool IsInteractObjectsInList(Container target)
    {
        foreach (var obj in interactableObjects)
        {
            if (target == obj)
            {
                return true;
            }
        }

        return false;
    }

    private void ClearNull()
    {
        foreach (var obj in collectableObjects)
        {
            if (obj == null)
            {
                var index = collectableObjects.FindIndex(x => x.Equals(obj));
                collectableObjects.RemoveAt(index);
            }
        }
            
        foreach (var obj in interactableObjects)
        {
            if (obj == null)
            {
                var index = interactableObjects.FindIndex(x => x.Equals(obj));
                interactableObjects.RemoveAt(index);
            }
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Item>())
        {
            var itemForAddToList = other.GetComponent<Item>();
            
            if (IsCollectObjectsInList(itemForAddToList) == false)
            {
                Debug.Log($"Add {itemForAddToList} to List");
                collectableObjects.Add(itemForAddToList);
            }
        }

        if (other.CompareTag("InteractableObject"))
        {
            var interactForAddToList = other.GetComponent<Container>();
            
            if (IsInteractObjectsInList(interactForAddToList) == false)
            {
                Debug.Log($"Add {interactForAddToList} to List");
                interactableObjects.Add(interactForAddToList);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag($"CollectableItem"))
        {
            var index = collectableObjects.FindIndex(x => x.Equals(other.gameObject.GetComponent<Item>()));
            collectableObjects.RemoveAt(index);
        }
            
        if (other.CompareTag($"InteractableObject"))
        {
            var index = interactableObjects.FindIndex(x => x.Equals(other.gameObject.GetComponent<Container>()));
            interactableObjects.RemoveAt(index);
        }
    }

    private void OnApplicationQuit()
    {
        // Save or somethings...
        var playerInventory = Manager.Instance.playerManager.inventory;
        
        if (playerInventory.Container != null)
        {
            playerInventory.Container.Clear();
        }
        
    }
}
