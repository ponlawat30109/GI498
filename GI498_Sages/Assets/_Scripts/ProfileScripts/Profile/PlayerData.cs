using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int exp;
    //public string rank;
    public CustomData customData;

    public PlayerData(PlayerProfile playerProfile, CustomModelManager customModelManager)
    {
        playerName = playerProfile.playerName;
        exp = playerProfile.exp;
        customData = customModelManager.SaveCustomData();
        //rank = JsonUtility.ToJson(playerProfile.rank);
    }

    public PlayerData()
    {
        playerName = "Player Name";
        exp = 0;
        //rank = RankManager.GetDefaultRank();
    }
}
