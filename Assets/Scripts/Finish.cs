using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject finishUI;
    public int levelNumber;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            finishUI.SetActive(true);
            LevelProgress.UnlockNextLevel(levelNumber);
        }
    }
}