using UnityEngine;
using UnityEngine.InputSystem; // Required for the new system

public class Movement : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public Animator animator;  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Called via the Player Input component (Message: OnMove)
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>(); // Captures WASD/Joystick input
    }

    void Update()
    {
        // Apply velocity based on input and speed
        //rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        //    rb.linearVelocity = moveInput * moveSpeed;

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        transform.Translate(moveInput);

        if(inputX != 0)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }
        else if (inputX == 0) 
        {

            animator.SetBool("isWalking", false);
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }

        if (inputY != 0)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }
        else if (inputX == 0)
        {

            animator.SetBool("isWalking", false);
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }

    }
    /*public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
      
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }*/
}
    

