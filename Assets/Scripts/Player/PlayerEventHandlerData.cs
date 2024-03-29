using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEventHandler", menuName = AssetPaths.PlayerEventHandlerData, order = 0)]
public class PlayerEventHandlerData : ScriptableObject
{
    [field: SerializeField] public EventTag ExitTag { get; private set; }
    [field: SerializeField] public SceneReference Menu { get; private set; }
}