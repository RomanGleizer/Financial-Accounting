using System.IO;
using UnityEngine;

public static class SaveLoadSystem
{
    public static void SaveData(string path, object data)
        => File.WriteAllText(path, JsonUtility.ToJson(data, true), System.Text.Encoding.UTF8);

    public static T GetData<T>(string path)
        => JsonUtility.FromJson<T>(File.ReadAllText(path, System.Text.Encoding.UTF8));
}