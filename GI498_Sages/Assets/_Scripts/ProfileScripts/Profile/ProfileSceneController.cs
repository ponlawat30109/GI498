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
    [SerializeField] private Animator cameraModelViewer;
    [SerializeField] private ModelPreview cameraModelViewerGroup;

    [Header("Custom")]
    [SerializeField] private Button quitCustomButton;
    [SerializeField] private GameObject alertUnsavePanel;
    [SerializeField] private Button alertYesButton;
    [SerializeField] private Button alertNoButton;

    [Header("Information")]
    [SerializeField] private GameObject informationPanel;
    [SerializeField] private Button informationButton;
    [SerializeField] private Button informationCloseButton;

    [Header("Change Scene")]
    [SerializeField] private Button openKitchenOButton;
    [SerializeField] private Button customCharacterButton;
    [SerializeField] private Button backButton;

    void Start()
    {
        //ชุดนี้ลบออกตอนงานเสร็จด้วย 55555
        Debug.Log("Flow ในการทำงาน(คลิกเพื่ออ่านต่อ)\n1.Pull Master branch\n2.สร้าง Scene ใน Unity ใหม่\n3.Commit Scene ที่สร้างไปที่ branch ใหม่/n4.หาก Test ผ่าน Merge ไปที่ Master branch");
        Debug.LogWarning("Fon : Don't forget to change UI (3 Button, Information, BG)");
        Debug.LogWarning("Fon : Don't forget to add detail (chef rank)");
        Debug.LogWarning("Fon : Don't forget to coding [change scene to Main Menu Scene] ให้เหมือนนนท์");
        Debug.LogWarning("Fon : Don't forget to coding [change scene to Game Play Scene] ให้เหมือนนนท์");

        Debug.Assert(mainUICanvas != null, "Profile UI (Canvas): mainUICanvas is null");
        //Model
        Debug.Assert(cameraModelViewer != null, "Profile UI (Canvas): ModelViewer is null");
        Debug.Assert(cameraModelViewerGroup != null, "Profile UI (Canvas): cameraModelViewerGroup is null");

        //Custom
        Debug.Assert(quitCustomButton != null, "Profile UI (Canvas): quitCustomButton is null");
        Debug.Assert(alertUnsavePanel != null, "Profile UI (Canvas): alertUnsavePanel is null");
        Debug.Assert(alertYesButton != null, "Profile UI (Canvas): alertYesButton is null");
        Debug.Assert(alertNoButton != null, "Profile UI (Canvas): alertNoButton is null");
        //Information
        Debug.Assert(informationPanel != null, "Profile UI (Canvas): informationPanel is null");
        Debug.Assert(informationButton != null, "Profile UI (Canvas): informationButton is null");
        Debug.Assert(informationCloseButton != null, "Profile UI (Canvas): informationCloseButton is null");
        //ChangeScene
        Debug.Assert(openKitchenOButton != null, "Profile UI (Canvas): openKitchenOButton is null");
        Debug.Assert(customCharacterButton != null, "Profile UI (Canvas): customCharacterButton is null");
        Debug.Assert(backButton != null, "Profile UI (Canvas): backButton is null");

        //buttonRank.onClick.AddListener()


        //Custom Button
        quitCustomButton.onClick.AddListener(() => alertUnsavePanel.SetActive(true));
        alertYesButton.onClick.AddListener(() => {
            ChangeSceneToProfileScene();
            Debug.Log("Fon : ยังบ่ได๋เซฟเด้อค่ะ");
            });
        alertNoButton.onClick.AddListener(() => alertUnsavePanel.SetActive(false));

        //Information Button
        informationButton.onClick.AddListener(() => informationPanel.SetActive(true));
        informationCloseButton.onClick.AddListener(() => informationPanel.SetActive(false));
        //ChangeScene Button
        openKitchenOButton.onClick.AddListener(ChangeSceneToToGamePlayScene);
        customCharacterButton.onClick.AddListener(ChangeSceneToCustomCharacterScene);
        backButton.onClick.AddListener(ChangeSceneToToMainMenuScene);

        informationPanel.SetActive(false);
        alertUnsavePanel.SetActive(false);
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
        cameraModelViewer.SetTrigger("Custom");
        mainUICanvas.SetTrigger("Custom");
        cameraModelViewerGroup.onProfile = false;
    }

    private void ChangeSceneToProfileScene()
    {
        Debug.Log("ChangeSceneToProfileScene");
        alertUnsavePanel.SetActive(false);
        cameraModelViewer.SetTrigger("Profile");
        mainUICanvas.SetTrigger("Profile");
        cameraModelViewerGroup.onProfile = true;
    }
}