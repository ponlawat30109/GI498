using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private GameObject infoPrefab;
    
    [SerializeField] public ItemObject item;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image itemFrameImage;
    [SerializeField] private Sprite selectUiSlot;
    [SerializeField] private Sprite deselectUiSlot;
    
    [SerializeField] private ContainerUI parentContainerUI;
    [SerializeField] private Transform containerInfo;

    [SerializeField] private ContainerInformationUI informationUI;
    
    public void InitializeItem(ItemObject toInitItem,ContainerUI _parentContainer,Transform transformContainerInfo)
    {
        item = toInitItem;
        itemImage.sprite = toInitItem.itemIcon;
        
        parentContainerUI = _parentContainer;
        containerInfo = transformContainerInfo;

        transform.SetParent(parentContainerUI.transform);
    }

    private void Update()
    {
        if (parentContainerUI.GetParent().GetSelectSlot() == this)
        {
            itemFrameImage.sprite = selectUiSlot;
        }
        else
        {
            itemFrameImage.sprite = deselectUiSlot;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        parentContainerUI.GetParent().SetSelectSlot(this);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        var newInfo = Instantiate(infoPrefab, Vector3.zero, Quaternion.identity);
        informationUI = newInfo.gameObject.GetComponent<ContainerInformationUI>();
        informationUI.InitializeInformation(item.itemName, item.description, item.itemIcon);
        informationUI.transform.SetParent(containerInfo.transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        informationUI.Clear();
    }

   
}
