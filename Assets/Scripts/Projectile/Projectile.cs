using Mirror;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : NetworkBehaviour
{
    SpriteRenderer spriteRenderer;

    public GameObject enemy;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private int damageAmount;
    [HideInInspector] public Vector2 direction;
    [SerializeField] private float speed;
    public void LaunchProjectile(Vector2 newDirection, GameObject fireballEnemy)
    {
        if (enemy == null)
            enemy = fireballEnemy;

        direction = newDirection.normalized;

        rb.velocity = direction * speed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (enemy == null) return;
        if (other.gameObject != enemy) return;

        PlayerHealth playerHealth = enemy.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(damageAmount);

        Destroy(gameObject);
    }
}
