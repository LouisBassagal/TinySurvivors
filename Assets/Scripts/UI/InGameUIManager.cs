using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance { get; private set; }

    private UIDocument InGameUIDocument;
    
    private VisualElement m_XPBar;

    public void UpdateXPBar() => SetXPBarFill(PlayerStats.Instance.CurrentXP);
    
    private void Awake()
    {
        Instance = this;
        if (Instance != this) { 
            Destroy(this.gameObject); 
            return; 
        }
    }

    void Start()
    {
        InGameUIDocument = GetComponent<UIDocument>();
        m_XPBar = InGameUIDocument.rootVisualElement.Q<VisualElement>("Foreground");
        SetXPBarFill(0);
        PlayerStats.Instance.OnXPAdded += UpdateXPBar;
        PlayerStats.Instance.OnLevelUp += UpdateXPBar;
    }

    private void SetXPBarFill(float fillAmount)
    {
        m_XPBar.style.width = Length.Percent((fillAmount / PlayerStats.Instance.XPToNextLevel) * 100);
    }
}
