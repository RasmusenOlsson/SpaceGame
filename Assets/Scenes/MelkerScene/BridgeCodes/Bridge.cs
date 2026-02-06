using UnityEngine;
using System.Collections;

public class BridgeController : MonoBehaviour
{
    public Transform bridge;
    public Vector3 extendedPosition;
    public float speed = 2f;

    public void ExtendBridge()
    {
        StartCoroutine(ExtendBridgeCoroutine());
    }

    private IEnumerator ExtendBridgeCoroutine()
    {
        while (Vector3.Distance(bridge.position, extendedPosition) > 0.01f)
        {
            bridge.position = Vector3.MoveTowards(bridge.position, extendedPosition, speed * Time.deltaTime);
            yield return null; 
        }

        bridge.position = extendedPosition; 
    }
}
