using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public ScreenFade screenFade;
    bool isDead = false;

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        // Pausa spelet under fade out
        Time.timeScale = 0f;

        // Fade out + reload
        screenFade.FadeOutAndRestart();
    }
}
