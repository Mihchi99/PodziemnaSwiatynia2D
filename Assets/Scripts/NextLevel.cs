using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string nextLevelName;

    public void LoadNextLevel()
    {
        CheckPoint.savedPosition = Vector2.zero;
        SceneManager.LoadScene(nextLevelName);
        Time.timeScale = 1;
    }
}