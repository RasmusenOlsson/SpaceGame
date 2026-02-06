using UnityEngine;

public class PlayerBladder : MonoBehaviour
{
    public float maxBladder = 100f;
    public float currentBladder = 0f;

    public float peeRate = 20f;
    public float minToPee = 5f;
    public KeyCode peeKey = KeyCode.P;

    public ParticleSystem peeParticles;

    void Update()
    {
        HandlePee();
    }

    void HandlePee()
    {
        if (Input.GetKey(peeKey) && currentBladder >= minToPee)
        {
            currentBladder -= peeRate * Time.deltaTime;

            if (!peeParticles.isPlaying)
                peeParticles.Play();
        }
        else
        {
            if (peeParticles.isPlaying)
                peeParticles.Stop();
        }

        currentBladder = Mathf.Clamp(currentBladder, 0f, maxBladder);
    }

    public void Drink(float amount)
    {
        currentBladder += amount;
        currentBladder = Mathf.Clamp(currentBladder, 0f, maxBladder);
    }
}
