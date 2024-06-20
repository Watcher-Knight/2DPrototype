using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[AddComponentMenu(ComponentPaths.GrapplePoint)]
public class GrapplePointBehavior : MonoBehaviour, IEventListener
{
    [SerializeField] EventTag SelectionTag;
    [SerializeField] EventTag DeselectionTag;
    [SerializeField] Color SelectColor = Color.red;
    private SpriteRenderer b_Renderer;
    private SpriteRenderer Renderer => b_Renderer ??= GetComponent<SpriteRenderer>();
    private Color DefaultColor;

    private void Awake()
    {
        DefaultColor = Renderer.color;
    }

    public void Invoke(EventTag tag)
    {
        // if (tag == SelectionTag) Select();
        // if (tag == DeselectionTag) Deselect();
    }

    private void Select() => Renderer.color = SelectColor;
    private void Deselect() => Renderer.color = DefaultColor;
}