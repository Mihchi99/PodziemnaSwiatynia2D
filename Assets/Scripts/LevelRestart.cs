using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
    public void LoadCurrentScene()
    {
        PauseMenu.canPause = true;
        PlayerMovement.isFrozen = false;
        CheckPoint.savedPosition = Vector2.zero;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
