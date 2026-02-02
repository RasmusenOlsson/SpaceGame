using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityController : MonoBehaviour
{
    public float GStrength = 20f;
    public float GChangeSpeed = 5f;
    public KeyCode GKeybind = KeyCode.G;

    private Rigidbody rb;
    private Vector3 GDirection = Vector3.down;
    private bool GControlOn = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void Update()
    {
       

        if (Input.GetKeyDown(GKeybind))
        {
            GControlOn = !GControlOn;
        }

        if (GControlOn)
        {
            HandleGravityInput();
        }
    }

    void FixedUpdate()
    {
        

        rb.AddForce(GDirection * GStrength, ForceMode.Acceleration);

        

        Quaternion targetRotation =
            Quaternion.FromToRotation(transform.up, -GDirection) * transform.rotation;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            GChangeSpeed * Time.fixedDeltaTime
        );
    }

    void HandleGravityInput()
    {
        Vector3 input = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) input += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) input += Vector3.back;
        if (Input.GetKey(KeyCode.A)) input += Vector3.left;
        if (Input.GetKey(KeyCode.D)) input += Vector3.right;
        if (Input.GetKey(KeyCode.Space)) input += Vector3.up;
        if (Input.GetKey(KeyCode.LeftControl)) input += Vector3.down;

        if (input != Vector3.zero)
            GDirection = input.normalized;
    }
}
