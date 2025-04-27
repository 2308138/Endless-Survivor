using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button retryButton;
    public Button homeButton;

    private void Start()
    {
        retryButton.onClick.AddListener(RetryGame);
        homeButton.onClick.AddListener(ReturnToHome);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void ReturnToHome()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}