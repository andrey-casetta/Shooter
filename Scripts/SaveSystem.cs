using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //player data
    public static void SavePlayer(PlayerBase playerBase)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(playerBase);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            
            stream.Close();
           
            return playerData;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    //system data
    public static void SaveSystemData(GameManager gameManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/system.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        SystemData systemData = new SystemData(gameManager);

        formatter.Serialize(stream, gameManager);
        stream.Close();
    }

    public static SystemData LoadSystemData()
    {
        string path = Application.persistentDataPath + "/system.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SystemData systemData = formatter.Deserialize(stream) as SystemData;

            stream.Close();

            return systemData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
