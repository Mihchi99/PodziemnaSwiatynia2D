using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject container;
    public static bool canPause = true;

    public void Pause(InputAction.CallbackContext context)
    {
        if(context.performed && canPause)
        {
            if(container.activeSelf)
            {
                ResumeButton();
            }
            else
            {
                PlayerMovement.isFrozen = true;
                container.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void ResumeButton()
    {
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        if(player != null)
        {
            player.ResetMovement();
        }
        
        PlayerMovement.isFrozen = false;
        container.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenuButton()
    {
        PauseMenu.canPause = true;
        PlayerMovement.isFrozen = false;
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
