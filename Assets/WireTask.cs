using UnityEngine;
using System.Collections;

public class WireTask : MonoBehaviour
{
    public int totalWires = 4;       // Hur många kablar som krävs
    private int connectedWires = 0;  // Räknare

    // Varje gång spelet öppnas (aktiveras) nollställer vi räknaren
    void OnEnable()
    {
        connectedWires = 0;
    }

    public void RegisterSuccess()
    {
        connectedWires++;
        Debug.Log("Kabel klar! Totalt: " + connectedWires);

        // Om alla 4 är klara, starta avslutnings-sekvensen
        if (connectedWires >= totalWires)
        {
            StartCoroutine(CloseTaskSequence());
        }
    }

    // HÄR SKER MAGIN SOM STÄNGER SPELET AUTOMATISKT
    IEnumerator CloseTaskSequence()
    {
        // 1. Vänta lite (t.ex. 0.5 eller 1 sekund) så man hinner se att man lyckades
        yield return new WaitForSeconds(0.7f);

        // 2. Stäng ner själva minigame-fönstret (detta objekt)
        gameObject.SetActive(false);

        // 3. Lås musen och göm den igen (så man kan styra gubben i 3D)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Minigame stängt automatiskt.");
    }
}
