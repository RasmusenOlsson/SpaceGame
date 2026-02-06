using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class DoorSceneTransition : MonoBehaviour
{
    [Header("Scene")]
    public string sceneToLoad;

    [Header("Fade")]
    public Image fadeImage;
    public float fadeSpeed = 2f;

    [Header("Player")]
    public string playerTag = "Player";

    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isTransitioning) return;

        if (other.CompareTag(playerTag))
        {
            StartCoroutine(FadeAndLoad());
        }
    }

    IEnumerator FadeAndLoad()
    {
        isTransitioning = true;

        Color c = fadeImage.color;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            c.a = t;
            fadeImage.color = c;
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
