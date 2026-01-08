using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Base Stats")]
    [SerializeField] private float baseDamage = 10f;
    [SerializeField] private float baseHealth = 100f;
    [SerializeField] private float baseMoveSpeed = 5f;

    public float CurrentDamage { get; private set; }
    public float CurrentHealth { get; private set; }
    public float CurrentMoveSpeed { get; private set; }

    public void AddDamage(float amount) => CurrentDamage += amount;
    public void MultiplyDamage(float factor) => CurrentDamage *= factor;
    public void AddHealth(float amount) => CurrentHealth += amount;
    public void AddMoveSpeed(float amount) => CurrentMoveSpeed += amount;


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

    public void ResetStats()
    {
        CurrentDamage = baseDamage;
        CurrentHealth = baseHealth;
        CurrentMoveSpeed = baseMoveSpeed;
    }
}
