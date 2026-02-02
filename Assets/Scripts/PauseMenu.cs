using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;

    public void Pause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(container.activeSelf)
            {
                ResumeButton();
            }
            else
            {
                container.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void ResumeButton()
    {
        container.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}