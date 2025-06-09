using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Transform orientation;

    private Vector2 inputDirection;
    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Calculate 3D direction based on input
        // moveDirection = orientation.forward * inputDirection.y + orientation.right * inputDirection.x;
        moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
    }

    // private void FixedUpdate()
    // {
    //     if (moveDirection.magnitude > 0.01f)
    //     {
    //         Debug.Log("Applying force: " + moveDirection);
    //     }

    //     rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    // }
    private void FixedUpdate()
    {
        if (moveDirection.magnitude > 0.01f)
        {
            Debug.Log("Applying velocity: " + moveDirection);
        }

        rb.linearVelocity = moveDirection.normalized * moveSpeed;
    }

    // This will be called automatically when the input system detects movement
    public void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
        Debug.Log("InputDirection: " + inputDirection);
    }
}