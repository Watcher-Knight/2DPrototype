using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public class EventData
{
    public static string Path = Application.dataPath.Replace("/Assets", "/ProjectSettings/Events.json");
    private static EventData b_Instance;
    private static EventData Instance
    {
        get
        {
            if (b_Instance == null)
            {
                if (!File.Exists(Path))
                {
                    FileStream file = File.Create(Path);
                    file.Close();
                    string newJson = JsonUtility.ToJson(new EventData());
                    File.WriteAllText(Path, newJson);
                }
                string json = File.ReadAllText(Path);
                b_Instance = JsonUtility.FromJson<EventData>(json);
            }
            return b_Instance;
        }
    }
    private static void SaveData()
    {
        string json = JsonUtility.ToJson(Instance);
        File.WriteAllText(Path, json);
    }
    [SerializeField] private List<EditorEventTag> b_Tags;
    private List<EditorEventTag> Tags
    {
        get
        {
            if (b_Tags == null || b_Tags.Count == 0)
            {
                CurrentId = 1;
                b_Tags = new() { new("Default", 1) };
            }
            return b_Tags;
        }
        set => b_Tags = value;
    }
    [SerializeField] private int CurrentId = 0;

    public static EditorEventTag[] GetTags() => Instance.Tags.ToArray();
    public static bool CreateTag(string name)
    {
        Instance.CurrentId += 1;
        return CreateTag(name, Instance.CurrentId);
    }
    private static bool CreateTag(string name, int id)
    {
        if (!Instance.Tags.Any(tag => tag.Name == name) && !Instance.Tags.Any(tag => tag.Id == id))
        {
            Instance.Tags.Add(new(name, id));
            SaveData();
            return true;
        }
        return false;
    }

    public static void DeleteTag(string name)
    {
        Instance.Tags = Instance.Tags.Remove(tag => tag.Name == name).ToList();
        SaveData();
    }

    public static void EditTag(int index, string name)
    {
        if (!Instance.Tags.Any(tag => tag.Name == name))
        {
            Instance.Tags[index] = new(name, Instance.Tags[index].Id);
            SaveData();
        }
    }
}
