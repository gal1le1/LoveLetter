using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    private void Awake()
    {
        cardList.Add(new Card(0, "None", "None", Resources.Load<Sprite>("CardImage/Baron"), "#FFFFFF"));
        cardList.Add(new Card(1, "Guard", "Name a non-Guard card and choose another player. If that player has that card, he or she is out round", Resources.Load<Sprite>("CardImage/Guard"), "#70410C"));
        cardList.Add(new Card(2, "Priest", "Look at another player's hand", Resources.Load<Sprite>("CardImage/Priest"), "#695B35"));
        cardList.Add(new Card(3, "Baron", "You and another player secretly compare hands. THe player with lower value is out of the round", Resources.Load<Sprite>("CardImage/Baron"), "#0B1E16"));
        cardList.Add(new Card(4, "Handmaid", "Until your next turn, Ignore all effects from other players' cards", Resources.Load<Sprite>("CardImage/Handmaid"), "#211D1C"));
        cardList.Add(new Card(5, "Prince", "Choose any player including yourself to discard her or his hand and draw a new card", Resources.Load<Sprite>("CardImage/Prince"), "#443426"));
        cardList.Add(new Card(6, "King", "Trade hands with another player of your choice", Resources.Load<Sprite>("CardImage/King"), "#391712"));
        cardList.Add(new Card(7, "Countess", "If you have this card and the King or Prince is in your hand, you must discard this card",Resources.Load<Sprite>("CardImage/Countess"), "#405344"));
        cardList.Add(new Card(8, "Princess", "If you discard this card, you are out of the round",Resources.Load<Sprite>("CardImage/Princess"), "#896A3C"));
    }
}

