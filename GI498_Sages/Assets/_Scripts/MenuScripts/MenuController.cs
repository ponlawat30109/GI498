using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("MainMenu")]
    [SerializeField] private Button buttonStart;
    [SerializeField] private Button buttonOption;
    [SerializeField] private Button buttonExit;

    [Header("Option")]
    [SerializeField] private GameObject panelOption;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSound;
    [SerializeField] private Button buttonOptionClose;

    [Header("Information")]
    [SerializeField] private GameObject panelInformation;
    [SerializeField] private Button buttonInformation;
    [SerializeField] private Button buttonInformationClose;

    private const string MusicVolKey = "MusicVol";
    private const string SoundVolKey = "SoundVol";

    //private void Awake()
    //{
    //    Debug.Log
    //}

    // Start is called before the first frame update
    void Start()
    {
        //ชุดนี้ลบออกตอนงานเสร็จด้วย 55555
        Debug.LogWarning("Fon : Don't forget to change UI (3 Button, Information, BG)");
        Debug.LogWarning("Fon : Don't forget to change UI (Panel Option, Panel Information)");
        Debug.Log("--------------");

        Debug.Assert(buttonStart != null, "MainMenu UI ("+name+"): buttonStart is null");
        Debug.Assert(buttonOption != null, "MainMenu UI (" + name + "): buttonOption is null");
        Debug.Assert(sliderMusic != null, "MainMenu UI (" + name + "): sliderMusic is null");
        //Debug.Assert(sliderSound != null, "MainMenu UI (" + name + "): sliderSound is null");
        Debug.Assert(buttonOptionClose != null, "MainMenu UI (" + name + "): buttonOptionClose is null");
        Debug.Assert(buttonInformation != null, "MainMenu UI (" + name + "): buttonInformation is null");
        Debug.Assert(buttonInformationClose != null, "MainMenu UI (" + name + "): buttonInformationClose is null");
        Debug.Assert(buttonExit != null, "MainMenu UI (" + name + "): buttonExit is null");

        Debug.Assert(panelOption != null, "MainMenu UI (" + name + "): panelOption is null");
        Debug.Assert(panelInformation != null, "MainMenu UI (" + name + "): panelInformation is null");

        buttonStart.onClick.AddListener(ChangeSceneToProfileScene);
        buttonOption.onClick.AddListener(OpenOptionPanel);
        buttonOptionClose.onClick.AddListener(CloseOptionPanel);
        buttonInformation.onClick.AddListener(OpenInformationPanel);
        buttonInformationClose.onClick.AddListener(CloseInformationPanel);
        buttonExit.onClick.AddListener(() => Application.Quit());
        
        panelOption.SetActive(false);
        panelInformation.SetActive(false);

        LoadOptionPrefs();
        //set soundVolumn in soundManager
        AudioManager.Instance.PlayMusic(AudioManager.Track.BGMMenu01);

        sliderMusic.onValueChanged.AddListener((volumn) => SetOption("music", volumn));
        //sliderSound.onValueChanged.AddListener((volumn) => SetOption("sound", volumn));
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
        _Scripts.SceneAnimator.Instance.ChangeScene(_Scripts.SceneEnum.scn_MainMenu, _Scripts.SceneEnum.scn_Profile);
    }

    private void OpenOptionPanel()
    {
        panelOption.SetActive(true);
    }

    private void CloseOptionPanel()
    {
        SaveOptionPrefs();
        panelOption.SetActive(false);
    }

    public void SetOption(string content, float volumn)
    {
        switch(content)
        {
            case "music":
                {
                    AudioManager.Instance.ChangeAudioVolumn(volumn);
                    break;
                }
            case "sound":
                {
                    AudioManager.Instance.ChangeSfxVolumn(volumn);
                    break;
                }
        }
        //Debug.Log(content + ": " + volumn);
    }

    public void SaveOptionPrefs()
    {
        PlayerPrefs.SetFloat(MusicVolKey, sliderMusic.normalizedValue);
        PlayerPrefs.SetFloat(SoundVolKey, sliderSound.normalizedValue);
        PlayerPrefs.Save();
    }

    public void LoadOptionPrefs()
    {
        sliderMusic.value = PlayerPrefs.GetFloat(MusicVolKey, 0.50f);
        sliderSound.value = PlayerPrefs.GetFloat(SoundVolKey, 0.50f);

        AudioManager.Instance.ChangeAudioVolumn(sliderMusic.value);
        AudioManager.Instance.ChangeSfxVolumn(sliderSound.value);
    }

}
