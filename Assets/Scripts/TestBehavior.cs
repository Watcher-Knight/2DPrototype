using UnityEngine;
using UnityEngine.UI;

[UpdateEditor]
[AddComponentMenu(ComponentPaths.Master + "/Test")]
public class TestBehavior : MonoBehaviour
{
    [SerializeField] private Vector2 Point;
    [SerializeField] private float Rotation;

    [Button] private void Test()
    {
        Quaternion rotation = Quaternion.Euler(0, 0, Rotation);
 
        Debug.Log(Vector2.Angle(Vector2.zero, Vector2.up));
    }
}