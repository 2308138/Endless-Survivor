using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public Button settingsButton;
    public Button creditsButton;
    public Button backButton;
    public Button quitButton;
    public AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;

        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);

        settingsButton.onClick.AddListener(OpenSettings);
        creditsButton.onClick.AddListener(OpenCredits);
        backButton.onClick.AddListener(CloseScreens);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseScreens()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        if (mainMenuPanel)
        {
            Application.Quit();
        }
    }
}