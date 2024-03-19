using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu(ComponentPaths.SceneMenuItem)]
public class SceneMenuItemBehavior : MenuItemBehavior
{
    [SerializeField] private SceneReference Scene;
    public override void OnClick()
    {
        Scene.Load();
    }
}