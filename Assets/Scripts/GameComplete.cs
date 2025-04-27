using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{
    public Button homeButton;

    private void Start()
    {
        homeButton.onClick.AddListener(ReturnToHome);
    }

    public void ReturnToHome()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}