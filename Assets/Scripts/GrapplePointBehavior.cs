using UnityEngine;

[RequireComponent(typeof(Renderer))]
[AddComponentMenu(ComponentPaths.GrapplePoint)]
public class GrapplePointBehavior : MonoBehaviour, IEventListener
{
    [SerializeField] EventTag SelectionTag;
    [SerializeField] EventTag DeselectionTag;
    [SerializeField] Color SelectColor;
    private Renderer b_Renderer;
    private Renderer Renderer => b_Renderer ??= GetComponent<Renderer>();
    private Color DefaultColor;

    private void Awake()
    {
        DefaultColor = Renderer.material.color;
    }

    public void Invoke(EventTag tag)
    {
        if (tag == SelectionTag) Select();
        if (tag == DeselectionTag) Deselect();
    }

    private void Select() => Renderer.material.color = SelectColor;
    private void Deselect() => Renderer.material.color = DefaultColor;
}