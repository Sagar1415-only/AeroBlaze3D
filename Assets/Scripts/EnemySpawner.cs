using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float initialSpawnDelay = 2f;
    public float spawnInterval = 5f;       // how often to spawn new enemies
    public int initialEnemies = 2;         // start with this many
    public int maxEnemies = 15;            // total limit at once
    public Vector3 spawnArea = new Vector3(20, 0, 20);

    private int totalSpawned = 0;

    void Start()
    {
        // Spawn initial few enemies
        for (int i = 0; i < initialEnemies; i++)
        {
            SpawnEnemy();
        }

        // Keep spawning at a fixed interval
        InvokeRepeating(nameof(SpawnEnemy), initialSpawnDelay, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        // Count how many are currently in the scene
        int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // If not exceeding the limit, spawn a new one
        if (currentEnemies < maxEnemies)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                1f,
                Random.Range(-spawnArea.z, spawnArea.z)
            );

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            totalSpawned++;
        }
    }
}
