using System.Collections;
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
    [SerializeField]
    private AnimationManager m_animationManager;

    private float currentHealth;
    private Transform m_playerTransform;
    private bool m_isDead = false;
    private float m_invincibilityTimer = 0.5f;
    private Collider2D m_collider;
    private Rigidbody2D m_rigidbody;

    void Start()
    {
        currentHealth = enemyData.maxHealth;
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (m_isDead)
            return;

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

        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x);
        if (m_playerTransform.position.x < transform.position.x)
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        else if (m_playerTransform.position.x > transform.position.x)
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
    }

    protected virtual void Die()
    {
        // Add death logic here (e.g., play animation, drop loot, etc.)
        m_animationManager.TriggerDeath();
        m_collider.enabled = false;
        m_isDead = true;
        StartCoroutine(FadeDeadBody());
    }

    public virtual void Hit()
    {
        if (!PlayerStats.Instance || m_isDead && m_invincibilityTimer > 0f)
            return;

        currentHealth -= PlayerStats.Instance.CurrentDamage;

        // Calculate knockback direction and apply force
        Vector2 knockbackDirection = (transform.position - m_playerTransform.position).normalized;
        m_rigidbody.AddForce(knockbackDirection * m_knockbackPower, ForceMode2D.Impulse);
        m_invincibilityTimer = 0.5f;

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        m_animationManager.TriggerHurt();
    }

    protected virtual IEnumerator FadeDeadBody()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        float fadeDuration = 5f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
