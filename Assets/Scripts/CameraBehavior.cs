using UnityEngine;

[AddComponentMenu(ComponentPaths.Camera)]
[RequireComponent(typeof(Camera))]
public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private float MinWidth = 28;
    [SerializeField] private float MinHeight = 18;

    private void Update()
    {
        Camera camera = GetComponent<Camera>();

        float orthographicWidth = MinWidth * camera.pixelHeight / camera.pixelWidth;
        float size = Mathf.Max(orthographicWidth, MinHeight) * 0.5f;

        camera.orthographicSize = size;
    }
}