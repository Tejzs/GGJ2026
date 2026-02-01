using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private string url = "https://scratch.mit.edu/projects/1273752334";
    [SerializeField] private float speed = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private Vector2 movement;
    private Vector2 screenBounds;
    private float playerHalfWidth;
    private float xPosLastFrame;
    private float input;
    private float y = 0;
    public Transform playerMask;

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
        FlipCharacterX();
    }


    private void FlipCharacterX()
    {
        if (input > 0 && (transform.position.x > xPosLastFrame))
        {
            // We are moving right
            spriteRenderer.flipX = false;
            playerMask.localPosition = new Vector3(0.15f, 0.8482001f, 1);
            
        } else if (input < 0 && (transform.position.x < xPosLastFrame))
        {
            // We are moving left
            spriteRenderer.flipX = true;
            playerMask.localPosition = new Vector3(-0.15f, 0.8482001f, 1);        }

        xPosLastFrame = transform.position.x;
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
        if (other.gameObject.CompareTag("Door2")) {
            SceneManager.LoadScene("Level3"); // replace with current scene + 1 to move to next
        }
        if (other.gameObject.CompareTag("Door3")) {
            SceneManager.LoadScene("Level4"); // replace with current scene + 1 to move to next
        }
        if (other.gameObject.CompareTag("Link")) {
        
            Application.OpenURL(url);
        }

        // eventually add if collide with "enemy" move to battle scene
    }
}
