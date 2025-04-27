using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button resumeButton;
    public Button settingsButton;
    public Button homeButton;
    private bool isPaused = false;

    public GameObject settingsPanel;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        settingsPanel.SetActive(false);

        resumeButton.onClick.AddListener(ResumeGame);
        settingsButton.onClick.AddListener(OpenSettings);
        homeButton.onClick.AddListener(ReturnToHome);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0.0F;
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1.0F;
        pauseMenuUI.SetActive(false);
    }

    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        pauseMenuUI.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ReturnToHome()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}