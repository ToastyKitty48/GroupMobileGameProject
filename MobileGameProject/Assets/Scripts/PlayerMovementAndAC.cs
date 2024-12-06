using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementAndAC : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5f; // Speed for horizontal movement when walking
    [SerializeField] private float jumpForce = 10f; // Force applied when jumping
    [SerializeField] private float shimmySpeed = 3f; //Speed for horizontal movement on shimmy bar
    [SerializeField] InputActionReference moveActionReference;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint; // Transform for ground check position
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius for ground check
    [SerializeField] private LayerMask groundLayer; // Layer mask for detecting ground

    [Header("Shimmy Check")]
    [SerializeField] private Transform handCheckPoint; // Transform for hand check position
    [SerializeField] private float handCheckRadius = 0.2f; // Radius for ground check
    [SerializeField] private LayerMask shimmyLayer; // Layer mask for detecting shimmy points

    bool jumpHold = false; //Bool for holding jump press
    float jumpHoldTimer; //Time jump bool will be true after press
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private bool isGrounded; // To check if the player is grounded
    private bool isOnBar = false; // To check if the player is on a shimmy bar
    private float playerGrav; //The players RB gravity scale
    public Transform rightCheckPointPos; //Position of right point for shimmy bar (assinged by "ShimmyBarCode" script)
    public Transform leftCheckPointPos; //Position of left point for shimmy bar (assinged by "ShimmyBarCode" script)
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialize the Rigidbody2D component
        playerGrav = rb.gravityScale; // Stores player gravity scale for later use
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        //Check if player hand collided with shimmy bar
        isOnBar = Physics2D.OverlapCircle(handCheckPoint.position, handCheckRadius, shimmyLayer);

        // Handle horizontal movement
        if (!isOnBar)
        {
            Debug.Log("Not on bar");
            float moveInput = moveActionReference.action.ReadValue<Vector2>().x;
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            rb.gravityScale = playerGrav;
        }

        // Handles shimmying
        if (isOnBar)
        {
            Vector3 rightPoint;
            Vector3 leftPoint;
            bool ran = false;
            float moveInput = moveActionReference.action.ReadValue<Vector2>().x;
            if (!ran)
            {
                float handPos = handCheckPoint.position.y;
                float right = rightCheckPointPos.position.y;
                float left = leftCheckPointPos.position.y;
                rightPoint = (new Vector3(rightCheckPointPos.position.x, rightCheckPointPos.position.y - handPos, rightCheckPointPos.position.z));
                leftPoint = (new Vector3(leftCheckPointPos.position.x, leftCheckPointPos.position.y - handPos, leftCheckPointPos.position.z));
            }
            if (moveInput > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, rightPoint, shimmySpeed * Time.deltaTime);
            }
            if (moveInput < 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, leftPoint, shimmySpeed * Time.deltaTime);
            }
            
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.gravityScale = 0;
            Debug.Log("On bar");
        }

        // Handle jumping
        if (jumpHold && jumpHoldTimer >= 0)
        {
            jumpHoldTimer -= Time.deltaTime;
            if (jumpHoldTimer <= 0)
            {
                jumpHold = false;
            }
        }
        if (isGrounded && jumpHold)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    public void Jump()
    {
        jumpHold = true;
        jumpHoldTimer = 0.2f;
    }

    void OnDrawGizmos()
    {
        // Draw a gizmo for the ground check in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(handCheckPoint.position, handCheckRadius);
    }
}
