using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage;              // UI Image som täcker hela skärmen
    public float fadeOutDuration = 1f;
    public float fadeInDuration = 1f;
    public Transform cameraToRotate;
    public float rotationAmount = 45f;
    public bool rotateDuringFade = true;

    private Quaternion initialRotation;

    void Awake()
    {
        // Se till att scenen börjar svart
        if (fadeImage != null)
            fadeImage.color = new Color(0, 0, 0, 1);

        if (cameraToRotate != null)
            initialRotation = cameraToRotate.localRotation;
    }

    void Start()
    {
        // Fade in automatiskt varje gång scenen laddas
        StartCoroutine(FadeIn());
    }

    public void FadeOutAndRestart()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float t = 0f;
        Quaternion startRot = cameraToRotate != null ? cameraToRotate.localRotation : Quaternion.identity;
        Quaternion endRot = startRot * Quaternion.Euler(-rotationAmount, 0, 0);

        while (t < fadeOutDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = t / fadeOutDuration;
            if (fadeImage != null)
                fadeImage.color = new Color(0, 0, 0, alpha);

            if (rotateDuringFade && cameraToRotate != null)
                cameraToRotate.localRotation = Quaternion.Slerp(startRot, endRot, alpha);

            yield return null;
        }

        // Ladda om scenen
        Time.timeScale = 1f; // återställ tid innan reload
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator FadeIn()
    {
        float t = fadeInDuration;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime;
            float alpha = t / fadeInDuration;
            if (fadeImage != null)
                fadeImage.color = new Color(0, 0, 0, alpha);

            yield return null;
        }

        // Återställ kameran
        if (cameraToRotate != null)
            cameraToRotate.localRotation = initialRotation;

        // Säkerställ att tiden är normal
        Time.timeScale = 1f;
    }
}
