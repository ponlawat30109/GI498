using UnityEngine;
using UnityEngine.UI;

public class ProfileSceneController : MonoBehaviour
{
    [SerializeField] private Animator mainPanelAnimator;
    [SerializeField] private Animator addPanelAnimator;

    [Header("Model")]
    [SerializeField] private Animator cameraModelViewer;
    [SerializeField] private ModelPreview cameraModelViewerGroup;

    [Header("Custom")]
    [SerializeField] private Button saveCustomButton;
    [SerializeField] private GameObject savedFeedbackPanel;
    [SerializeField] private Button quitCustomButton;
    [SerializeField] private GameObject alertUnsavePanel;
    [SerializeField] private Button alertYesButton;
    [SerializeField] private Button alertNoButton;

    [Header("Change Scene")]
    [SerializeField] private Button openKitchenOButton;
    [SerializeField] private Button customCharacterButton;
    [SerializeField] private Button backButton;

    [Header("Is Profile Save")]
    [SerializeField] private bool isProfileSave = false;

    void Start()
    {

        try
        {
            //Custom Button
            saveCustomButton.onClick.AddListener(SavedFeedback);
            saveCustomButton.onClick.AddListener(() => isProfileSave = true);
            // quitCustomButton.onClick.AddListener(() => alertUnsavePanel.SetActive(true));
            quitCustomButton.onClick.AddListener(() => CheckSkinIsSave());
            alertYesButton.onClick.AddListener(VisualToProfile);
            alertNoButton.onClick.AddListener(() => alertUnsavePanel.SetActive(false));

            //ChangeScene Button
            openKitchenOButton.onClick.AddListener(ChangeSceneToToGamePlayScene);
            customCharacterButton.onClick.AddListener(VisualToCustomCharacter);
            backButton.onClick.AddListener(ChangeSceneToToMainMenuScene);

            savedFeedbackPanel.SetActive(false);
            alertUnsavePanel.SetActive(false);
        }
        catch
        {
            Debug.Assert(mainPanelAnimator != null, "Profile UI (Canvas): mainUICanvas is null");

            //Model
            Debug.Assert(cameraModelViewer != null, "Profile UI (Canvas): ModelViewer is null");
            Debug.Assert(cameraModelViewerGroup != null, "Profile UI (Canvas): cameraModelViewerGroup is null");

            //Custom
            Debug.Assert(savedFeedbackPanel != null, "Profile UI (Canvas): savedFeedbackPanel is null");
            Debug.Assert(quitCustomButton != null, "Profile UI (Canvas): quitCustomButton is null");
            Debug.Assert(alertUnsavePanel != null, "Profile UI (Canvas): alertUnsavePanel is null");
            Debug.Assert(alertYesButton != null, "Profile UI (Canvas): alertYesButton is null");
            Debug.Assert(alertNoButton != null, "Profile UI (Canvas): alertNoButton is null");
            //ChangeScene
            Debug.Assert(openKitchenOButton != null, "Profile UI (Canvas): openKitchenOButton is null");
            Debug.Assert(customCharacterButton != null, "Profile UI (Canvas): customCharacterButton is null");
            Debug.Assert(backButton != null, "Profile UI (Canvas): backButton is null");
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMusic(AudioManager.Track.BGMMenu01);

    }

    void Update()
    {
        var isProfileSkinChange = CustomModelManager.isProfileSkinChange;
        if (isProfileSkinChange)
        {
            isProfileSave = false;
        }

        // saveCustomButton.onClick.AddListener(() => isProfileSave = true);
    }

    void CheckSkinIsSave()
    {
        if (isProfileSave)
        {
            VisualToProfile();
            // CustomModelManager.isProfileSkinChange = false;
        }
        else
        {
            alertUnsavePanel.SetActive(true);
            // VisualToProfile();
        }
    }

    private void ChangeSceneToToMainMenuScene()
    {
        _Scripts.SceneAnimator.Instance.ChangeScene(_Scripts.SceneEnum.scn_Profile, _Scripts.SceneEnum.scn_MainMenu);
    }

    private void ChangeSceneToToGamePlayScene()
    {
        AudioManager.Instance.StopAudio();
        //Debug.Log("ChangeSceneToToGamePlayScene");
        //var loadScenes = new string[] { "KitchenAssembly", "scn_ItemExample" };
        var loadScenes = new string[] { "KitchenAssembly" };
        var unloadScenes = new string[] { "scn_Profile" };
        _Scripts.SceneAnimator.Instance.ChangeScene(unloadScenes, loadScenes);
    }

    private void VisualToCustomCharacter()
    {
        cameraModelViewer.SetTrigger("Custom");
        mainPanelAnimator.SetTrigger("Custom");
        cameraModelViewerGroup.onProfile = false;
    }

    private void VisualToProfile()
    {
        alertUnsavePanel.SetActive(false);
        cameraModelViewer.SetTrigger("Profile");
        mainPanelAnimator.SetTrigger("Profile");
        cameraModelViewerGroup.onProfile = true;
    }

    private void SavedFeedback()
    {
        addPanelAnimator.SetTrigger("Saved");
        CustomModelManager.isProfileSkinChange = false;
        Debug.Log(CustomModelManager.isProfileSkinChange);
        // isProfileSave = true;
    }
}