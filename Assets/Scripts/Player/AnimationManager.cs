using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [Header("Animator Settings")]
    public Animator animator;

    private static readonly int m_attackHash = Animator.StringToHash("Attack");
    private static readonly int m_walkHash = Animator.StringToHash("IsWalking");
    private static readonly int m_hurtHash = Animator.StringToHash("Hurt");
    private static readonly int m_deathHash = Animator.StringToHash("Death");

    private void Awake()
    {
        if (animator != null)
        {
            Animator animator = GetComponent<Animator>();
        }
    }

    #region Movement

    public void SetMoving(bool isMoving)
    {
        animator.SetBool(m_walkHash, isMoving);
    }

    #endregion

    #region Action

    public void TriggerAttack()
    {
        animator.SetTrigger(m_attackHash);
    }

    public void TriggerHurt()
    {
        animator.SetTrigger(m_hurtHash);
    }

    public void TriggerDeath()
    {
        animator.SetTrigger(m_deathHash);
    }

    #endregion

    #region Utils

    public bool IsPlayingAnimation(string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    public float GetCurrentAnimationTime()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void SetAnimationSpeed(float speed)
    {
        animator.speed = speed;
    }

    #endregion
}
