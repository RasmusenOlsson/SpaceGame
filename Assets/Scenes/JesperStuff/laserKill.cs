using UnityEngine;

public class LaserKill : MonoBehaviour
{
    public float maxDistance = 50f;
    public LayerMask hitMask;

    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        FireLaser();
    }

    void FireLaser()
    {
        Vector3 startPos = transform.position;
        Vector3 dir = transform.forward;

        line.SetPosition(0, startPos);

        if (Physics.Raycast(startPos, dir, out RaycastHit hit, maxDistance, hitMask))
        {


            if (hit.collider.CompareTag("Player"))
            {
                PlayerDeath death = hit.collider.GetComponent<PlayerDeath>();
                if (death != null)
                {
                    death.Die();
                }
            }
            else
            {
                line.SetPosition(1, hit.point);
            }
            
        }
        else
        {
            line.SetPosition(1, startPos + dir * maxDistance);
        }
    }
}
