using System.Collections;
using UnityEngine;

public class EndingButtonScript : MonoBehaviour
{
    [Header("Ending Button settings")]
    public float endingButtonMoveDistance = 0.2f;
    public float endingButtonSpeed = 5f;

    [Header("Weight settings")]
    public float requiredMass = 5f; // Objektets minsta massa som kan trycka ner knappen

    [Header("Ending Settings")]
    public EndingScript endingScript;
    public bool triggerEndingA;

    [Header("Sounds")]
    [SerializeField] private AudioClip kaboooommmmmmm;

    public bool IsPressed { get; private set; }

    private Vector3 startPos;
    private Vector3 downPos;
    private Coroutine moveRoutine;

    void Start()
    {
        startPos = transform.localPosition;
        downPos = startPos - new Vector3(0, endingButtonMoveDistance, 0);
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

        if (endingScript != null)
        {
            if (triggerEndingA)
            {
                SoundManager.instance.PlaySoundFXclip(kaboooommmmmmm, transform, 1f);
                endingScript.ShowEndingA();
            }
            else
            {
                endingScript.ShowEndingB();
            }
        }
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
                endingButtonSpeed * Time.deltaTime
            );
            yield return null;
        }

        moveRoutine = null;
    }
}
