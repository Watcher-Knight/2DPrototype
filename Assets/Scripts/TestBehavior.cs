using UnityEngine;

[UpdateEditor]
[AddComponentMenu(ComponentPaths.Master + "/Test")]
public class TestBehavior : MonoBehaviour
{
    [Display] Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    private Vector2 Origin;
    private void Update()
    {
        Debug.DrawLine(Origin, MousePosition, Color.red);

        if (Input.GetMouseButtonDown(1)) Origin = MousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            // RaycastHit2D hit = Physics2D.CircleCast(Origin, Vector2.Distance(Origin, MousePosition), Vector2.right);
            // Debug.Log(hit.point);

            Debug.Log(Vector2.SignedAngle(Vector2.up, MousePosition - Origin));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Origin, Vector2.Distance(Origin, MousePosition));
    }
}