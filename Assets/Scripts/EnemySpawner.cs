using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("--- Spawner Settings ---")]
    public GameObject enemyPrefab;
    public float spawnRate = 2.0F;
    public float spawnRadius = 8.0F;
    private float nextSpawnTime = 0F;

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}