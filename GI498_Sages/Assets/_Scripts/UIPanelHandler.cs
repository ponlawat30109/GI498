using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIPanelHandler : MonoBehaviour
{
    public static UIPanelHandler instance;
    private GameObject uiPanel;

    public bool UIPanelActive = false;
    // public bool isFirstPlay = false;

    private void Awake()
    {
        instance = this;
        UIPanelActive = false;
    }

    // void Start()
    // {
    //     PlayerController.instance._playerInput.UI.ClosePanel.started += ctx =>
    //             {
    //                 UIPanelActive = false;
    //                 PlayerController.instance.UIPanelActive = UIPanelActive;
    //                 Debug.Log("Close UI");
    //             };
    // }

    void Update()
    {
        CheckUIisActive();

        // if (Keyboard.current.escapeKey.wasPressedThisFrame)
        // {
        //     UIIsUnactive();
        // }

        // if (PlayerController.instance != null)
        // {
        //     PlayerController.instance.UIPanelActive = UIPanelActive;
        // }

        if (PlayerController.instance._playerInput.UI.ClosePanel.triggered)
        {
            UIPanelActive = false;
            PlayerController.instance.UIPanelActive = UIPanelActive;
            // Debug.Log("Close UI");
        }

        // await Task.Delay(System.TimeSpan.FromSeconds(0.1));
        CloseUIPanel();
    }

    void CheckUIisActive()
    {
        if (this.gameObject.activeInHierarchy)
        {
            UIPanelActive = true;
            PlayerController.instance.UIPanelActive = UIPanelActive;
        }
    }

    // void UIIsActive()
    // {
    //     UIPanelActive = true;
    // }

    // void UIIsUnactive()
    // {
    //     UIPanelActive = false;
    // }

    void CloseUIPanel()
    {
        if (UIPanelActive == false)
            // await Task.Delay(System.TimeSpan.FromSeconds(1));
            gameObject.SetActive(false);
    }
}
