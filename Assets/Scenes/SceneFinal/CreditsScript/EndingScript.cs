using UnityEngine;

public class EndingScript : MonoBehaviour
{ 
    public GameObject endingACanvas;
    public GameObject endingBCanvas;

    public bool endingChosen = false;

    public void ShowEndingA()
    {
        if (endingChosen) return;
        endingChosen = true;
        endingACanvas.SetActive(true);
    }

    public void ShowEndingB()
    {
        if (endingChosen) return;
        endingChosen = true;
        endingBCanvas.SetActive(true);
    }
}
