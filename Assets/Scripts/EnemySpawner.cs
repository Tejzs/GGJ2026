using System.Collections;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Spawn Range")]
    public float minX = -8f;
    public float maxX = 8f;
    public float spawnY = 0f;

    [Header("Timing")]
    public float timeBetweenSpawns = 0.5f;
    public float timeBetweenWaves = 3f;

    private int[] waveEnemyCounts = { 2, 4, 6, 8 };
    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        // Stop after 4 waves
        if (currentWaveIndex >= waveEnemyCounts.Length)
        {
            Debug.Log("All waves completed!");
            yield break;
        }

        int enemiesToSpawn = waveEnemyCounts[currentWaveIndex];
        Debug.Log($"Spawning Wave {currentWaveIndex + 1} ({enemiesToSpawn} enemies)");

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(minX, maxX);
        Vector2 spawnPos = new Vector2(randomX, spawnY);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemiesAlive++;
    }

    public void EnemyDied()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            currentWaveIndex++;

            if (currentWaveIndex < waveEnemyCounts.Length)
            {
                StartCoroutine(NextWave());
            }
            else
            {
                Debug.Log("All waves cleared 🎉");
            }
        }
    }

    IEnumerator NextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave());
    }
}