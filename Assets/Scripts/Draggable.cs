using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo = null;

    public enum Type
    {
        ENEMY,
        ME,
        BOTH
    };
    
    public Type typeOfCard;
    private int type;

    private void Start()
    {
        type = this.GetComponent<TheCard>().thisId;
        
        if (type == 1 || type == 2 || type == 3 || type == 6)
        {
            typeOfCard = Type.ENEMY;
        }
        else if (type == 4 || type == 7 || type == 8)
        {
            typeOfCard = Type.ME;
        }
        else if(type == 5)
        {
            typeOfCard = Type.BOTH;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        GameEvent.cardDragActive = true;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameEvent.current.CardDragBegin();
        
    }


    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parentToReturnTo);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameEvent.cardDragActive = false;
        GameEvent.current.CardExit();

    }
}
