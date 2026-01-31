using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private Vector2 movement;
    private Vector2 screenBounds;
    private float playerHalfWidth;
    private float xPosLastFrame;
    private float input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Convert screen width and height into world unit
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        playerHalfWidth = spriteRenderer.bounds.extents.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        ClampMovement();
        FlipCharacterX();
    }


    private void FlipCharacterX()
    {
        if (input > 0 && (transform.position.x > xPosLastFrame))
        {
            // We are moving right
            spriteRenderer.flipX = false;
        } else if (input < 0 && (transform.position.x < xPosLastFrame))
        {
            // We are moving left
            spriteRenderer.flipX = true;
        }

        xPosLastFrame = transform.position.x;
    }

    private void ClampMovement()
    {
        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x + playerHalfWidth, screenBounds.x - playerHalfWidth);
        Vector2 pos = transform.position; // Get the player's current position
        pos.x = clampedX; // Reassign the X value to the clamped position
        transform.position = pos; // Reassign the clamped value back to the player
    }


    private void HandleMovement()
    {
        // input will store a value between -1 and +1
        // GetAxisRaw() takes exactly -1 or +1
        // GetAxis() takes a value between and up to -1 to +1 (useful for acceleration)
        // Getting the axis is mapped to A/D, left/right arrow
        input = Input.GetAxis("Horizontal");
        movement.x = input * speed * Time.deltaTime;
        transform.Translate(movement);
        if (input != 0)
        {
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }
    }

    // To move to new scene
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Door1")) {
            SceneManager.LoadScene("Level2"); // replace with current scene + 1 to move to next
        }

        // eventually add if collide with "enemy" move to battle scene
    }
}
