using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;

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
        horizontalInput = Input.GetAxisRaw("Horziontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    private void Update()
    {
        MyInput();
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
}
