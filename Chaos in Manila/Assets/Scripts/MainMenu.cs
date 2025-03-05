using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenSettings()
    {
        Debug.Log("Open Settings");
    }

    public void OpenCredits()
    {
        Debug.Log("Open Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
