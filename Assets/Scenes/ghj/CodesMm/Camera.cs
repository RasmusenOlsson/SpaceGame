using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    public Transform cameraTransform;
    public float mouseSensitivity = 2.5f;

    float pitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        
        transform.Rotate(Vector3.up * mouseX);

        
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}

