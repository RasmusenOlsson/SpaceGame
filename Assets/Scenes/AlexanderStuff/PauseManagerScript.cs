using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject pausePanel;     // contains Resume/Settings/Quit
    public GameObject settingsPanel;

    private bool isPaused = false;

    void Start()
    {
        pauseCanvas.SetActive(false);
        settingsPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If settings open → close settings
            if (settingsPanel.activeSelf)
            {
                CloseSettings();
            }
            // If paused → resume
            else if (isPaused)
            {
                ResumeGame();
            }
            // If playing → pause
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        pausePanel.SetActive(true);     // show buttons

        isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseCanvas.SetActive(false);

        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    // ⭐ NEW BEHAVIOR
    public void OpenSettings()
    {
        pausePanel.SetActive(false);      // hide Resume/Settings/Quit
        settingsPanel.SetActive(true);    // show settings
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);       // show buttons again
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
