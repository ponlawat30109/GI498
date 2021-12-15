using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerProfile : MonoBehaviour
{
    //Saved Parameter
    public string playerName;
    public int exp { get; private set; }
    public Rank rank { get; private set; }

    //Caluculated Parameter
    public int maxExp { get; private set; }

    //Received Parameter from other Scene
    private int gainExp = -1;
    private const string GainExpKey = "GainExp";

    // private bool isProfileSave = false;

    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private Button saveProfileButton;
    [SerializeField] private CustomModelManager customModelManager;

    //[SerializeField] private List<FoodObject> availableFoodList
    //[SerializeField] private List<IngredientObject> availableIngredientList

    void Start()
    {
        saveProfileButton.onClick.AddListener(SaveProfile);

        if (DataCarrier.PlayerProfileData == null)
        {
            LoadProfile();
        }
        else
        {
            LoadProfile(DataCarrier.PlayerProfileData);
        }
        GetPlayerPrefExp();
    }

    private void GetPlayerPrefExp()
    {
        gainExp = PlayerPrefs.GetInt(GainExpKey, 0);
        PlayerPrefs.SetInt(GainExpKey, 0);

        gainExp += DataCarrier.PlayerProfileData.comingExp;
        DataCarrier.PlayerProfileData.comingExp = 0;

        DataCarrier.PlayerProfileData.exp += gainExp;

        if (gainExp > 0)
        {
            RankManager.Instance.UpdateExp(exp, gainExp);
            exp += gainExp;
            gainExp = 0;
        }
    }

    public void SaveProfile()
    {
        playerName = playerNameInput.text;
        var playerData = SaveSystem.SavePlayerProfile(this, customModelManager);
        DataCarrier.SetPlayerData(playerData);

        // isProfileSave = true;
    }

    public void LoadProfile()
    {
        var data = DataCarrier.PlayerProfileData;
        LoadProfile(data);
    }

    public void LoadProfile(PlayerData data)
    {
        if (data == null)
        {
            ResetProfile();
            return;
        }

        playerName = data.playerName;
        playerNameInput.text = playerName;
        exp = data.exp;
        customModelManager.LoadCustomData(data.customData);
        RankManager.Instance.InitialRankListPanel(exp);
    }

    public void ResetProfile()
    {
        Debug.Log("Reset");
        PlayerData data = new PlayerData();
        playerName = "Player Name";
        playerNameInput.text = playerName;
        exp = 0;
        customModelManager.LoadCustomData(data.customData);
        RankManager.Instance.InitialRankListPanel(exp);
        DataCarrier.SetPlayerData(data);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("OnSetRankHolder", 0);
        SaveProfile();
    }

}
