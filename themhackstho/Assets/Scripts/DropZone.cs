using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public bool placed;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " Placed in the " + gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if(d != null && !placed)
        {
            d.parentToSnapTo = this.transform;
        }

        if (gameObject.name == "Battle Zone" && !placed)
        {
            placed = true;
            eventData.pointerDrag.GetComponent<card>().chosenCard();
        }
    }
}
