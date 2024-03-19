using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu(ComponentPaths.Master + "/Test")]
public class TestBehavior : MonoBehaviour
{
    [SerializeField] private SceneReference Scene;
    [Button]
    private void Test()
    {
        Scene.Load();
    }
}