using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int exp;
    //public string rank;

    public PlayerData(PlayerProfile playerProfile)
    {
        playerName = playerProfile.playerName;
        exp = playerProfile.exp;
        //rank = JsonUtility.ToJson(playerProfile.rank);
    }

    public PlayerData()
    {
        playerName = "Player Name";
        exp = 0;
        //rank = RankManager.GetDefaultRank();
    }
}
