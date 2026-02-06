using UnityEngine;
using System.Collections;

public class WallButton : MonoBehaviour
{
    [Header("Interaction")]
    public KeyCode interactKey = KeyCode.E;
    public float interactDistance = 2f;
    public Transform playerCamera;

    [Header("Button movement")]
    public float pressDepth = 0.05f;
    public float pressSpeed = 12f;
    public float pressTime = 0.1f;

    public bool IsOn { get; private set; } // <-- DETTA styr lampor/dörrar

    Vector3 startPos;
    Vector3 pressedPos;
    bool isAnimating = false;

    void Start()
    {
        startPos = transform.localPosition;
        pressedPos = startPos - new Vector3(0, 0, pressDepth);
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey) && !isAnimating && PlayerIsLooking())
        {
            StartCoroutine(PressButton());
        }
    }

    IEnumerator PressButton()
    {
        isAnimating = true;

        // TOGGLE LOGIK
        IsOn = !IsOn;
        Debug.Log("Button state: " + (IsOn ? "ON" : "OFF"));

        // IN
        while (Vector3.Distance(transform.localPosition, pressedPos) > 0.001f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                pressedPos,
                pressSpeed * Time.deltaTime
            );
            yield return null;
        }

        yield return new WaitForSeconds(pressTime);

        // UT
        while (Vector3.Distance(transform.localPosition, startPos) > 0.001f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                startPos,
                pressSpeed * Time.deltaTime
            );
            yield return null;
        }

        isAnimating = false;
    }

    bool PlayerIsLooking()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        return Physics.Raycast(ray, out RaycastHit hit, interactDistance)
               && hit.transform == transform;
    }
}
    