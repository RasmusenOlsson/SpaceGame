using UnityEngine;
using System.Collections;

public class DoubleDoor : MonoBehaviour
{
    [Header("References")]
    public Transform leftDoor;
    public Transform rightDoor;
    public GeneralButton button; // knappen som styr dörren

    [Header("Movement")]
    public float openDistance = 2f;
    public float openSpeed = 2f;

    public enum MoveAxis { X, Y, Z }

    [Header("Left Door")]
    public MoveAxis leftAxis = MoveAxis.X;
    public bool invertLeft;

    [Header("Right Door")]
    public MoveAxis rightAxis = MoveAxis.X;
    public bool invertRight;

    [Header("Sounds")]
    [SerializeField] private AudioClip doorOpening;
    [SerializeField] private AudioClip doorClosing;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private Vector3 leftOpenPos;
    private Vector3 rightOpenPos;

    private bool isOpen = false;

    void Start()
    {
        leftClosedPos = leftDoor.position;
        rightClosedPos = rightDoor.position;

        leftOpenPos = leftClosedPos + GetDirection(leftDoor, leftAxis, invertLeft) * openDistance;
        rightOpenPos = rightClosedPos + GetDirection(rightDoor, rightAxis, invertRight) * openDistance;
    }

    void Update()
    {
        if (button == null) return;

        if (button.IsPressed && !isOpen)
            OpenDoor();

        if (!button.IsPressed && isOpen)
            CloseDoor();
    }

    Vector3 GetDirection(Transform door, MoveAxis axis, bool invert)
    {
        Vector3 dir = Vector3.zero;

        switch (axis)
        {
            case MoveAxis.X: dir = door.right; break;
            case MoveAxis.Y: dir = door.up; break;
            case MoveAxis.Z: dir = door.forward; break;
        }

        // Hantera negativ scale
        Vector3 localScaleSign = new Vector3(
            Mathf.Sign(door.localScale.x),
            Mathf.Sign(door.localScale.y),
            Mathf.Sign(door.localScale.z)
        );

        if (axis == MoveAxis.X) dir *= localScaleSign.x;
        if (axis == MoveAxis.Y) dir *= localScaleSign.y;
        if (axis == MoveAxis.Z) dir *= localScaleSign.z;

        return invert ? -dir : dir;
    }

    void OpenDoor()
    {
        isOpen = true;
        SoundManager.instance.PlaySoundFXclip(doorOpening, transform, 1f);
        StopAllCoroutines();
        StartCoroutine(MoveDoor(leftDoor, leftOpenPos));
        StartCoroutine(MoveDoor(rightDoor, rightOpenPos));
    }

    void CloseDoor()
    {
        isOpen = false;
        SoundManager.instance.PlaySoundFXclip(doorClosing, transform, 1f);
        StopAllCoroutines();
        StartCoroutine(MoveDoor(leftDoor, leftClosedPos));
        StartCoroutine(MoveDoor(rightDoor, rightClosedPos));
    }

    IEnumerator MoveDoor(Transform door, Vector3 target)
    {
        while (Vector3.Distance(door.position, target) > 0.01f)
        {
            door.position = Vector3.MoveTowards(door.position, target, openSpeed * Time.deltaTime);
            yield return null;
        }
    }
}

