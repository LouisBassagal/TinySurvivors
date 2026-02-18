using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance { get; private set; }

    private UIDocument InGameUIDocument;
    
    private VisualElement m_XPBar;
    private VisualElement m_LevelUpPopup;
    private VisualElement m_leftCard;
    private VisualElement m_middleCard;
    private VisualElement m_rightCard;
    [SerializeField]
    private List<AbilitySO> m_allAbilities = new();

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
        m_LevelUpPopup = InGameUIDocument.rootVisualElement.Q<VisualElement>("LevelUpPopup");
        m_leftCard = m_LevelUpPopup.Q<VisualElement>("LeftCard");
        m_middleCard = m_LevelUpPopup.Q<VisualElement>("MiddleCard");
        m_rightCard = m_LevelUpPopup.Q<VisualElement>("RightCard");
        SetXPBarFill(0);

        PlayerStats.Instance.OnXPAdded += UpdateXPBar;
        PlayerStats.Instance.OnLevelUp += UpdateXPBar;
        PlayerStats.Instance.OnLevelUp += () => ShowLevelUpPopup();
        m_LevelUpPopup.SetEnabled(false);
    }

    private void SetXPBarFill(float fillAmount)
    {
        m_XPBar.style.width = Length.Percent(fillAmount / PlayerStats.Instance.XPToNextLevel * 100);
    }

    public void ShowLevelUpPopup()
    {
        Time.timeScale = 0f;
        m_LevelUpPopup.SetEnabled(true);

        foreach (var card in new[] { m_leftCard, m_middleCard, m_rightCard })
        {
            var title = card.Q<Label>("TitleCard");
            var description = card.Q<Label>("DescriptionCard");
            var ability = m_allAbilities[Random.Range(0, m_allAbilities.Count)];
            title.text = ability.Title;
            description.text = ability.Description;
        }
    }

    public void OnUpgradeSelected()
    {
        Time.timeScale = 1f;
        m_LevelUpPopup.SetEnabled(false);
    }
}
