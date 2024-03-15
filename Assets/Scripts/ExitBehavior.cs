using UnityEngine;

[AddComponentMenu(ComponentPaths.Master + "/Exit")]
public class ExitBehavior : MonoBehaviour
{
    [SerializeField] private EventTag EventTag;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.SendMessage(EventTag);
    }
}