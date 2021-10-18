using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private Button saveProfileButton;
    [SerializeField] private CustomModelManager customModelManager;

    //[SerializeField] private List<FoodObject> availableFoodList
    //[SerializeField] private List<IngredientObject> availableIngredientList

    void Start()
    {
        saveProfileButton.onClick.AddListener(SaveProfile);
        LoadProfile();
        RankManager.Instance.InitialRankListPanel(exp);
        GetPlayerPrefExp();
    }

    private void GetPlayerPrefExp()
    {
        gainExp = PlayerPrefs.GetInt(GainExpKey, 0);
        if (gainExp > 0)
        {
            Debug.Log("gainExp = " + gainExp);
            PlayerPrefs.SetInt(GainExpKey, -1);
            RankManager.Instance.UpdateExp(exp, gainExp);
            exp += gainExp;
        }
    }

    public void SaveProfile()
    {
        playerName = playerNameInput.text;
        SaveSystem.SavepPlayerProfile(this, customModelManager);
    }

    public void LoadProfile()
    {
        PlayerData data = SaveSystem.LoadPlayerProfile();

        if (data != null)
        {
            playerName = data.playerName;
            playerNameInput.text = playerName;
            exp = data.exp;
            //LoadRank();
            customModelManager.LoadCustomData(data.customData);

        }
        else
        {
            data = new PlayerData();
            playerName = "Player Name";
            playerNameInput.text = playerName;
            exp = 0;
            customModelManager.LoadCustomData(data.customData);
            //LoadRank();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("OnSetRankHolder", 0);
        SaveProfile();
    }

    //private void LoadRank()
    //{
    //    rank = RankManager.Instance.GetRank(exp);
    //    maxExp = rank.nextRank.minExperience;
    //}

    //private void SetupRank()
    //{

    //}

    //private void UpdateRank()
    //{
    //    //Find if new rank
    //    //Play RankUp Animation
    //    /*
    //     * send
    //     * - gain Exp
    //     * - rank (get max exp)
    //     * - slider (set max exp)
    //     */
    //    gainExp = 0;
    //}


}
