using UnityEngine;
using System.IO;

public class SaveSystem
{
    private static SaveData saveData = new SaveData();

    [System.Serializable]
    public struct SaveData
    {
        public PlayerSaveData playerData;
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save" + ".save";
        return saveFile;
    }

    public static void Save()
    {
        HandleSaveData();

        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(saveData));
    }

    private static void HandleSaveData()
    {
        PlayerMovement.current.Save(ref saveData.playerData);
    }

    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());

        saveData  =JsonUtility.FromJson<SaveData>(saveContent);
        HandleLoadData();
    }

    private static void HandleLoadData()
    {
        PlayerMovement.current.Load(saveData.playerData);
    }
}
