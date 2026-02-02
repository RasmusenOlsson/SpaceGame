using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float airSpeedMult = 0.4f;
    public float groundDrag = 5f;

    [Header("Gravity")]
    public Vector3 gDirection = Vector3.down;
    public float gStrength = 20f;
    public float gRotateSpeed = 6f;
    public KeyCode gravityKeybind = KeyCode.G;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    private bool gControl = false;
    private bool isGrounded = false;

    private float gravityChangeCooldown = 1f;
    private float gravityChangeTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(gravityKeybind))
            gControl = !gControl;

        if (gControl)
        {
            HandleGravityCardinalInput();
            gravityChangeTimer = gravityChangeCooldown;
        }

        if (gravityChangeTimer > 0f)
            gravityChangeTimer -= Time.deltaTime;

        GroundCheck();
        ControlDrag();
    }

    void FixedUpdate()
    {
        ApplyGravity();
        MovePlayer();
        RotatePlayerToGravity();
        LimitSpeed();
    }

    
    void MovePlayer()
    {
        Vector3 moveDir = transform.forward * vertical + transform.right * horizontal;
        Vector3 projectedMove = Vector3.ProjectOnPlane(moveDir, gDirection).normalized;

        float multiplier = isGrounded ? 1f : airSpeedMult;
        rb.AddForce(projectedMove * moveSpeed * 10f * multiplier, ForceMode.Force);
    }

    void LimitSpeed()
    {
        Vector3 surfaceVel = Vector3.ProjectOnPlane(rb.linearVelocity, gDirection);

        if (surfaceVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = surfaceVel.normalized * moveSpeed;
            rb.linearVelocity = limitedVel + Vector3.Project(rb.linearVelocity, gDirection);
        }
    }

    void ControlDrag()
    {
        Vector3 surfaceVel = Vector3.ProjectOnPlane(rb.linearVelocity, gDirection);

        if (isGrounded)
            rb.linearVelocity -= surfaceVel * (groundDrag * Time.deltaTime);
    }

    
    void ApplyGravity()
    {
        rb.AddForce(gDirection * gStrength, ForceMode.Acceleration);
    }

    void RotatePlayerToGravity()
    {
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -gDirection) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, gRotateSpeed * Time.fixedDeltaTime);
    }

   
    void GroundCheck()
    {
        if (gravityChangeTimer > 0f)
        {
            isGrounded = false;
            return;
        }

        float checkDist = playerHeight * 0.5f + 0.3f;
        RaycastHit hit;

        isGrounded = Physics.Raycast(transform.position, -gDirection, out hit, checkDist, groundMask);
    }
    void HandleGravityCardinalInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetGravityCardinal("down");
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetGravityCardinal("up");
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetGravityCardinal("forward");
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetGravityCardinal("back");
        if (Input.GetKeyDown(KeyCode.Alpha5)) SetGravityCardinal("right");
        if (Input.GetKeyDown(KeyCode.Alpha6)) SetGravityCardinal("left");
    }

    public void SetGravityCardinal(string direction)
    {
        switch (direction.ToLower())
        {
            case "down": gDirection = Vector3.down; break;
            case "up": gDirection = Vector3.up; break;
            case "forward": gDirection = Vector3.forward; break;
            case "back": gDirection = Vector3.back; break;
            case "right": gDirection = Vector3.right; break;
            case "left": gDirection = Vector3.left; break;
            default:
                break;
        }
    }
}
