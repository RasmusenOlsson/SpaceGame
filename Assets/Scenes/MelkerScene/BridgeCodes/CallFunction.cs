using UnityEngine;

public class BridgeInputController : MonoBehaviour
{
    public BridgeController bridgeController;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (bridgeController != null)
            {
                bridgeController.ExtendBridge();
            }
        }
    }
}
