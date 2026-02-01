using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyWaveSpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<EnemyWaveSpawner>();
    }

    public void Die()
    {
        spawner.EnemyDied();
        Destroy(gameObject);
    }
}