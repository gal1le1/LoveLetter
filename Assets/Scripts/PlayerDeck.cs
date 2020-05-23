using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerDeck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();

    public int deckSize;
    public static int staticDeckSize;
    
    // Start is called before the first frame update
    private void Awake()
    {
        staticDeckSize = deckSize;
    }

    void Start()
    {
        for (int i = 0, j = 1; i < deckSize; i++)
        {
            deck[i] = CardDataBase.cardList[j];
            if (i == 4 || i == 6 || i == 8 || i == 10 || i == 12 || i == 13 || i == 14)
            {
                j++;
            }
        }
        
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        staticDeckSize = deckSize;
    }

    public void Shuffle()
    {
        for (int i = 0; i < deckSize; i++)
        {
            container[0] = deck[i];
            int randomIndex = (Random.Range(i, 256))%16;
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];
        }
    }
}
