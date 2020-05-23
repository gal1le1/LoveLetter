using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThePlayer : MonoBehaviour
{
    public string playerId;
    public string nickName;
    public string points;
    public string socketId;
    public string @protected;
    public List<NetworkManager.CardsJSON> discardedCards;
    

    public TextMeshProUGUI pubName;
    
    private void Start()
    {
        pubName.text = nickName;
    }
}
