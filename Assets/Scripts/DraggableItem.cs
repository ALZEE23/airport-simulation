using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public bool isDraggable = false;
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    private GraphicRaycaster graphicRaycaster;
    private EventSystem eventSystem;
    void Start()
    {
        graphicRaycaster = GetComponentInParent<Canvas>().GetComponent<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
        if (graphicRaycaster == null)
        {
            Debug.LogError("GraphicRaycaster not found on parent Canvas.");
        }

        if (eventSystem == null)
        {
            Debug.LogError("EventSystem not found in the scene.");
        }

        if (image == null)
        {
            Debug.LogError("Image component not found on this GameObject.");
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsFirstSibling();
        image.raycastTarget = false;
        isDraggable = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("OnDrag");
        transform.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);
        foreach (RaycastResult result in results)
        {
            Debug.Log("Raycast hit: " + result.gameObject.name);
            if (result.gameObject == gameObject)
            {
                Debug.Log("UI element is being raycast in world space");
            }
        }

        Vector2 rayPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(rayPosition, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log("Raycast hit 2D world object: " + hit.collider.gameObject.name);
        }
        else    
        {
            Debug.Log("No 2D world object hit by raycast.");
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        isDraggable = false;
    }


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
}

internal interface BeginDragHandler
{
}