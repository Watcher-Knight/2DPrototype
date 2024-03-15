using System;

[Serializable]
public struct EditorEventTag
{
    public string Name;
    public int Id;
    public EditorEventTag(string name, int id)
    {
        Name = name;
        Id = id;
    }
}