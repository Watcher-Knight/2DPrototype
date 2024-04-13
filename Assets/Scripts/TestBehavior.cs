using UnityEngine;

[UpdateEditor]
[AddComponentMenu(ComponentPaths.Master + "/Test")]
public class TestBehavior : MonoBehaviour
{
    [SerializeField] private Vector2 a;
    [SerializeField] private Vector2 b;
    [SerializeField] private bool Dot;

    [Button] private void Test()
    {
        Debug.Log(Vector2.zero.magnitude);
    }
}