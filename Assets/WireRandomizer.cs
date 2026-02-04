using UnityEngine;
using System.Collections.Generic; // Behövs för Listor

public class WireRandomizer : MonoBehaviour
{
    [Header("Dra in alla slutpunkter (Högersidan) här")]
    public List<RectTransform> wireEnds; 

    void Start()
    {
        ShufflePositions();
    }

    void ShufflePositions()
    {
        // 1. Spara alla ursprungliga positioner i en lista
        List<Vector3> startPositions = new List<Vector3>();
        foreach (RectTransform t in wireEnds)
        {
            startPositions.Add(t.anchoredPosition3D);
        }

        // 2. Blanda listan med positioner (Fisher-Yates shuffle)
        for (int i = 0; i < startPositions.Count; i++)
        {
            Vector3 temp = startPositions[i];
            int randomIndex = Random.Range(i, startPositions.Count);
            startPositions[i] = startPositions[randomIndex];
            startPositions[randomIndex] = temp;
        }

        // 3. Tilldela de nya, blandade positionerna till objekten
        for (int i = 0; i < wireEnds.Count; i++)
        {
            wireEnds[i].anchoredPosition3D = startPositions[i];
        }
    }
}
