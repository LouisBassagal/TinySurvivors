using System.Collections.Generic;
using System.Linq;
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
    private readonly Dictionary<Button, EventCallback<ClickEvent>> m_cardAbilityMap = new();

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
        m_leftCard = m_LevelUpPopup.Q<Button>("LeftCard");
        m_middleCard = m_LevelUpPopup.Q<Button>("MiddleCard");
        m_rightCard = m_LevelUpPopup.Q<Button>("RightCard");
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

        foreach (Button card in new[] { m_leftCard, m_middleCard, m_rightCard })
        {
            var title = card.Q<Label>("TitleCard");
            var description = card.Q<Label>("DescriptionCard");
            var ability = m_allAbilities[Random.Range(0, m_allAbilities.Count)];
            var abilityLevel = PlayerStats.Instance.GetAbilityLevel(ability.name);

            EventCallback<ClickEvent> callback = evt => OnUpgradeSelected(ability);
            m_cardAbilityMap[card] = callback;
            card.RegisterCallbackOnce(callback);

            title.text = ability.Title + " Lv" + abilityLevel;
            description.text = ability.Description;
        }
    }

    private void OnUpgradeSelected(AbilitySO ability)
    {
        Time.timeScale = 1f;
        m_LevelUpPopup.SetEnabled(false);

        ClearCallbacks();
        PlayerStats.Instance.SetAbility(ability);
        Debug.Log("Selected upgrade: " + ability.Title);
    }

    /// <summary>
    /// Unregisters all callbacks from the upgrade cards and clears the mapping dictionary.
    /// </summary>
    private void ClearCallbacks()
    {
        foreach (Button card in new[] { m_leftCard, m_middleCard, m_rightCard }.Cast<Button>())
        {
            if (m_cardAbilityMap.TryGetValue(card, out var callback))
                card.UnregisterCallback(callback);
        }
        m_cardAbilityMap.Clear();
    }
}
