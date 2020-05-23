using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Draggable.Type typeOfZone; 
    
    public GameObject mainPlayer;
    private GameObject card;
    public GameObject cardToMove;
    private GameObject instantiatedCard;
    private int cardType;
    public int panelId;

    private void Start()
    {
        GameEvent.current.onCardDragBegin += OnCardDrag;
        GameEvent.current.onCardEnter += OnCardEnter;
        GameEvent.current.onCardExit += OnCardExit;
    }


    public void OnDrop(PointerEventData eventData)
    {
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        card = eventData.pointerDrag.gameObject;
        cardType = card.GetComponent<TheCard>().thisId;
        cardToMove.GetComponent<TheCard>().thisId = cardType;
        
        if (d != null)
        {
            if (typeOfZone == d.typeOfCard || d.typeOfCard == Draggable.Type.BOTH)
            {
                d.parentToReturnTo = this.transform;
                LeanTween.scale(card, new Vector3(0f, 0f, 0f), 0.7f).setDelay(1f).setOnComplete(DestroyGO);
            }
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameEvent.cardDragActive)
        {
            Draggable dr = eventData.pointerDrag.GetComponent<Draggable>();
            if (typeOfZone == dr.typeOfCard || dr.typeOfCard == Draggable.Type.BOTH)
            {
                GameEvent.current.CardEnter(panelId);
            }
            // Debug.Log("I am here 1");

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameEvent.cardDragActive)
        {
            // Draggable dr = eventData.pointerDrag.GetComponent<Draggable>();
            // if (typeOfZone == dr.typeOfCard || dr.typeOfCard == Draggable.Type.BOTH)
            // {
                GameEvent.current.CardDragBegin();
            // }
            // Debug.Log("I am here 2");
        }
    }

    void DestroyGO()
    {
        Destroy(card);
        instantiatedCard = Instantiate(cardToMove, mainPlayer.transform);
        instantiatedCard.SetActive(false);
        instantiatedCard.transform.localScale = new Vector3(0,0,0);
        LeanTween.scale(instantiatedCard, new Vector3(0.4f, 0.4f, 0.4f), 0.7f).setDelay(0.5f).setOnStart(Instant);

    }

    void Instant()
    {
        Debug.Log("instant");
        instantiatedCard.SetActive(true);        
    }

    private void OnCardDrag()
    {
        this.GetComponent<Image>().color = new Color32(255,255,225,70);
    }

    private void OnCardEnter(int id)
    {
        if (id == panelId)
        {
            this.GetComponent<Image>().color = new Color32(255,255,225,130);
        }
    }

    private void OnCardExit()
    {
        this.GetComponent<Image>().color = new Color32(255,255,225,0);
    }
    
}
