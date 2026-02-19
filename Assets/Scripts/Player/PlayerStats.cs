using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Base Stats")]
    [SerializeField] private PlayableCharacterSO characterData;

    public float CurrentDamage { get; private set; }
    public float CurrentHealth { get; private set; }
    public float CurrentMoveSpeed { get; private set; }
    public float CurrentLuck { get; private set; }
    public int CurrentLevel { get; private set; } = 0;
    public int CurrentXP { get; private set; } = 0;
    public int XPToNextLevel { get; private set; } = 0;
    public Dictionary<string, AbilityBase> AbilityLevels { get; private set; } = new Dictionary<string, AbilityBase>();

    public void AddDamage(float amount) => CurrentDamage += amount;
    public void MultiplyDamage(float factor) => CurrentDamage *= factor;
    public void AddHealth(float amount) => CurrentHealth += amount;
    public void AddMoveSpeed(float amount) => CurrentMoveSpeed += amount;

    public Action OnLevelUp;
    public Action OnXPAdded;

    private void Awake()
    {
        Instance = this;
        if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        ResetStats();
        XPToNextLevel = CalculateXPToNextLevel(CurrentLevel);
    }

    private void Update()
    {
        if (CurrentXP < XPToNextLevel)
            return;
        LevelUp();
    }

    private void LevelUp()
    {
        CurrentLevel += 1;
        CurrentXP -= XPToNextLevel;
        XPToNextLevel = CalculateXPToNextLevel(CurrentLevel);
        Debug.Log("Leveled up to level " + CurrentLevel);
        OnLevelUp?.Invoke();
    }

    private int CalculateXPToNextLevel(int x)
    {
        return (int)Math.Truncate(3 * Math.Pow(x, 2) + 10);
    }

    public void ResetStats()
    {
        CurrentDamage = characterData.baseDamage;
        CurrentHealth = characterData.baseHealth;
        CurrentMoveSpeed = characterData.baseMoveSpeed;
        CurrentLuck = characterData.baseLuck;
    }

    public void AddXP(int amount)
    {
        CurrentXP += amount;
        OnXPAdded?.Invoke();
    }

    public void SetAbility(AbilitySO ability)
    {
        if (AbilityLevels.TryGetValue(ability.name, out AbilityBase abilityBase))
        {
            abilityBase.LevelUp();
            return;
        }
        CreateNewAbility(ability);
    }

    private void CreateNewAbility(AbilitySO ability)
    {
        var abilityContainer = transform.Find("Abilities");
        GameObject newAbilityGO = Instantiate(ability.Prefab, transform.position, Quaternion.identity, abilityContainer);
        var abilityComponent = newAbilityGO.GetComponent<AbilityBase>();

        abilityComponent.Initialize(ability);
        AbilityLevels[ability.name] = abilityComponent;
    }

    public int GetAbilityLevel(string abilityName)
    {
        return AbilityLevels.TryGetValue(abilityName, out AbilityBase abilityBase) ? abilityBase.Level : 0;
    }
}
