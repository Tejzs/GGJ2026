using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float jumpForce = 6;
    private float playerHalfHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHalfHeight = spriteRenderer.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && GetIsGrounded())
        {
            Jump();
        }
        
    }


    private bool GetIsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, playerHalfHeight + 0.1f, LayerMask.GetMask("Ground"));

    }


    private void Jump()
    {
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
