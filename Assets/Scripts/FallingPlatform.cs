using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float reactionTime = 0.5f;
    public float destroyTime = 1f;
    private bool isFalling = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !isFalling)
        {
            isFalling = true;
            Invoke("StartFalling", reactionTime);
        }
    }

    private void StartFalling()
    {
        Rigidbody2D rb = transform.parent.GetComponent<Rigidbody2D>();
        if(rb == null)
        {
            rb = transform.parent.gameObject.AddComponent<Rigidbody2D>();
        }
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(transform.parent.gameObject, destroyTime);
    }
}
