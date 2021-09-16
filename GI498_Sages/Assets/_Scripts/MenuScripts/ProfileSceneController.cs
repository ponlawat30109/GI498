using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileSceneController : MonoBehaviour
{
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonCustomCharacter;
    [SerializeField] private Button buttonOpenKitchen;
    void Start()
    {
        //ชุดนี้ลบออกตอนงานเสร็จด้วย 55555
        Debug.Log("Flow ในการทำงาน\n1.Pull Master branch\n2.สร้าง Scene ใน Unity ใหม่\n3.Commit Scene ที่สร้างไปที่ branch ใหม่/n4.หาก Test ผ่าน Merge ไปที่ Master branch");
        Debug.LogWarning("Fon : Don't forget to change UI (3 Button, Information, BG)");
        Debug.LogWarning("Fon : Don't forget to add detail (chef rank)");
        Debug.LogWarning("Fon : Don't forget to coding [change scene to Main Menu Scene] ให้เหมือนนนท์");
        Debug.LogWarning("Fon : Don't forget to coding [change scene to Game Play Scene] ให้เหมือนนนท์");

        Debug.Assert(buttonBack != null, "Profile UI: buttonBack is null");
        Debug.Assert(buttonCustomCharacter != null, "Profile UI: buttonCustomCharacter is null");
        Debug.Assert(buttonOpenKitchen != null, "Profile UI: buttonOpenKitchen is null");

        buttonBack.onClick.AddListener(ChangeSceneToToMainMenuScene);
        buttonCustomCharacter.onClick.AddListener(ChangeSceneToCustomCharacterScene);
        buttonOpenKitchen.onClick.AddListener(ChangeSceneToToGamePlayScene);

    }

    private void ChangeSceneToToMainMenuScene()
    {
        Debug.Log("ChangeSceneToMainMenuScene");
        /*วิธี Load/Unload Test เฉพาะ Scene
        1. พิมพ์ชื่อ scn_name ในช่อง Inspector ของ Scene Handler ใน Starter Scene (scn_Starter)
        2. กด Load/Unload Specific Scene*/

        //temp code (ลบ using UnityEngine.SceneManagement; ด้วย)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
        /*วิธี Load/Unload Test เฉพาะ Scene
        1. พิมพ์ชื่อ scn_name ในช่อง Inspector ของ Scene Handler ใน Starter Scene (scn_Starter)
        2. กด Load/Unload Specific Scene*/

        //temp code (ลบ using UnityEngine.SceneManagement; ด้วย)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
}
