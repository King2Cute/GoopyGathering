using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;

    [Header("References")]
    public Camera playerCamera;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float rotationX = 0f;
    private float rotationY = 0f;
    protected Rigidbody rb;
    protected bool isGround = false;
    private Transform modelTransform;

    [Header("Ground")]
    public float groundCheckRadius = 0.2f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        modelTransform = transform.GetChild(0).transform;
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        HandleCameraRotation();
        HandleMovement();
        HandleJumping();
    }

    protected void HandleCameraRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            rotationX -= mouseY * lookSpeedY;
            rotationY += mouseX * lookSpeedX;

            rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Prevent flipping

            // Apply rotation to the camera and player
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }
    }

    protected void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = modelTransform.forward;
        Vector3 right = modelTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 movementDirection = (forward * vertical + right * horizontal).normalized;

        Vector3 moveVelocity = new Vector3(movementDirection.x * moveSpeed, rb.velocity.y, movementDirection.z * moveSpeed);
        rb.velocity = moveVelocity;
        
        if (isGround)
        {
            modelTransform.rotation = Quaternion.Lerp(
            modelTransform.rotation, Quaternion.LookRotation(movementDirection, Vector3.up), 
            Time.deltaTime * moveVelocity.magnitude / 3);
        }
        
        /*  Code copied from my other proj for acceleration + deceleration (drag) for 2d movement - Joe

        // Capping speed
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        if (Mathf.Abs(rb.velocity.y) > maxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxSpeed);
        }

        // Applying drag on the horizontal and vertical axis seperately
        drag = rb.velocity;
        if (Mathf.Abs(hori) < 0.4f)
        {
            drag.x *= dragVal;
            rb.velocity = drag;
        }
        if (Mathf.Abs(vert) < 0.4f)
        {
            drag.y *= dragVal;
            rb.velocity = drag;
        }
        if (Mathf.Abs(hori) > 0.4f || Mathf.Abs(vert) > 0.4f)
        {
            drag = Vector2.zero;
        }*/
    }

    protected virtual void HandleJumping()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Reset vertical velocity
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    protected bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
