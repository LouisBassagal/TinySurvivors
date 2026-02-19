using System.Collections.Generic;
using UnityEngine;

class FireballAbility : AbilityBase
{
    [SerializeField]
    private GameObject m_fireballPrefab;
    private List<GameObject> m_fireballs = new();

    private void Update()
    {
        ManageFireballRotation();
    }

    protected sealed override void OnLevelUp()
    {
        GameObject newFireball = Instantiate(m_fireballPrefab, transform.position, Quaternion.identity, this.transform);
        m_fireballs.Add(newFireball);
    }

    private void ManageFireballRotation()
    {
        foreach (var fireball in m_fireballs)
        {
            if (fireball != null)
            {
                fireball.transform.Rotate(Vector3.up, 360 * Time.deltaTime);
            }
        }
    }
}