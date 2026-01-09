using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : Enemy
{
    private float m_attackTimer = 0f;

    protected override void Update()
    {
        if (m_isDead)
            return;
        
        m_attackTimer += Time.deltaTime;
        HandleMovement();
        HandleFacing();
    }

    protected override void HandleMovement()
    {
        if (!m_playerTransform)
            return;
        
        float distanceToPlayer = Vector2.Distance(transform.position, m_playerTransform.position);

        if (distanceToPlayer <= enemyData.attackRange)
        {
            m_animationManager.SetMoving(false);
            if (m_attackTimer >= enemyData.attackCooldown)
            {
                m_animationManager.TriggerAttack();
                m_attackTimer = 0f;
            }
            return;
        }
        Vector2 direction = (m_playerTransform.position - transform.position).normalized;
        transform.Translate(enemyData.moveSpeed * Time.deltaTime * direction);
        m_animationManager.SetMoving(true);
    }
}
