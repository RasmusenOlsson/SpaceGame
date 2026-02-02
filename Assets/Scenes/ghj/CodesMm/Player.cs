using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRigidbodyMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float groundDrag = 5f;
    public float airControlMultiplier = 0.4f;

    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    private bool isGrounded;

    public float playerHeight = 2f;
    public LayerMask groundMask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Get input (do NOT move Rigidbody here)
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        GroundCheck();
        ControlDrag();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;

        if (isGrounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airControlMultiplier, ForceMode.Force);
    }

    void ControlDrag()
    {
        rb.linearDamping = isGrounded ? groundDrag : 0f;
    }

    void GroundCheck()
    {
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            playerHeight * 0.5f + 0.3f,
            groundMask
        );
    }
}
