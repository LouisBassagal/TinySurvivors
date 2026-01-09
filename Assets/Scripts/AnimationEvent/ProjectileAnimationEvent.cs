using UnityEngine;

public class ProjectileAnimationEvent : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectile;

    public void LaunchProjectileTowrdPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (!player || !projectile)
            return;

        Vector2 direction = (player.transform.position - projectile.transform.position).normalized;
        var projectileInstance = Instantiate(projectile, transform.parent.position, Quaternion.LookRotation(Vector3.forward, direction));
        projectileInstance.GetComponent<Rigidbody2D>().linearVelocity = direction * 10f;
    }
}
