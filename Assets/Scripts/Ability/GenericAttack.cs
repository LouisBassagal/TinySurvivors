using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GenericAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy"))
            return;

        if (collision.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.Hit();
        }
    }
}
