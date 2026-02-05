using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainButtons;   // panel that holds all buttons
    public GameObject settingsPanel;

    private void Start()
    {
        settingsPanel.SetActive(false);
        mainButtons.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("RasmusScene");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainButtons.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainButtons.SetActive(true);
    }

    private void Update()
    {
        if (settingsPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSettings();
        }
    }
}
