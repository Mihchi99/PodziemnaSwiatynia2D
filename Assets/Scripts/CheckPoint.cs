using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Vector2 savedPosition = Vector2.zero;
    private AudioSource audioSource;
    public AudioClip checkPointClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(checkPointClip, 0.5f);
            savedPosition = collision.transform.position;
            
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if(player != null)
            {
                player.health = 3;
                player.UpdateHealthDisplay();
            }
        }
    }
}
