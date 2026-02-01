using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 18f;
    public float lifeTime = 5f;
    public LayerMask enemyLayer;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        transform.Rotate(5f, 0.2f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }

            Destroy(gameObject);
        }
    }
}