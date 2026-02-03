using UnityEngine;
using System.Collections;

public class GeneralButton : MonoBehaviour
{
    [Header("Button settings")]
    public float buttonMoveDistance = 0.2f; // Hur långt knappen åker ner
    public float buttonSpeed = 5f;          // Hur snabbt knappen rör sig

    private Vector3 buttonStartPos;
    private Vector3 buttonDownPos;
    private bool isPressed = false;

    // Event: Används för att trigga andra scripts
    public delegate void ButtonAction();
    public event ButtonAction OnButtonPressed;
    public event ButtonAction OnButtonReleased;

    void Start()
    {
        buttonStartPos = transform.position;
        buttonDownPos = buttonStartPos - new Vector3(0, buttonMoveDistance, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            isPressed = true;

            // Flytta knappen ner
            StartCoroutine(MoveButton(buttonDownPos));

            // Skriv ut i konsolen
            Debug.Log("Knapp tryckt!");

            // Trigga event
            if (OnButtonPressed != null)
                OnButtonPressed.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPressed)
        {
            isPressed = false;

            // Flytta knappen upp
            StartCoroutine(MoveButton(buttonStartPos));

            // Skriv ut i konsolen
            Debug.Log("Knapp släppt!");

            // Trigga event
            if (OnButtonReleased != null)
                OnButtonReleased.Invoke();
        }
    }

    IEnumerator MoveButton(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, buttonSpeed * Time.deltaTime);
            yield return null;
        }
    }
}