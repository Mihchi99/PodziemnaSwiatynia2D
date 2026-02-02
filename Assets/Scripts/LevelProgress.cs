using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    public static int unlockedLevel = 1;

    public static void UnlockNextLevel(int completedLevel)
    {
        int nextLevel = completedLevel + 1;
        if(nextLevel <= 6 && nextLevel > unlockedLevel)
        {
            unlockedLevel = nextLevel;
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);
            PlayerPrefs.Save();
        }
    }

    public static void LoadProgress()
    {
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    }

    public static void WipeProgress()
    {
        unlockedLevel = 1;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        CheckPoint.savedPosition = Vector2.zero;
    }
}