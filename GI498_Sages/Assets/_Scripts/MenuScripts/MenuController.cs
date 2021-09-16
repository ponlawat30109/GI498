using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Start")]
    [SerializeField] private Button buttonStart;

    [Header("Option")]
    [SerializeField] private GameObject panelOption;
    [SerializeField] private Button buttonOption;
    [SerializeField] private Button buttonOptionClose;

    [Header("Exit")]
    [SerializeField] private Button buttonExit;

    [Header("Information")]
    [SerializeField] private GameObject panelInformation;
    [SerializeField] private Button buttonInformation;
    [SerializeField] private Button buttonInformationClose;

    // Start is called before the first frame update
    void Start()
    {
        //ชุดนี้ลบออกตอนงานเสร็จด้วย 55555
        Debug.Log("Flow ในการทำงาน\n1.Pull Master branch\n2.สร้าง Scene ใน Unity ใหม่\n3.Commit Scene ที่สร้างไปที่ branch ใหม่/n4.หาก Test ผ่าน Merge ไปที่ Master branch");
        Debug.LogWarning("Fon : Don't forget to change UI (3 Button, Information, BG)");
        Debug.LogWarning("Fon : Don't forget to change UI (Panel Option, Panel Information)");
        Debug.LogWarning("Fon : Don't forget to coding [option setting]");
        Debug.LogWarning("Fon : Don't forget to coding [change scene to profile] ให้เหมือนนนท์");
        Debug.Log("--------------");

        Debug.Assert(buttonStart != null, "MainMenu UI: buttonStart is null");
        Debug.Assert(buttonOption != null, "MainMenu UI: buttonOption is null");
        Debug.Assert(buttonOptionClose != null, "MainMenu UI: buttonOptionClose is null");
        Debug.Assert(buttonInformation != null, "MainMenu UI: buttonInformation is null");
        Debug.Assert(buttonInformationClose != null, "MainMenu UI: buttonInformationClose is null");
        Debug.Assert(buttonExit != null, "MainMenu UI: buttonExit is null");

        Debug.Assert(panelOption != null, "MainMenu UI: panelOption is null");
        Debug.Assert(panelInformation != null, "MainMenu UI: panelInformation is null");

        buttonStart.onClick.AddListener(ChangeSceneToProfileScene);
        buttonOption.onClick.AddListener(OpenOptionPanel);
        buttonOptionClose.onClick.AddListener(CloseOptionPanel);
        buttonInformation.onClick.AddListener(OpenInformationPanel);
        buttonInformationClose.onClick.AddListener(CloseInformationPanel);
        buttonExit.onClick.AddListener(Exit);

        panelOption.SetActive(false);
        panelInformation.SetActive(false);
    }

    private void CloseInformationPanel()
    {
        panelInformation.SetActive(false);
    }

    private void OpenInformationPanel()
    {
        panelInformation.SetActive(true);
    }

    private void ChangeSceneToProfileScene()
    {
        Debug.Log("ChangeSceneToProfileScene");
        /*วิธี Load/Unload Test เฉพาะ Scene
        1. พิมพ์ชื่อ scn_name ในช่อง Inspector ของ Scene Handler ใน Starter Scene (scn_Starter)
        2. กด Load/Unload Specific Scene*/

        //temp code (ลบ using UnityEngine.SceneManagement; ด้วย)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void CloseOptionPanel()
    {
        panelOption.SetActive(false);
    }

    private void OpenOptionPanel()
    {
        panelOption.SetActive(true);
    }

    private void Exit()
    {
        Debug.Log("Application.Quit");
        Application.Quit();
    }

}
