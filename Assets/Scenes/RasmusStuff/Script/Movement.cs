using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;

    public float groundBrag;

    [Header("Ground Check")]

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 movedIrection;

    Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
        MyInput();
        SpeedControl();

        //Handle the drag
        if (grounded)
            rb.linearDamping = groundBrag;
        else
            rb.linearDamping = 0f;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        //Calculate movement birection

        movedIrection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(movedIrection.normalized * moveSpeed * 10f, ForceMode.Force);
    }


    public void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }
}
