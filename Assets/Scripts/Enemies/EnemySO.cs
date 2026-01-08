using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "TinySurvivors/EnemySO")]
public class EnemySO : ScriptableObject
{
    [Header("Enemy Stats")]
    public string enemyName;
    public float maxHealth = 100f;
    public float moveSpeed = 3f;
    public float damage = 10f;
    public int experienceValue = 5;

    [Header("Combat")]
    public float attackRange = 1f;
    public float attackCooldown = 1f;
}
