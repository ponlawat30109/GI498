using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCarrier : MonoBehaviour
{
    public static DataCarrier Instance { get; private set; }

    private static PlayerData playerProfileData;
    public static PlayerData PlayerProfileData
    {
        get
        {
            if(playerProfileData == null)
            {
                playerProfileData = SaveSystem.LoadPlayerProfile();
            }
            return playerProfileData;
        }
    }

    public static CustomData customData
    {
        get
        {
            if (playerProfileData == null)
                return null;
            return playerProfileData.customData;
        }
    }

    public static int currentRankIndex { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }


    public static void SetPlayerData(PlayerData playerData)
    {
        playerProfileData = playerData;
    }

    public static void SetComingExp(int xP)
    {
        playerProfileData.comingExp = xP;
    }

    public static void AddExp(int xP)
    {
        if (playerProfileData == null)
            Debug.Log("playerProfileData  null");
        else
            playerProfileData.comingExp += xP;
    }

    public static void SetRankIndex(int rankIndex)
    {
        currentRankIndex = rankIndex;
    }

    private void OnApplicationQuit()
    {
        if(playerProfileData != null)
        {
            SaveSystem.SavePlayerProfile(playerProfileData);
        }
    }
}
