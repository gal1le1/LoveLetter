using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject enemyPanel;
    public GameObject mainPlayer;
    public GameObject mainPlHand;
    public GameObject mainPlMove;
    public GameObject cardToHand;
    public GameObject cardToMove;
    public GameObject instantiatedCard;
    public GameObject instantiatedEnemyCard;
    public GameObject turnPermission;
    public GameObject cardHold;
    public GameObject mainPlHoldingCards;
    
    public List<GameObject> enemies;
    public List<GameObject> enemyPrefab;
    public int maxPlayersInGame;
    public GameObject networkMan;
    public NetworkManager.PlayerJSON mainJsonPlayer;

    public static string turnPlayerId;
    public static bool turnPlayerStart = false;
    
    private void Awake()
    {
        networkMan = GameObject.Find("NetworkManager");
        maxPlayersInGame = networkMan.GetComponent<NetworkManager>().currentGameRoom.maxPlayers;
        mainJsonPlayer = networkMan.GetComponent<NetworkManager>().mainPlayer;
        mainPlayer.GetComponent<ThePlayer>().nickName = mainJsonPlayer.nickname;

        mainPlHoldingCards = mainPlayer.transform.Find("HoldingCards").gameObject;
        
        for (int i = 0; i < maxPlayersInGame; i++)
        {
            if (mainJsonPlayer.id != networkMan.GetComponent<NetworkManager>().currentGameRoom.players[i].id)
            {
                enemyPrefab.Add(Instantiate(enemies[i], enemyPanel.transform));
                if (enemyPrefab.Count-1 < i)
                {
                    enemyPrefab[i - 1].GetComponentInChildren<DropZone>().mainPlayer = mainPlMove;
                    enemyPrefab[i - 1].GetComponent<ThePlayer>().nickName =
                        networkMan.GetComponent<NetworkManager>().currentGameRoom.players[i].nickname;
                    enemyPrefab[i - 1].GetComponent<ThePlayer>().playerId =
                        networkMan.GetComponent<NetworkManager>().currentGameRoom.players[i].id;
                    enemyPrefab[i - 1].GetComponent<ThePlayer>().socketId =
                        networkMan.GetComponent<NetworkManager>().currentGameRoom.players[i].socketId;
                }
                else
                {
                    
                    enemyPrefab[i].GetComponentInChildren<DropZone>().mainPlayer = mainPlMove;
                    enemyPrefab[i].GetComponent<ThePlayer>().nickName =
                    networkMan.GetComponent<NetworkManager>().currentGameRoom.players[i].nickname;
                    enemyPrefab[i].GetComponent<ThePlayer>().playerId =
                    networkMan.GetComponent<NetworkManager>().currentGameRoom.players[i].id;
                    enemyPrefab[i].GetComponent<ThePlayer>().socketId =
                    networkMan.GetComponent<NetworkManager>().currentGameRoom.players[i].socketId;
                }

                Instantiate(cardHold, enemyPrefab[i].transform.Find("HoldingCards"));
            }
            else
            {
            }
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mainJsonPlayer.id == turnPlayerId)
        {
            turnPermission.SetActive(false);
        }
        else
        {
            turnPermission.SetActive(true);
        }
        
        foreach (NetworkManager.CardsJSON card in mainJsonPlayer.cards)
        {
            Instantiate(cardHold, mainPlHoldingCards.transform);
            instantiatedCard = Instantiate(cardToHand, mainPlHand.transform);
            instantiatedCard.GetComponent<TheCard>().thisId = Convert.ToInt32(card.power);
            
        }
        
        // Debug.Log(NetworkManager.turnPlayerId + " ||");
        Debug.Log(turnPlayerId + " ||");

        for (int i = 0; i < enemyPrefab.Count; i++)
        {
            if (turnPlayerId == enemyPrefab[i].GetComponent<ThePlayer>().playerId)
            {
                Instantiate(cardHold, enemyPrefab[i].transform.Find("HoldingCards"));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnPermission(string playerId)
    {
        Debug.Log(playerId + " || " + mainJsonPlayer.id);
        if (playerId == mainJsonPlayer.id)
        {
            turnPermission.SetActive(false);
        }
        else
        {
            turnPermission.SetActive(true);
        }
    }

    // public void EnemyMove(List<NetworkManager.PlayerJSON> players)
    // {
    //     for (int i = 0; i < maxPlayersInGame; i++)
    //     {
    //         if (players[i].id != mainJsonPlayer.id)
    //         {
    //             Debug.Log(players[i].nickname);
    //             for (int j = 0; j < players[i].discardedCards.Count; j++)
    //             {
    //                 Debug.Log(players[i].discardedCards[j].name + " $");
    //                 Debug.Log(enemyPrefab[i].GetComponent<ThePlayer>().discardedCards[j] + " &");
    //                 if (!enemyPrefab[i].GetComponent<ThePlayer>().discardedCards.Contains(players[i].discardedCards[j]))
    //                 {
    //                     instantiatedEnemyCard = Instantiate(cardToMove, enemyPrefab[i].transform.Find("Move"));
    //                     Destroy(enemyPrefab[i].transform.Find("HoldingCards").transform.GetChild(0));
    //                 }
    //                 
    //             }
    //             
    //         }
    //     
    //     }
    //     
    // }
    
}
