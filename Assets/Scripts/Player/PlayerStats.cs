using System;
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
    public int CurrentLevel { get; private set; } = 1;
    public int CurrentXP { get; private set; } = 0;
    public int XPToNextLevel { get; private set; } = 100;

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
        OnLevelUp?.Invoke();
    }

    private int CalculateXPToNextLevel(int x)
    {
        return (int)Math.Truncate(0.25 * x + 10);
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
}
