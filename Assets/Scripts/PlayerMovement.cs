using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 4f;
    public float jumpForce = 10f;
    public int maxJumps = 2;
    private int jumpsRemaining = 2;
    private float horizontalMovement;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public int health = 3;
    public Image[] lifeIcons;
    private AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip damageClip;
    public AudioClip bounceClip;
    public AudioClip runClip;
    public AudioClip landClip;
    private bool isRunning = false;
    private bool isGrounded = false;
    public static bool isFrozen = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        UpdateHealthDisplay();

        if(CheckPoint.savedPosition != Vector2.zero)
        {
            transform.position = CheckPoint.savedPosition;
        }
    }

    void Update()
    {
        if(isFrozen)
        {
            return;
        }

        Vector2 newVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
        rb.linearVelocity = newVelocity;
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);

        if(!isGrounded && isRunning)
        {
            audioSource.Stop();
            isRunning = false;
        }

        if(horizontalMovement > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(horizontalMovement < 0)
        {
            spriteRenderer.flipX = true;
        }

        if(transform.position.y < -20)
        {
            Death();
        }
    }

    public void UpdateHealthDisplay()
    {
        for(int i = 0; i < lifeIcons.Length; i++)
        {
            if(i < health)
            {
                lifeIcons[i].enabled = true;
            }
            else
            {
                lifeIcons[i].enabled = false;
            }
        }
    }

    public void ResetMovement()
    {
        horizontalMovement = 0f;
        if(isRunning)
        {
            audioSource.Stop();
            isRunning = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(isFrozen)
        {
            return;
        }

        horizontalMovement = context.ReadValue<Vector2>().x;

        if(horizontalMovement != 0 && !isRunning && isGrounded)
        {
            audioSource.clip = runClip;
            audioSource.volume = 0.5f;
            audioSource.loop = true;
            audioSource.Play();
            isRunning = true;
        }
        else if(horizontalMovement == 0 && isRunning)
        {
            audioSource.Stop();
            isRunning = false;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(isFrozen)
        {
            return;
        }

        if(context.performed && jumpsRemaining > 0)
        {
            audioSource.PlayOneShot(jumpClip, 0.5f);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsRemaining--;
            animator.SetTrigger("jump");
            isGrounded = false;

            if(isRunning)
            {
                audioSource.Stop();
                isRunning = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            audioSource.PlayOneShot(landClip, 0.5f);
            jumpsRemaining = maxJumps;
            isGrounded = true;
        }
        else if(collision.gameObject.CompareTag("Damage"))
        {
            audioSource.PlayOneShot(damageClip, 0.5f);
            health -= 1;
            UpdateHealthDisplay();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsRemaining = 1;
            StartCoroutine(DamageEffect());
            if(health <= 0)
            {
                Death();
            }
        }
        else if(collision.gameObject.CompareTag("Bounce"))
        {
            audioSource.PlayOneShot(bounceClip, 0.5f);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 2);
        }
    }

    private IEnumerator DamageEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    private void Death()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
