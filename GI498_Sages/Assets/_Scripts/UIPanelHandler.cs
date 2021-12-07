using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIPanelHandler : MonoBehaviour
{
    public static UIPanelHandler instance;
    private GameObject uiPanel;

    public bool UIPanelActive = true;
    // public bool isFirstPlay = false;

    private void Awake()
    {
        instance = this;
    }

    // void Start()
    // {

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
            Debug.Log("Close UI");
            CloseUIPanel();
        }

    }

    void CheckUIisActive()
    {
        if (this.gameObject.activeInHierarchy)
        {
            UIPanelActive = true;
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
        // await Task.Delay(System.TimeSpan.FromSeconds(1));
        this.gameObject.SetActive(false);
    }
}
