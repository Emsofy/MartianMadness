using UnityEngine;
using UnityEngine.InputSystem; // Required for the new system

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called via the Player Input component (Message: OnMove)
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>(); // Captures WASD/Joystick input
    }

    void FixedUpdate()
    {
        // Apply velocity based on input and speed
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
    }
}
