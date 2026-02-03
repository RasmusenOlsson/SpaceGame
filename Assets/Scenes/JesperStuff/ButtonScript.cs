using UnityEngine;
using System.Collections;

public class GeneralButton : MonoBehaviour
{
    [Header("Button settings")]
    public float buttonMoveDistance = 0.2f; // Hur långt knappen åker ner
    public float buttonSpeed = 5f;          // Hur snabbt knappen rör sig

    public bool IsPressed { get; private set; } // Universellt state

    private Vector3 startPos;
    private Vector3 downPos;

    private Coroutine moveRoutine;

    void Start()
    {
        startPos = transform.localPosition; // Lokala positionen
        downPos = startPos - new Vector3(0, buttonMoveDistance, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPressed) return;

        IsPressed = true;

        // Stoppa eventuell pågående rörelse innan ny startar
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveButton(downPos));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsPressed) return;

        IsPressed = false;

        // Stoppa eventuell pågående rörelse innan ny startar
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

        // När rörelsen är klar, rensa referensen
        moveRoutine = null;
    }
}
