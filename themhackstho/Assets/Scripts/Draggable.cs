using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    audioMan AM;
    public Transform parentToSnapTo = null;

    public void Start()
    {
        AM = audioMan.instance;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        AM.hold();

        parentToSnapTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");

        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        AM.hold();

        this.transform.SetParent(parentToSnapTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
