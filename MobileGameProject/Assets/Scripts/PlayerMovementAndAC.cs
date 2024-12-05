using UnityEngine;

public class PlayerMovementAndAC : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5f; // Speed for horizontal movement
    [SerializeField] private float jumpForce = 10f; // Force applied when jumping

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint; // Transform for ground check position
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius for ground check
    [SerializeField] private LayerMask groundLayer; // Layer mask for detecting ground

    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private bool isGrounded; // To check if the player is grounded

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialize the Rigidbody2D component
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        // Handle horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Handle jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnDrawGizmos()
    {
        // Draw a gizmo for the ground check in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
