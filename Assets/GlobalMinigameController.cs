using UnityEngine;

public class GlobalMinigameController : MonoBehaviour
{
    [Header("Inställningar")]
    public GameObject minigameUI; // Dra in din Canvas/Panel här
    public KeyCode openKey = KeyCode.E; // Knappen för att öppna

    void Start()
    {
        // Se till att spelet är stängt när vi startar
        if (minigameUI != null)
        {
            minigameUI.SetActive(false);
        }
    }

    void Update()
    {
        // Lyssnar efter knapptryck varje frame
        if (Input.GetKeyDown(openKey))
        {
            ToggleGame();
        }
    }

    void ToggleGame()
    {
        // Om ui är på blir det av, är det av blir det på
        bool isActive = !minigameUI.activeSelf;
        minigameUI.SetActive(isActive);

        // --- VIKTIGT FÖR MUSPEKAREN ---
        if (isActive)
        {
            // Visa muspekaren så du kan klicka i minigamet
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // Lås musen igen så du kan styra gubben (om det är FPS)
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
