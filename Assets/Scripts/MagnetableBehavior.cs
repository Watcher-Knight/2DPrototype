using UnityEngine;

[AddComponentMenu(ComponentPaths.Magnetable)]
public class MagnetableBehavior : MonoBehaviour
{
    [SerializeField] Color SelectColor = Color.red;
    private SpriteRenderer b_Renderer;
    private SpriteRenderer Renderer => b_Renderer ??= GetComponent<SpriteRenderer>();
    private Color DefaultColor;

    private void Awake()
    {
        DefaultColor = Renderer.color;
    }
    public void Select() => Renderer.color = SelectColor;
    public void Deselect() => Renderer.color = DefaultColor;
}