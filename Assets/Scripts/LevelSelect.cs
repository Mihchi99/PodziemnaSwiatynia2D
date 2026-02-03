using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] levelButtons;
    public Button wipeProgressButton;

    void Start()
    {
        LevelProgress.LoadProgress();
        UpdateButtonStates();
    }

    void UpdateButtonStates()
    {
        for(int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;
            
            if(levelIndex <= LevelProgress.unlockedLevel)
            {
                levelButtons[i].interactable = true;
                ColorBlock colors = levelButtons[i].colors;
                colors.normalColor = Color.white;
                levelButtons[i].colors = colors;
            }
            else
            {
                levelButtons[i].interactable = false;
                ColorBlock colors = levelButtons[i].colors;
                colors.disabledColor = Color.gray;
                levelButtons[i].colors = colors;
            }
        }
    }

    public void LoadLevel(int levelNumber)
    {
        PauseMenu.canPause = true;
        CheckPoint.savedPosition = Vector2.zero;
        SceneManager.LoadScene("Level" + levelNumber);
    }

    public void WipeProgress()
    {
        LevelProgress.WipeProgress();
        UpdateButtonStates();
    }

    public void BackToMainMenu()
    {
        PauseMenu.canPause = true;
        SceneManager.LoadScene("MainMenu");
    }
}
