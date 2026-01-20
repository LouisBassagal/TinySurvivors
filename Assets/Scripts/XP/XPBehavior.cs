using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class XPBehavior : MonoBehaviour
{
    [Header("Bounce Animation Settings")]
    [SerializeField]
    private AnimationCurve m_bounceCurve;
    [SerializeField]
    private float m_idleAnimationDuration = 2f;
    [SerializeField]
    private float m_spawnAnimationDuration = 1f;

    public void Start()
    {
        // StartIdleAnimation();
    }

    public void StartAnimationToPosition(Vector3 targetPosition)
    {
        Debug.Log("Starting XP animation to position: " + targetPosition);
        StopAnimation();
        transform.DOJump(targetPosition, 1, 3, m_spawnAnimationDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => StartIdleAnimation());
    }

    private void StartIdleAnimation()
    {
        transform.DOMoveY(transform.position.y + 0.2f, m_idleAnimationDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetRelative(false);
    }

    private void StopAnimation()
    {
        transform.DOKill();
    }
}
