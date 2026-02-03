using UnityEngine;

public class LaserKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();

            if (playerDeath != null)
            {
                playerDeath.Die();
            }
        }
    }
}
