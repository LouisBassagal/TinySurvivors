using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MapBorder : MonoBehaviour
{
    private Transform m_playerTransform;
    private Collider2D m_collider2D;


    void Awake()
    {
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        
        var closestPoint = m_collider2D.bounds.ClosestPoint(m_playerTransform.position);
        m_playerTransform.position = Vector2.Lerp(m_playerTransform.position, closestPoint, 1f);
    }    
}
