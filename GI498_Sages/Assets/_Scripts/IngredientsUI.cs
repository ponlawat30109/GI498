using System.Collections;
using System.Collections.Generic;
using _Scripts.InventorySystem.UI.NPCOrder;
using _Scripts.ManagerCollection;
using UnityEngine;

public class IngredientsUI : MonoBehaviour
{
    public PlayerInput _playerInput;
    public GameObject panel;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }
    
    void Update()
    {
        if (_playerInput.UI.OpenIngredientsPanel.triggered)
        {
            Manager.Instance.npcOrderUI.OpenUI();
        }
        
        
        if (Manager.Instance.npcOrderUI != null)
        {
            if (Manager.Instance.npcOrderUI.IsTakeOrder() == false)
            {
                panel.gameObject.SetActive(false);
            }
            else
            {
                panel.gameObject.SetActive(true);
            }
        }
    }
    
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    
}
