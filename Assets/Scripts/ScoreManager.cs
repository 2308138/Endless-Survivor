using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    private void Update()
    {
        scoreText.text = $"Score: {score}";
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }
}