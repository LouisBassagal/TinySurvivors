using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public EnemySO enemyData;
    [SerializeField]
    private float m_knockbackPower = 50f;

    private float currentHealth;

    private Transform m_playerTransform;

    void Start()
    {
        currentHealth = enemyData.maxHealth;
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        HandleMovement();
        HandleFacing();
    }

    protected virtual void HandleMovement()
    {
        if (!m_playerTransform)
            return;
        
        Vector2 direction = (m_playerTransform.position - transform.position).normalized;
        transform.Translate(enemyData.moveSpeed * Time.deltaTime * direction);
    }

    protected virtual void HandleFacing()
    {
        if (!m_playerTransform)
            return;

        if (m_playerTransform.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (m_playerTransform.position.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
    }

    protected virtual void Die()
    {
        // Add death logic here (e.g., play animation, drop loot, etc.)
        Destroy(gameObject);
    }

    public virtual void Hit()
    {
        if (!PlayerStats.Instance)
            return;

        currentHealth -= PlayerStats.Instance.CurrentDamage;
        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        // Apply knockback
        Vector2 knockbackDirection = (transform.position - m_playerTransform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(knockbackDirection * m_knockbackPower, ForceMode2D.Impulse);
    }
}
