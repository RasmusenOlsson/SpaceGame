using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [Header("Optional UI")]
    public GameObject settingsPanel;

    private void Start()
    {
        // Show cursor in menu when the scene starts
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Hide the settings panel at start
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    private void Update()
    {
        // Close settings panel with ESC
        if (settingsPanel != null && settingsPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            settingsPanel.SetActive(false);
        }
    }

    // Called when Play button is pressed
    public void PlayGame()
    {
        // Make sure cursor is visible in the game scene
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Load the actual game scene
        SceneManager.LoadScene("RasmusScene"); // Replace with your game's scene name
    }

    // Called when Exit button is pressed
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit"); // Only works in the editor
    }

    // Open settings panel
    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    // Close settings panel manually (optional)
    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
}
