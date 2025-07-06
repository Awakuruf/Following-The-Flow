using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float groundDrag = 5f;
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier = 0.4f;
    private bool readyToJump = true;

    [Header("Ground Check")]
    public float playerHeight = 6.0f;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;

    private Vector2 inputDirection;
    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        PerformGroundCheck();

        rb.linearDamping = grounded ? groundDrag : 0f;

        // Convert input to movement direction
        Vector3 inputDir = new Vector3(inputDirection.x, 0, inputDirection.y);
        moveDirection = orientation.forward * inputDir.z + orientation.right * inputDir.x;
    }

    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (grounded && readyToJump)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveForce = moveDirection.normalized * moveSpeed * 10f;

        if (grounded)
        {
            rb.AddForce(moveForce, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveForce * airMultiplier, ForceMode.Force);
        }

        Vector3 horizontalVelocity = moveDirection.normalized * moveSpeed;
        rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void PerformGroundCheck()
    {
        float rayLength = playerHeight / 2f + 0.3f; 
        grounded = Physics.Raycast(transform.position, Vector3.down, rayLength, whatIsGround);
    }
}