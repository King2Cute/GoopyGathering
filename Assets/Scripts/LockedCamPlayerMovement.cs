using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedCamPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    float maxSpeed = 10;

    [SerializeField]
    float
        force = 10,
        groundDrag = 5,
        airDrag = 0,
        jumpForce = 5;

    bool
     canJump = true;
    Vector3
        moveDirection;
    protected Rigidbody
        rb;

    [Header("Model Rotation")]
    [SerializeField]
    Transform model;
    [SerializeField]
    float 
        minSpeedToRotateModel,
        rotationSpeedMultiplier = 1;

    [Header("Ground Check")]
    [SerializeField]
    Transform
        groundCheck;
    [SerializeField]
    float
        groundCheckRadius = .1f,
        jumpCooldown = .2f;
    [SerializeField]
    LayerMask
        groundLayer;
    protected bool
        isGrounded;
    
    //Inputs
    float
        hInput,
        vInput;

    [Header("Temp Inputs")]
    [SerializeField]
    protected KeyCode forward = KeyCode.W;
    [SerializeField]
    protected KeyCode back = KeyCode.S;
    [SerializeField]
    protected KeyCode left = KeyCode.A;
    [SerializeField]
    protected KeyCode right = KeyCode.D;
    [SerializeField]
    protected KeyCode jump = KeyCode.Space;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    protected void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        PlayerInput();
        MoveDirection();
        SpeedControl();

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        RotateModel();
    }

    void PlayerInput()
    {
        vInput = 0;
        hInput = 0;
        if (Input.GetKey(forward))
        {
            vInput += 1;
        }
        if(Input.GetKey(back))
        {
            vInput += -1;
        }
        if (Input.GetKey(left))
        {
            hInput += -1;
        }
        if (Input.GetKey(right))
        {
            hInput += 1;
        }
        DoJump();
    }

    protected virtual void DoJump()
    {
        if (Input.GetKey(jump) && canJump && isGrounded)
        {
            Jump();
        }
    }

    void MoveDirection()
    {
        moveDirection = transform.forward * vInput + transform.right * hInput;
        //if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * force * Time.fixedDeltaTime, ForceMode.Force);
        }
        SpeedControl();
    }

    void SpeedControl()
    {
        Vector3 vel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        vel = Vector3.ClampMagnitude(vel, maxSpeed);
        rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);

    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        canJump = false;
        Invoke("ResetJump", jumpCooldown);

    }

    void ResetJump()
    {
        canJump = true;
    }

    void RotateModel()
    {
        if (model == null)
            return;
        Vector3 velocity = rb.velocity;
        velocity.y = 0;

        if(velocity.magnitude > minSpeedToRotateModel)
        {
            //model.LookAt(transform.position + velocity * 100);
            model.rotation = Quaternion.Lerp(
            model.rotation, Quaternion.LookRotation(velocity * 100, Vector3.up),
            Time.deltaTime * velocity.magnitude * rotationSpeedMultiplier);
        }
    }

    private void OnDrawGizmos()
    {
        if (rb == null) // to fix error spam when not playing
        {
            return;
        }
        Vector3 velocity = rb.velocity;
        velocity.y = 0;
        Gizmos.DrawSphere(transform.position + velocity, .2f);
    }
}
