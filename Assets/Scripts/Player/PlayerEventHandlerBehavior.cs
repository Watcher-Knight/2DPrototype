using UnityEngine;

[AddComponentMenu(ComponentPaths.PlayerEventHandler)]
public class PlayerEventHandlerBehavior : MonoBehaviour, IEventListener
{
    [SerializeField] PlayerEventHandlerData Data;

    public void Invoke(EventTag tag)
    {
        if (tag == Data.ExitTag)
        {
            Data.Menu.Load();
        }
    }
}