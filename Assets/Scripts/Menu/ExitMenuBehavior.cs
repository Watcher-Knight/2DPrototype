using System.Runtime.InteropServices;
using UnityEngine;

[AddComponentMenu(ComponentPaths.ExitMenuItem)]
public class ExitMenuBehavior : MenuItemBehavior
{
        public override void OnClick()
        {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
#elif UNITY_WEBGL
        Exit();
#endif
        }

#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void Exit();
#endif
}