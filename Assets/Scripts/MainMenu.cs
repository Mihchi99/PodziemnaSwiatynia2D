using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        PauseMenu.canPause = true;
        CheckPoint.savedPosition = Vector2.zero;
        SceneManager.LoadScene("Level1");
    }

    public void LevelSelect()
    {
        PauseMenu.canPause = true;
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
