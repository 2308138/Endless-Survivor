using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public Text waveText;
    public EnemySpawner enemySpawner;

    private void Update()
    {
        FindObjectOfType<ScoreManager>().IncreaseScore(100);
        waveText.text = $"Wave: {enemySpawner.wave}";
    }
}