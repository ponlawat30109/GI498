using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUI : MonoBehaviour
{
    [SerializeField] private Container parent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform containerSlotInformation;

    [SerializeField] private Button takeInButton;
    [SerializeField] private Button takeOutButton;
    [SerializeField] private Button closeButton;
    
    public List<ItemSlotUI> SlotList = new List<ItemSlotUI>();
    
    
    void Start()
    {
        SlotList.Clear();
        CreateUI();

        takeInButton.onClick.AddListener(TakeInButtonAcion);
        takeOutButton.onClick.AddListener(TakeOutButtonAction);
        closeButton.onClick.AddListener(CloseButtonAction);
    }

    public Container GetParent()
    {
        return parent;
    }

    public void TakeInButtonAcion()
    {
        var otherContainer = parent.GetInventory();
        var playerContainer = Manager.Instance.playerManager.inventory;
        var item = parent.GetSelectSlot().item;

        if (item != null)
        {
            parent.TakeIn(otherContainer, playerContainer, item);
        }
    }

    public void TakeOutButtonAction()
    {
        var otherContainer = parent.GetInventory();
        var playerContainer = Manager.Instance.playerManager.inventory;
        var item = parent.GetSelectSlot().item;

        if (item != null)
        {
            parent.TakeOut(playerContainer, otherContainer, item);
        }
    }
    
    public void CloseButtonAction()
    {
        parent.CloseUI();
    }
    
    public void CreateUI()
    {
        var slot = parent.GetInventory().Container;
        
        for (int i = 0; i < slot.Count; i++)
        {
            var newSlot = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
            var componentSlot = newSlot.gameObject.GetComponent<ItemSlotUI>();
            componentSlot.InitializeItem(slot[i].item,this,containerSlotInformation);
            SlotList.Add(componentSlot);
        }
    }
}
