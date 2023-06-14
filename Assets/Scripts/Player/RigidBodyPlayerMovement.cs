using UnityEngine;
//D0c
public class RigidBodyPlayerMovement : MonoBehaviour
{
    public static RigidBodyPlayerMovement Instance;

    //----COMPONENTS----
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CapsuleCollider groundingCol;
    [SerializeField] private Transform orientation;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private PlayerMovementSettings movementSettings;

    [Header("Inputs")]
    [SerializeField] private KeyCode crouchKey;
    [SerializeField] private KeyCode jumpKey;

    private float interactBufferCounter;
    [SerializeField] public bool grounded;
    [HideInInspector] public bool isCrouching = false;

    //----Movement related stuff----
    public bool movementFreeze = false;
    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;
    private RaycastHit slopeHit;

    // Jump Related
    public bool readyToJump = true;
    public float coyoteTimeCounter;
    public float jumpBufferCounter;

    private void Awake()
    {
        Instance = this;
        rb.freezeRotation = true;
    }

    //----MOVEMENT STUFF----
    private void Update()
    {
        CheckIfGrounded(false);
        GetInput();
    }

    public void GetInput()
    {
        // Forwards Sideways movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Crouching
        if (Input.GetKeyDown(crouchKey)) Crouch(true);
        if (Input.GetKeyUp(crouchKey)) Crouch(false);

        // Jumping
        if (Input.GetKey(jumpKey)) { jumpBufferCounter = movementSettings.jumpBufferTime; }
        else jumpBufferCounter -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (movementFreeze) return;

        SpeedCap();
        ApplyDrag();

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0 && readyToJump)
        {
            readyToJump = false;
            Jump();
            Invoke("ResetJump", movementSettings.jumpCooldown);
        }

        else if (OnSlope()) ApplyMovement(GetSlopeMoveDirection());
        else
        {
            ApplyMovement(orientation.forward);
            IncreaseFallGravity(movementSettings.gravity);
        }
        rb.useGravity = !OnSlope();
    }


    private void ApplyMovement(Vector3 trueForward)
    {
        moveDirection = trueForward * verticalInput + orientation.right * horizontalInput;

        if (grounded) rb.AddForce(moveDirection.normalized * movementSettings.moveSpeed * 10, ForceMode.Force);
        else rb.AddForce(moveDirection.normalized * movementSettings.airMultiplier * 10, ForceMode.Force);
    }

    private void ApplyDrag()
    {
        if (grounded) rb.drag = movementSettings.groundDrag;
        else rb.drag = movementSettings.airDrag;
    }

    public void FreezePlayerMovement(bool state)
    {
        movementFreeze = state;
        rb.useGravity = !state;
        rb.velocity = Vector3.zero;
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * movementSettings.jumpForce, ForceMode.Impulse);
    }

    //----CHECKS----

    public void CheckIfGrounded(bool resimulating)
    {
        bool onGround = Physics.Raycast(groundCheck.position, Vector3.down, movementSettings.groundCheckHeight, ground);
        grounded = onGround;

        if (grounded) coyoteTimeCounter = movementSettings.coyoteTime;
        else coyoteTimeCounter -= Time.deltaTime;
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void SpeedCap()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (!isCrouching)
        {
            if (flatVel.magnitude > movementSettings.moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * movementSettings.moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        else
        {
            if (flatVel.magnitude > movementSettings.moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * (movementSettings.moveSpeed * movementSettings.crouchedSpeedMultiplier);
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void IncreaseFallGravity(float force)
    {
        rb.AddForce(Vector3.down * force);
    }

    private void Crouch(bool state)
    {
        isCrouching = state;
        if (state)
        {
            groundingCol.height = groundingCol.height / 2;
            groundCheck.localPosition = new Vector3(0, groundCheck.localPosition.y / 2, 0);
        }
        else
        {
            groundingCol.height = groundingCol.height * 2;
            groundCheck.localPosition = new Vector3(0, groundCheck.localPosition.y * 2, 0);
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(groundCheck.position, Vector3.down, out slopeHit, movementSettings.groundCheckHeight))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < movementSettings.maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(orientation.forward, slopeHit.normal).normalized;
    }
}
