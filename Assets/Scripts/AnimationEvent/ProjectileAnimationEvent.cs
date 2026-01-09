using UnityEngine;

public class ProjectileAnimationEvent : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectile;

    public void LaunchProjectileTowardPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (!player || !projectile)
            return;

        Vector2 spawnPosition = transform.parent.position;
        Vector2 direction = (player.transform.position - (Vector3)spawnPosition).normalized;
        var projectileInstance = Instantiate(projectile, spawnPosition, Quaternion.LookRotation(Vector3.forward, direction));
        projectileInstance.GetComponent<Projectile>().damage = transform.parent.GetComponent<Enemy>().enemyData.damage;
        projectileInstance.GetComponent<Rigidbody2D>().linearVelocity = direction * 10f;
        Destroy(projectileInstance, 10f);
    }
}
