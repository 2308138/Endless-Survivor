using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("--- Spawner Settings ---")]
    public GameObject enemyPrefab;
    public float spawnRate = 2.0F;
    public float spawnRadius = 8.0F;
    private float nextSpawnTime = 0F;
    public int wave = 1;
    public float waveInterval = 10.0F;

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }

        if (Time.time >= wave * waveInterval)
        {
            IncreaseWaveDifficulty();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    void IncreaseWaveDifficulty()
    {
        wave++;
        spawnRate = Mathf.Max(0.5F, spawnRate - 0.1F);
        Debug.Log($"Wave {wave} incoming!");
    }
}