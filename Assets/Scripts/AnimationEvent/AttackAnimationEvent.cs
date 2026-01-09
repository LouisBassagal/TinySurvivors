using UnityEngine;

public class AttackAnimationEvent : MonoBehaviour
{
    [Header("Attack Collider")]
    public GameObject attackCollider;

    public void EnableAttackCollider()
    {
        if (attackCollider != null)
        {
            attackCollider.SetActive(true);
        }
    }

    public void DisableAttackCollider()
    {
        if (attackCollider != null)
        {
            attackCollider.SetActive(false);
        }
    }
}
