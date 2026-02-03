using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject finishUI;
    public int levelNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerMovement.isFrozen = true;
            PauseMenu.canPause = false;
            finishUI.SetActive(true);
            LevelProgress.UnlockNextLevel(levelNumber);
            Time.timeScale = 0f;
        }
    }
}
