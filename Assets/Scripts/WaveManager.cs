using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public Text waveText;
    public EnemySpawner enemySpawner;

    private void Update()
    {
        waveText.text = $"Wave: {enemySpawner.wave}";
    }
}