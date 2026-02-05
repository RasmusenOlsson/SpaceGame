using UnityEngine;

public class WaterBottle : MonoBehaviour
{
    public float drinkAmount = 30f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerBladder bladder = other.GetComponent<PlayerBladder>();
        if (bladder != null)
        {
            bladder.Drink(drinkAmount);
            Destroy(gameObject);
        }
    }
}
