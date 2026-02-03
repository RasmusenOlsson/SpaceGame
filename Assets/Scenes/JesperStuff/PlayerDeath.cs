using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    public ScreenFade screenFade;
    public Transform cameraTransform;
    public float cameraTiltAngle = 45f; // hur mycket kameran ska luta uppåt
    public float tiltDuration = 1f;

    private bool isDead = false;

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // Freeze spelarens position
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        // Disable alla script på kameran som kan styra rotation
        if (cameraTransform != null)
        {
            MonoBehaviour[] lookScripts = cameraTransform.GetComponents<MonoBehaviour>();
            foreach (var script in lookScripts)
                script.enabled = false;
        }

        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        // Start fade direkt
        if (screenFade != null)
            screenFade.FadeOutAndRestart();

        if (cameraTransform != null)
        {
            // Ta startrotationen som utgångspunkt
            Quaternion startRot = cameraTransform.localRotation;

            // Räkna ut targetrotation: lyft X med cameraTiltAngle uppåt
            Quaternion targetRot = startRot * Quaternion.Euler(-cameraTiltAngle, 0f, 0f);

            float t = 0f;
            while (t < tiltDuration)
            {
                t += Time.unscaledDeltaTime;
                cameraTransform.localRotation = Quaternion.Slerp(startRot, targetRot, t / tiltDuration);
                yield return null;
            }

            cameraTransform.localRotation = targetRot; // lås slutrotationen
        }
    }
}
