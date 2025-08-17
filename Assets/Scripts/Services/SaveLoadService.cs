using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class SaveLoadService
{
    private readonly List<OrganizationData> organizations;
    private static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    public static void Save(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
        Debug.Log($"[SaveLoadService] Save data saved to {SavePath}");
    }

    public static SaveData Load()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("[SaveLoadService] No save file found.");
            return new SaveData();
        }

        string json = File.ReadAllText(SavePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        Debug.Log($"[SaveLoadService] Save data loaded from {SavePath}");
        return saveData;
    }

    public static void SaveProgress(List<OrganizationData> organizations)
    {
        Save(new SaveData { organizations = organizations });
    }
}
