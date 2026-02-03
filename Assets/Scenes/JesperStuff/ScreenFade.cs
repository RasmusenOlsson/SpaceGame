using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage;

    [Header("Fade durations (sekunder)")]
    public float fadeInDuration = 0.5f;   // Fade in när scenen startar
    public float fadeOutDuration = 2f;    // Fade ut när spelaren dör

    private void Awake()
    {
        // Se till att tiden är normal
        Time.timeScale = 1f;

        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.color = Color.black;
            StartCoroutine(FadeFromBlack());
        }
    }

    IEnumerator FadeFromBlack()
    {
        float t = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = new Color(0, 0, 0, 0);

        while (t < fadeInDuration)
        {
            t += Time.unscaledDeltaTime;
            fadeImage.color = Color.Lerp(startColor, targetColor, t / fadeInDuration);
            yield return null;
        }

        fadeImage.color = targetColor;
        fadeImage.gameObject.SetActive(false);
    }

    public void FadeOutAndRestart()
    {
        StartCoroutine(FadeOutAndReloadCoroutine());
    }

    IEnumerator FadeOutAndReloadCoroutine()
    {
        fadeImage.gameObject.SetActive(true);
        float t = 0f;
        Color startColor = fadeImage.color;
        Color targetColor = Color.black;

        while (t < fadeOutDuration)
        {
            t += Time.unscaledDeltaTime;
            fadeImage.color = Color.Lerp(startColor, targetColor, t / fadeOutDuration);
            yield return null;
        }

        fadeImage.color = targetColor;

        // Ladda om scenen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
