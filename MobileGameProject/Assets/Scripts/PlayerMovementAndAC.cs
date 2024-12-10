using Unity.VisualScripting;
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
    private bool touchingBar; // To check if the player is on a shimmy bar
    private float playerGrav; //The players RB gravity scale
    public Transform rightCheckPointPos; //Position of right point for shimmy bar (assinged by "ShimmyBarCode" script)
    public Transform leftCheckPointPos; //Position of left point for shimmy bar (assinged by "ShimmyBarCode" script)
    bool ran = false; //Bool to check if shimmy position code has ran yet
    Vector3 rightPoint; //Points for shimmy bar
    Vector3 leftPoint;
    float barGrabIFrames;
    bool isOnBar = false;
    bool barJump = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerGrav = rb.gravityScale; // Stores player gravity scale for later use
    }

    void FixedUpdate()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        //Check if player hand collided with shimmy bar
        touchingBar = Physics2D.OverlapCircle(handCheckPoint.position, handCheckRadius, shimmyLayer);
        barGrabIFrames -= Time.deltaTime;
        // Handle horizontal movement
        if (!isOnBar)
        {
            float moveInput = moveActionReference.action.ReadValue<Vector2>().x;
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            ran = false;
            //Restores gravity to original state after bar is let go of
            rb.gravityScale = playerGrav;
        }
        // Handles shimmying
        if (isOnBar && barGrabIFrames > 0)
        {
            isOnBar = false;
        }
        if (touchingBar && barGrabIFrames <= 0)
        {
            float handPos = handCheckPoint.localPosition.y;
            float moveInput = moveActionReference.action.ReadValue<Vector2>().x;
            if (!ran)
            {
                //Calculates the position of of bar an shimmy points;
                float right = rightCheckPointPos.position.y;
                float left = leftCheckPointPos.position.y;
                transform.position = new Vector3(transform.position.x, left - handPos, transform.position.z);
                rightPoint = (new Vector3(rightCheckPointPos.position.x, right - handPos, rightCheckPointPos.position.z));
                leftPoint = (new Vector3(leftCheckPointPos.position.x, left - handPos, leftCheckPointPos.position.z));
                ran = true;
                //Stops movement of player and turns off gravity
                rb.velocity = new Vector2(0, 0);
                rb.gravityScale = 0;
                isOnBar = true;
            }
            //Moves player towards shimmy points
            if (moveInput > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, rightPoint, shimmySpeed);
            }
            if (moveInput < 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, leftPoint, shimmySpeed);
            }
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
        if (isGrounded && jumpHold || jumpHold && barJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            barJump = false;
        }
    }
    public void Jump()
    {
        jumpHold = true;
        jumpHoldTimer = 0.2f;
        if (isOnBar)
        {
            barGrabIFrames = 1f;
            barJump = true;
        }
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
