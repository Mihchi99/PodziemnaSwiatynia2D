using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public void LoadNextLevel()
    {
        PauseMenu.canPause = true;
        PlayerMovement.isFrozen = false;
        CheckPoint.savedPosition = Vector2.zero;
        
        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentLevel = int.Parse(currentSceneName.Replace("Level", ""));
        int nextLevel = currentLevel + 1;
        
        SceneManager.LoadScene("Level" + nextLevel);
        Time.timeScale = 1f;
    }
}
