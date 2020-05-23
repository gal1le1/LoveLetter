using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    private int cardInDeck;
    private int oldCardInDeck;
    string childObjectName;
    private int currentCard, difference;

    // public int test, cardSize;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cardInDeck = PlayerDeck.staticDeckSize;
        currentCard = cardInDeck;
        // currentCard = 1;
        // childObjectName = $"Card ({currentCard})";
        // this.gameObject.transform.Find(childObjectName).gameObject.SetActive(false);
        // oldCardInDeck = cardSize;
    }

    // Update is called once per frame
    void Update()
    {
        cardInDeck = PlayerDeck.staticDeckSize;
        if (cardInDeck != oldCardInDeck)
        {
            difference = oldCardInDeck - cardInDeck;
            for (int i = 0; i < difference; i++)
            {
                childObjectName = $"Card ({currentCard})";
                this.gameObject.transform.Find(childObjectName).gameObject.SetActive(false);
                currentCard--;
            }
        }
        oldCardInDeck = cardInDeck;
    }
}
