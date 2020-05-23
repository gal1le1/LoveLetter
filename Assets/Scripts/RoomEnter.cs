using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomEnter : MonoBehaviour
{
    public GameObject networkManager;
    public GameObject menuManager;
    private string roomId;
    private string maxPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        networkManager = GameObject.Find("NetworkManager");
        menuManager = GameObject.Find("MenuManager");
        roomId = gameObject.GetComponentInChildren<Text>().text;
        maxPlayer = gameObject.transform.Find("maxPlayer").gameObject.GetComponent<Text>().text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressRoomEnter()
    {
        menuManager.GetComponent<MainMenu>().ActiveRoomList(false);
        Debug.Log(roomId);
        NetworkManager.enteredRoomId = roomId;
        NetworkManager.maxPlayers = Convert.ToInt32(maxPlayer);
        networkManager.GetComponent<NetworkManager>().EnterRoom(roomId);
        menuManager.GetComponent<MainMenu>().ActiveWaitRoom(true);
        
    }
}
