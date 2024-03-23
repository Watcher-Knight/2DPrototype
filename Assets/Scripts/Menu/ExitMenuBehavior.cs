using UnityEngine;

[AddComponentMenu(ComponentPaths.ExitMenuItem)]
public class ExitMenuBehavior : MenuItemBehavior
{
    public override void OnClick()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}