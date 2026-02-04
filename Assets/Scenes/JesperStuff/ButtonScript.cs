using UnityEngine;
using System.Collections;

public class GeneralButton : MonoBehaviour
{
    [Header("Button settings")]
    public float buttonMoveDistance = 0.2f;
    public float buttonSpeed = 5f;

    [Header("Weight settings")]
    public float requiredMass = 5f; // Objektets minsta massa som kan trycka ner knappen

    public bool IsPressed { get; private set; }

    private Vector3 startPos;
    private Vector3 downPos;
    private Coroutine moveRoutine;

    void Start()
    {
        startPos = transform.localPosition;
        downPos = startPos - new Vector3(0, buttonMoveDistance, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        // Om det inte finns Rigidbody eller massan är för liten, ignorera
        if (rb == null || rb.mass < requiredMass) return;

        if (IsPressed) return;

        IsPressed = true;

        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveButton(downPos));
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        // Endast lyfta knappen om samma masskrav gäller
        if (rb == null || rb.mass < requiredMass) return;

        if (!IsPressed) return;

        IsPressed = false;

        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveButton(startPos));
    }

    IEnumerator MoveButton(Vector3 target)
    {
        while (Vector3.Distance(transform.localPosition, target) > 0.001f)
        {
            transform.localPosition = Vector3.MoveTowards(
                transform.localPosition,
                target,
                buttonSpeed * Time.deltaTime
            );
            yield return null;
        }

        moveRoutine = null;
    }
}
