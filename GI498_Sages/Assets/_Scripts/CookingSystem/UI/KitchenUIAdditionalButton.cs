using System.Collections;
using System.Collections.Generic;
using _Scripts.CookingSystem;
using _Scripts.ManagerCollection;
using UnityEngine;
using UnityEngine.UI;

public class KitchenUIAdditionalButton : MonoBehaviour
{
    [SerializeField] private Kitchen parent;
    [SerializeField] private Button putInButton;
    [SerializeField] private Button takeOutButton;
    [SerializeField] private Button closeButton;
    
    public void Start()
    {
        putInButton.onClick.AddListener(PutInButtonAction);
        takeOutButton.onClick.AddListener(TakeOutButtonAction);
        closeButton.onClick.AddListener(CloseButtonAction);
    }
        
    // Storage Interact Method
    // Put Item Into A storage from B storage
    // A = this Storage Object (parent)
    // B = player Storage Object (player)
    public void PutInButtonAction()
    {
        var psHandler = Manager.Instance.playerManager.PSHandler();
        //var a = parent;
        //var b = psHandler.storage.GetStorageObject();
        var item = (IngredientObject) psHandler.currentHoldItemObject;

        if (psHandler.IsHoldingItem() && parent.GetStorageObject().HasFreeSpace())
        {
            // If Holding Ingredient
            if (psHandler.currentHoldFoodObject == null && psHandler.currentHoldItemObject != null)
            {
                parent.GetStorageObject().PutIn(item);
                parent.CheckWhenIngredientAdd(item);
                
                //
                //parent.GetStorageObject().GetSlotByItem(item);
            }
            else
            {
                Manager.Instance.notifyManager.CreateNotify("Ewww!", "It's not ingredient!");
            }
        }
    }

    // Take Out Item from A storage and Add to B storage
    // A = this Storage Object (parent)
    // B = player Storage Object (player)
    public void TakeOutButtonAction()
    {
        var psHandler = Manager.Instance.playerManager.PSHandler();
        
        if (psHandler.storage.GetStorageObject().HasFreeSpace() && psHandler.IsHoldingItem() == false) // Player Have Free Space
        {
            /*if (parent.IsTrashFull() == false) // Trash Is Not Full
            {
                // Remove Item from this Storage
                // parent.RemoveAtSelectSlot();
            }
            else
            {
                parent.GrabTrash();
            }*/

            if (parent.GetStorageObject().GetSlotCount() > 0)
            {
                parent.GrabTrash();
            }
            else
            {
                Manager.Instance.notifyManager.CreateNotify("Ops..", "Nothing to throw...");
            }

        }
        else
        {
            // Hand is full
        }
    }
    
    public void CloseButtonAction()
    {
        parent.CloseUI();
    }
}
