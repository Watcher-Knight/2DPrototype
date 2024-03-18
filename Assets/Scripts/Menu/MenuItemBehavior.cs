using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[AddComponentMenu(ComponentPaths.MenuItem)]
public class MenuItemBehavior : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color DefaultColor;
    [SerializeField] private Color SelectColor;
    [SerializeField] private string SceneName;

    private TextMeshProUGUI b_Text;
    private TextMeshProUGUI Text => b_Text ??= GetComponent<TextMeshProUGUI>();

    private void Start()
    {
        Text.color = DefaultColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Text.color = SelectColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Text.color = DefaultColor;
    }
}