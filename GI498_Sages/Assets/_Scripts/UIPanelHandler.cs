using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIPanelHandler : MonoBehaviour
{
    public static UIPanelHandler instance;
    private GameObject uiPanel;

    public PlayerInput _playerInput;

    public bool UIPanelActive = false;

    private void Awake()
    {
        instance = this;
        UIPanelActive = false;
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        CheckUIisActive();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        CheckUIisActive();
    }

    void Start()
    {
        // CheckUIisActive();

        _playerInput.UI.ClosePanel.started += ctx =>
        {
            UIPanelActive = false;
            PlayerController.instance.UIPanelActive = UIPanelActive;
            Debug.Log("Close UI");
        };
        _playerInput.UI.ClosePanel.canceled += ctx =>
        {
            CloseUIPanel();
        };
    }

    // void Update()
    // {
    //     // CheckUIisActive();

    //     // if (PlayerController.instance._playerInput.UI.ClosePanel.triggered)
    //     // {
    //     //     UIPanelActive = false;
    //     //     PlayerController.instance.UIPanelActive = UIPanelActive;
    //     //     // Debug.Log("Close UI");
    //     // }

    //     // await Task.Delay(System.TimeSpan.FromSeconds(0.1));
    //     // CloseUIPanel();

    //     // PlayerController.instance.UIPanelActive = UIPanelActive;

    //     // CheckUIisActive();
    // }

    public void CheckUIisActive()
    {
        if (this.gameObject.activeSelf || this.gameObject.activeInHierarchy)
        {
            UIPanelActive = true;
            PlayerController.instance.UIPanelActive = UIPanelActive;
        }
        else
        {
            UIPanelActive = false;
            PlayerController.instance.UIPanelActive = UIPanelActive;
        }
    }

    public void CloseUIPanel()
    {
        if (UIPanelActive == false)
        {
            UIPanelActive = false;
            gameObject.SetActive(false);
        }
    }
}
