using UnityEngine;

[AddComponentMenu(ComponentPaths.ExitMenuItem)]
public class ExitMenuBehavior : MenuItemBehavior
{
    public override void OnClick()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}