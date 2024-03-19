using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MenuItemBehavior : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Color DefaultColor;
    [SerializeField] private Color SelectColor;

    private TextMeshProUGUI b_Text;
    private TextMeshProUGUI Text => b_Text ??= GetComponent<TextMeshProUGUI>();

    private void Awake()
    {
        DefaultColor = Text.color;
    }

    public void OnPointerClick(PointerEventData eventData) => OnClick();

    public void OnPointerEnter(PointerEventData eventData)
    {
        Text.color = SelectColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Text.color = DefaultColor;
    }

    public abstract void OnClick();
}