using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public static class ObjectEditorExtensions
{
    public static void CreateAsset<T>(this T obj, string path) where T : UnityEngine.Object
    {
        if (!Regex.IsMatch(path, @".asset$")) throw new ArgumentException("Path must end with .asset");
        string[] directories = path.Split("/").SkipLast(1).ToArray();
        string currentDirectory = Application.dataPath;
        for (int i = 1; i < directories.Length; i++)
        {
            currentDirectory += "/" + directories[i];
            if (!Directory.Exists(currentDirectory)) Directory.CreateDirectory(currentDirectory);
        }
        AssetDatabase.CreateAsset(obj, path);
    }
}