using UnityEngine;
using UnityEngine.EventSystems;

public class UIWireDebug : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Inställningar")]
    public string targetName = "RedEnd"; 
    public Canvas parentCanvas;          

    private LineRenderer line;
    private RectTransform canvasRect;
    private bool isConnected = false;
    private Vector3 startPos;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        // Om parentCanvas inte är vald i inspector, försök hitta den automatiskt
        if (parentCanvas == null) parentCanvas = GetComponentInParent<Canvas>();
        canvasRect = parentCanvas.GetComponent<RectTransform>();
    }

    void Start()
    {
        startPos = transform.position;
        line.positionCount = 2;
        line.useWorldSpace = true; // VIKTIGT: Linjen ritas i världskoordinater
        ResetLine();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isConnected) return;
        Debug.Log("Börjar dra sladden..."); // Ser du detta i konsolen?
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isConnected) return;

        Vector3 worldPoint;
        
        // Här omvandlar vi musens position till 3D-position på Canvasen
        bool hitCanvas = RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvasRect, 
            eventData.position, 
            eventData.pressEventCamera, // Viktigt för World Space canvas
            out worldPoint
        );

        if (hitCanvas)
        {
            // Sätt Z lite närmare kameran så linjen syns framför plattan
            // OBS: Om din canvas är vriden kan detta behöva justeras.
            // Enklast är att använda samma Z som canvasen men minus litegrann.
            Vector3 finalPos = worldPoint;
            finalPos.z = transform.position.z - 0.05f; 

            line.SetPosition(1, finalPos);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isConnected) return;
        Debug.Log("Släppte sladden.");

        GameObject hitObject = eventData.pointerEnter;

        if (hitObject != null)
        {
            Debug.Log("Träffade objekt: " + hitObject.name);
            
            // Vi kollar om namnet stämmer (eller om föräldern har namnet)
            if (hitObject.name == targetName)
            {
                Connect(hitObject.transform.position);
            }
            else
            {
                Debug.Log("Fel mål! Förväntade '" + targetName + "' men träffade '" + hitObject.name + "'");
                ResetLine();
            }
        }
        else
        {
            Debug.Log("Träffade ingenting (null).");
            ResetLine();
        }
    }

    void Connect(Vector3 targetPos)
    {
        isConnected = true;
        line.SetPosition(1, targetPos);
        Debug.Log("Koppling lyckades!");
    }

    void ResetLine()
    {
        line.SetPosition(0, startPos);
        line.SetPosition(1, startPos);
    }
}

