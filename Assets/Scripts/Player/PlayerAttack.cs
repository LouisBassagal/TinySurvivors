using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PlayerAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;
        
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.Hit();
        }
    }
}
