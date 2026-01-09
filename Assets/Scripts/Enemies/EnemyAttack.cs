using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private EnemySO enemyData;

    private void Start()
    {
        enemyData = transform.parent.GetComponent<Enemy>().enemyData;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        {
            if (collision.TryGetComponent<PlayerController>(out var player))
            {
                CameraController.Instance.ShakeCamera(.5f);
                player.Hit(this.transform.parent.transform, enemyData.damage);
            }
        }
    }
}
