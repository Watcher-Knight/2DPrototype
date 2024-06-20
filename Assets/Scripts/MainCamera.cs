using UnityEngine;

public static class MainCamera
{
    private static Camera Instance;
    public static Camera Get()
    {
        if (Instance != null) return Instance;
        if (Camera.main != null) return Instance = Camera.main;
        if (Object.FindObjectOfType<Camera>() != null) return Object.FindObjectOfType<Camera>();
        return new GameObject("Main Camera").AddComponent<Camera>();
    }
    public static void Initialize(Camera camera) => Instance = camera;
};