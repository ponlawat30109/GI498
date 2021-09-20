using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileSceneController : MonoBehaviour
{
    [SerializeField] private Animator mainUICanvas;

    [Header("Model")]
    [SerializeField] private Animator ModelViewer;
    
    [Header("Profile")]
    [SerializeField] private Button buttonChefRank;

    [Header("Custom")]
    [SerializeField] private Button buttonQuitCustom;
    [SerializeField] private GameObject panelAlertUnsave;
    [SerializeField] private Button buttonAlertYes;
    [SerializeField] private Button buttonAlertNo;

    [Header("Information")]
    [SerializeField] private GameObject panelInformation;
    [SerializeField] private Button buttonInformation;
    [SerializeField] private Button buttonInformationClose;

    [Header("Change Scene")]
    [SerializeField] private Button buttonOpenKitchen;
    [SerializeField] private Button buttonCustomCharacter;
    [SerializeField] private Button buttonBack;

    void Start()
    {
        //ชุดนี้ลบออกตอนงานเสร็จด้วย 55555
        Debug.Log("Flow ในการทำงาน(คลิกเพื่ออ่านต่อ)\n1.Pull Master branch\n2.สร้าง Scene ใน Unity ใหม่\n3.Commit Scene ที่สร้างไปที่ branch ใหม่/n4.หาก Test ผ่าน Merge ไปที่ Master branch");
        Debug.LogWarning("Fon : Don't forget to change UI (3 Button, Information, BG)");
        Debug.LogWarning("Fon : Don't forget to add detail (chef rank)");
        Debug.LogWarning("Fon : Don't forget to coding [change scene to Main Menu Scene] ให้เหมือนนนท์");
        Debug.LogWarning("Fon : Don't forget to coding [change scene to Game Play Scene] ให้เหมือนนนท์");

        Debug.Assert(mainUICanvas != null, "Profile UI: mainUICanvas is null");
        //Model
        Debug.Assert(ModelViewer != null, "Profile UI: ModelViewer is null");
        //Profile
        Debug.Assert(buttonChefRank != null, "Profile UI: buttonChefRank is null");
        //Custom
        Debug.Assert(buttonQuitCustom != null, "Profile UI: buttonQuitCustom is null");
        Debug.Assert(panelAlertUnsave != null, "Profile UI: panelAlertUnsave is null");
        Debug.Assert(buttonAlertYes != null, "Profile UI: buttonAlertYes is null");
        Debug.Assert(buttonAlertNo != null, "Profile UI: buttonAlertNo is null");
        //Information
        Debug.Assert(panelInformation != null, "Profile UI: panelInformation is null");
        Debug.Assert(buttonInformation != null, "Profile UI: buttonInformation is null");
        Debug.Assert(buttonInformationClose != null, "Profile UI: buttonInformationClose is null");
        //ChangeScene
        Debug.Assert(buttonOpenKitchen != null, "Profile UI: buttonOpenKitchen is null");
        Debug.Assert(buttonCustomCharacter != null, "Profile UI: buttonCustomCharacter is null");
        Debug.Assert(buttonBack != null, "Profile UI: buttonBack is null");

        //buttonRank.onClick.AddListener()

        //Custom Button
        buttonQuitCustom.onClick.AddListener(() => panelAlertUnsave.SetActive(true));
        buttonAlertYes.onClick.AddListener(() => {
            ChangeSceneToProfileScene();
            Debug.Log("Fon : ยังบ่ได๋เซฟเด้อค่ะ");
            });
        buttonAlertNo.onClick.AddListener(() => panelAlertUnsave.SetActive(false));

        //Information Button
        buttonInformation.onClick.AddListener(() => panelInformation.SetActive(true));
        buttonInformationClose.onClick.AddListener(() => panelInformation.SetActive(false));
        //ChangeScene Button
        buttonOpenKitchen.onClick.AddListener(ChangeSceneToToGamePlayScene);
        buttonCustomCharacter.onClick.AddListener(ChangeSceneToCustomCharacterScene);
        buttonBack.onClick.AddListener(ChangeSceneToToMainMenuScene);

        panelInformation.SetActive(false);
        panelAlertUnsave.SetActive(false);
    }

    private void ChangeSceneToToMainMenuScene()
    {
        Debug.Log("ChangeSceneToMainMenuScene");
        /*วิธี Load/Unload Test เฉพาะ Scene
        1. พิมพ์ชื่อ scn_name ในช่อง Inspector ของ Scene Handler ใน Starter Scene (scn_Starter)
        2. กด Load/Unload Specific Scene*/

        //temp code (ลบ using UnityEngine.SceneManagement; ด้วย)
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void ChangeSceneToToGamePlayScene()
    {
        Debug.Log("ChangeSceneToToGamePlayScene");
        /*วิธี Load/Unload Test เฉพาะ Scene
        1. พิมพ์ชื่อ scn_name ในช่อง Inspector ของ Scene Handler ใน Starter Scene (scn_Starter)
        2. กด Load/Unload Specific Scene*/

        //temp code (ลบ using UnityEngine.SceneManagement; ด้วย)
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ChangeSceneToCustomCharacterScene()
    {
        Debug.Log("ChangeSceneToCustomCharacterScene");

        //temp code (ลบ using UnityEngine.SceneManagement; ด้วย)
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        ModelViewer.SetTrigger("Custom");
        mainUICanvas.SetTrigger("Custom");
    }

    private void ChangeSceneToProfileScene()
    {
        Debug.Log("ChangeSceneToProfileScene");
        panelAlertUnsave.SetActive(false);
        ModelViewer.SetTrigger("Profile");
        mainUICanvas.SetTrigger("Profile");
    }
}