using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static PlayerData SavePlayerProfile (PlayerProfile playerProfile, CustomModelManager customModelManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        //string path = "C:/System/"
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerProfile, customModelManager);

        formatter.Serialize(stream, data);
        stream.Close();

        return data;
    }

    public static PlayerData SavePlayerProfile(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();

        return data;
    }

    public static PlayerData LoadPlayerProfile()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data;

            try
            {
                data = formatter.Deserialize(stream) as PlayerData;
            }
            catch
            {
                data = new PlayerData();
            }

            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Save file not found in" + path);
            return null;
        }
    }

    //public static void SaveCustomModel()
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    //string path = "C:/System/"
    //    string path = Application.persistentDataPath + "/player.fun";
    //    FileStream stream = new FileStream(path, FileMode.Create);

    //    PlayerData data = new PlayerData(playerProfile);

    //    formatter.Serialize(stream, data);
    //    stream.Close();
    //}
}
