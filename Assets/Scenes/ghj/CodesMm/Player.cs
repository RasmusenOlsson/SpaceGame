using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityPlayer : MonoBehaviour
{
    [Header("View Reference")]
    public Transform viewTransform;

    [Header("Sounds")]
    [SerializeField] private AudioClip landSoundEffect;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float airSpeedMult = 0.4f;
    public float groundDrag = 5f;
    public float jumpForce = 6f;

    [Header("Gravity")]
    public Vector3 gDirection = Vector3.down;
    public float gStrength = 20f;
    public float gRotateSpeed = 6f;
    public KeyCode gravityKeybind = KeyCode.G;

    [Header("Allowed Gravity Directions")]
    public bool enableDown = true;
    public bool enableUp = true;
    public bool enableForward = true;
    public bool enableBack = true;
    public bool enableRight = true;
    public bool enableLeft = true;

    [Header("Gravity Cooldown")]
    public float gravityCooldown = 0.75f;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    public bool Grounded;
    public float GroundedCheckDistance;
    private float bufferCheckDistance = 0.1f;
    
    private bool gravityControlActive = false;
    private bool gravityReady = true;
    private float gravityCooldownTimer = 0f;
    RaycastHit hit;
    public Vector3 ratcast = Vector3.down;
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

        if (gravityCooldownTimer > 0f)
            gravityCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(gravityKeybind) && gravityCooldownTimer <= 0f)
        {
            SoundManager.instance.PlaySoundFXclip(landSoundEffect, transform, 1f);
            gravityControlActive = !gravityControlActive;
            gravityReady = gravityControlActive;
        }

        if (gravityControlActive && gravityReady && gravityCooldownTimer <= 0f)
            HandleGravityInput();

        GroundedCheckDistance = (GetComponent<CapsuleCollider>().height / 2) + bufferCheckDistance;

        if (Physics.Raycast(transform.position, ratcast, out hit, GroundedCheckDistance))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }

    }


    void FixedUpdate()
    {
        ApplyGravity();
        MovePlayer();
        RotateToGravity();
        LimitSpeed();
        ControlDrag();
    }

    void MovePlayer()
    {
        Vector3 gDir = gDirection;
        Vector3 viewForward = Vector3.ProjectOnPlane(viewTransform.forward, gDir).normalized;
        Vector3 viewRight = Vector3.Cross(-gDir, viewForward).normalized;

        Vector3 moveDir = viewForward * vertical + viewRight * horizontal;

        float multiplier = Grounded ? 1f : airSpeedMult;
        rb.AddForce(moveDir * moveSpeed * 10f * multiplier, ForceMode.Force);
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
        if (!Grounded) return;

        Vector3 surfaceVel = Vector3.ProjectOnPlane(rb.linearVelocity, gDirection);
        rb.linearVelocity -= surfaceVel * (groundDrag * Time.fixedDeltaTime);
    }

    void ApplyGravity()
    {
        rb.AddForce(gDirection * gStrength, ForceMode.Acceleration);
    }

    void RotateToGravity()
    {
        Quaternion targetRotation =
            Quaternion.FromToRotation(transform.up, -gDirection) * transform.rotation;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            gRotateSpeed * Time.fixedDeltaTime
        );
    }

    void HandleGravityInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && enableDown) SetGravity(Vector3.down);
        if (Input.GetKeyDown(KeyCode.Alpha2) && enableUp) SetGravity(Vector3.up);
        if (Input.GetKeyDown(KeyCode.Alpha3) && enableForward) SetGravity(Vector3.forward);
        if (Input.GetKeyDown(KeyCode.Alpha4) && enableBack) SetGravity(Vector3.back);
        if (Input.GetKeyDown(KeyCode.Alpha5) && enableRight) SetGravity(Vector3.right);
        if (Input.GetKeyDown(KeyCode.Alpha6) && enableLeft) SetGravity(Vector3.left);
    }

    void SetGravity(Vector3 dir)
    {
        gDirection = dir;
        gravityReady = false;
        gravityControlActive = false;
        gravityCooldownTimer = gravityCooldown;
        ratcast = dir;
    }

}
