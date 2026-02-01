using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float stopDistance = 4f;

    [Header("Shooting")]
    public float shootRange = 6f;
    public float fireRate = 1.5f;
    public LayerMask playerLayer;
    public Transform firePoint;

    private Transform player;
    private float nextFireTime;
    private bool facingRight = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        FollowPlayer();
        ShootPlayer();
    }

    void FollowPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            // Separation: avoid overlapping other enemies
            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, 1f); // radius = 1 unit
            Vector2 separation = Vector2.zero;

            foreach (Collider2D col in nearbyEnemies)
            {
                if (col.gameObject != gameObject && col.CompareTag("Enemy"))
                {
                    Vector2 away = (Vector2)(transform.position - col.transform.position);
                    if (away != Vector2.zero)
                        separation += away.normalized / away.magnitude; // stronger push if closer
                }
            }

            Vector2 moveDir = (direction + separation).normalized;
            transform.position += new Vector3(moveDir.x, 0, 0) * moveSpeed * Time.deltaTime;
        }

        // Flip enemy
        if (player.position.x > transform.position.x && !facingRight)
            Flip();
        else if (player.position.x < transform.position.x && facingRight)
            Flip();
    }


    void ShootPlayer()
    {
        if (Time.time < nextFireTime) return;

        Vector2 direction = facingRight ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(
            firePoint.position,
            direction,
            shootRange,
            playerLayer
        );

        if (hit.collider != null)
        {
            PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        if (firePoint == null) return;

        Gizmos.color = Color.red;
        Vector2 dir = facingRight ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(firePoint.position, firePoint.position + (Vector3)(dir * shootRange));
    }
}
