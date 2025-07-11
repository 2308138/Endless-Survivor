using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using System.Collections;
using NUnit.Framework;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public Text waveText;

    [Header("--- Spawner Settings ---")]
    public GameObject enemyPrefab;
    public float spawnRate = 2.0F;
    private float nextSpawnTime = 0F;
    public int wave = 0;
    public float waveInterval = 10.0F;
    public Transform[] spawnPoints;
    public PlayerController playerController;

    [Header("--- Tutorial Settings ---")]
    public Image moveImage;
    public Image dashImage;
    public Image attackImage;
    public GameObject tutorialPanel;
    private bool moved = false;
    private bool attacked = false;
    public bool tutorialComplete = false;
    private float inactiveAlpha = 0.5F;
    private float activeAlpha = 1.0F;

    private void Start()
    {
        if (wave == 0)
        {
            UpdateTutorialVisuals();
        }
        else
        {
            SpawnEnemy();
        }
    }

    private void Update()
    {
        if (!tutorialComplete && wave == 0)
        {
            HandleTutorial();
            return;
        }

        waveText.text = $"Wave: {wave}";

        if (tutorialComplete)
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

        if (wave == 16)
        {
            SceneManager.LoadScene("GameComplete", LoadSceneMode.Single);
        }
    }

    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }

    void IncreaseWaveDifficulty()
    {
        FindObjectOfType<ScoreManager>().IncreaseScore(100);
        wave++;
        if (playerController.currentHealth < 3)
        {
            playerController.currentHealth++;
        }
        else
        {
            return;
        }

        spawnRate = Mathf.Max(0.1F, spawnRate - 0.1F);
        Debug.Log($"Wave {wave} incoming!");
    }

    void HandleTutorial()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            moved = true;
            Debug.Log("Moved!");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            attacked = true;
            Debug.Log("Attacked!");
        }

        UpdateTutorialVisuals();

        if (moved && attacked)
        {
            CompleteTutorial();
        }
    }

    void UpdateTutorialVisuals()
    {
        SetAlpha(moveImage, moved ? activeAlpha : inactiveAlpha);
        SetAlpha(attackImage, attacked ? activeAlpha : inactiveAlpha);
    }

    void SetAlpha(Image img, float alpha)
    {
        Color c = img.color;
        c.a = alpha;
        img.color = c;
    }

    void CompleteTutorial()
    {
        tutorialComplete = true;
        Debug.Log("Completed Tutorial!");
        tutorialPanel.SetActive(false);
    }
}