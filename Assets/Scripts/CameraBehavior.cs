using UnityEngine;

[AddComponentMenu(ComponentPaths.Camera)]
[RequireComponent(typeof(Camera))]
public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private Vector2 MinBounds = new(28, 18);

    private void Update()
    {
        Camera camera = GetComponent<Camera>();

        float orthographicWidth = MinBounds.x * camera.pixelHeight / camera.pixelWidth;
        float size = Mathf.Max(orthographicWidth, MinBounds.y) * 0.5f;

        camera.orthographicSize = size;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, MinBounds);
    }
}