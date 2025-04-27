using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUI : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerController playerController;
    private List<GameObject> hearts = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < playerController.maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, transform);
            hearts.Add(heart);
        }
    }

    private void Update()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].SetActive(i < playerController.currentHealth);
        }
    }
}