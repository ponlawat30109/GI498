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
            var orderPanel = Manager.Instance.npcOrderUI;

            // ถ้ามี Order อยู่ 
            if (orderPanel.GetOrderInfoComponent().IsCurrentOrderNull() == false)
            {
                //ยังไม่ได้เทคออเดอร์
                if (orderPanel.IsTakeOrder() == false)
                {
                    // UI Panel ถูกปิดไป
                    if (orderPanel.gameObject.activeSelf == false)
                    {
                        panel.gameObject.SetActive(true);
                        _playerInput.Enable();
                    }
                }
                // เทคออเดอร์แล้ว
                else
                {
                    // UI Panel ถูกปิดไป
                    if (orderPanel.gameObject.activeSelf == false)
                    {
                        panel.gameObject.SetActive(true);
                        _playerInput.Enable();
                    }
                }
            }
            // ถ้ายังไม่มี Order มา
            else
            {
                //ยังไม่ได้เทคออเดอร์
                if (orderPanel.IsTakeOrder() == false)
                {
                    // UI Panel ถูกปิดไป
                    if (orderPanel.gameObject.activeSelf == false)
                    {
                        panel.gameObject.SetActive(false);
                        _playerInput.Enable();
                    }
                }
                // เทคออเดอร์แล้ว
                else
                {
                    // UI Panel ถูกปิดไป
                    if (orderPanel.gameObject.activeSelf == false)
                    {
                        panel.gameObject.SetActive(true);
                        _playerInput.Enable();
                    }
                }
            }
        }
    }
    
    private void OnEnable()
    {
        //_playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    
}
