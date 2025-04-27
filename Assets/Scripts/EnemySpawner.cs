using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("--- Spawner Settings ---")]
    public GameObject enemyPrefab;
    public float spawnRate = 2.0F;
    private float nextSpawnTime = 0F;
    public int wave = 0;
    public float waveInterval = 10.0F;
    public Transform[] spawnPoints;
    public PlayerController playerController;

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
        int randomIndex = Random.Range(0, spawnPoints.Length);
        
        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }

    void IncreaseWaveDifficulty()
    {
        wave++;
        if (playerController.currentHealth < 3)
        {
            playerController.currentHealth++;
        }
        else
        {
            return;
        }
            spawnRate = Mathf.Max(0.5F, spawnRate - 0.1F);
        Debug.Log($"Wave {wave} incoming!");
    }
}