using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{

    public static GameEvent current;
    public static bool cardDragActive;

    private void Awake()
    {
        current = this;
        cardDragActive = false;
    }
    public event Action onCardDragBegin;
    public event Action<int> onCardEnter;
    public event Action onCardExit;
    

    public void CardDragBegin()
    {
        if (onCardDragBegin != null)
        {
            onCardDragBegin();
        }
    }
    
    public void CardEnter(int id)
    {
        if (onCardEnter != null)
        {
            onCardEnter(id);
        }
    }
    
    public void CardExit()
    {
        if (onCardExit != null)
        {
            onCardExit();
        }
    }
    

}
