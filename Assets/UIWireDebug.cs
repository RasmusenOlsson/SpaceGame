using UnityEngine;
using UnityEngine.EventSystems;

public class UIWireDebug : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Inställningar")]
    public string targetName = "RedEnd";
    public Canvas parentCanvas;
    public WireTask wireTask;

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
        line.useWorldSpace = true;
        ResetLine();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isConnected) return;
        Debug.Log("Börjar dra sladden...");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isConnected) return;

        Vector3 worldPoint;

        bool hitCanvas = RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvasRect,
            eventData.position,
            eventData.pressEventCamera,
            out worldPoint
        );

        if (hitCanvas)
        {
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

            if (hitObject.name == targetName)
            {
                Connect(hitObject.transform.position);
                wireTask.RegisterSuccess();
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

