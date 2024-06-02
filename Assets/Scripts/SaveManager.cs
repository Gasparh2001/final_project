using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static void SavePlayerData(ScSaveZone player)
    {
        PlayerData playerData = new PlayerData(player);//Data Save
        string dataPath = Application.persistentDataPath + "/playerTime.save";//Directory
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);//Create File
        BinaryFormatter binaryFormatter = new BinaryFormatter();//011001010
        binaryFormatter.Serialize(fileStream, playerData);//Convert 01100101
        fileStream.Close();//Close the Archive
    }

    public static PlayerData LoadPlayerData()
    {
        string dataPath = Application.persistentDataPath + "/playerTime.save";//Directory

        if(File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);//Open File
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            PlayerData playerData = (PlayerData) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return playerData;//Return call Archive
        }
        else
        {
            Debug.LogError("No save files");
            return null;
        }
    }
}
