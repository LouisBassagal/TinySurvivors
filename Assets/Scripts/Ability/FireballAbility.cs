using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

class FireballAbility : AbilityBase
{
    [SerializeField]
    private GameObject m_fireballPrefab;
    private List<GameObject> m_fireballs = new();
    private readonly float m_radius = 0.5f;

    private void OnEnable()
    {
        OnLevelUp();
    }

    protected sealed override void OnLevelUp()
    {
        DOTween.Kill(transform);

        GameObject newFireball = Instantiate(m_fireballPrefab, transform.position, Quaternion.identity, this.transform);
        m_fireballs.Add(newFireball);
        PlaceFireballs();

        transform.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
    }

    private void PlaceFireballs()
    {
        int count = m_fireballs.Count;
        for (int i = 0; i < count; i++)
        {
            if (m_fireballs[i] != null)
            {
                float angle = i * Mathf.PI * 2 / count;
                Vector3 newPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * m_radius;
                m_fireballs[i].transform.localPosition = newPos;

                float facingAngle = angle * Mathf.Rad2Deg * 180f;
                m_fireballs[i].transform.localRotation = Quaternion.Euler(0, 0, facingAngle);
            }
        }
    }
}