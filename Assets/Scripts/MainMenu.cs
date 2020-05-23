using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject networkManager;
     
    public GameObject warning;
    public TMP_InputField userName;
    public GameObject consoleUserName;
    public GameObject createRoom;
    public GameObject roomList;
    public GameObject waitRoom;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActiveWaitRoom(bool cond)
    {
        waitRoom.SetActive(cond);
    }    
    
    public void ActiveRoomList(bool cond)
    {
        roomList.SetActive(cond);
    }
    

    public void QuitGame()
    {
        Debug.Log("Quit Game Pressed");
        Application.Quit();
    }

    public void CreateRoom()
    {
        if (NetworkManager.mainUserName == "")
        {
            warning.SetActive(true);
            // Debug.Log("activated");
        }
        else
        {
            createRoom.SetActive(true);
        }
    }    
    
    public void JoinRoom()
    {
        if (NetworkManager.mainUserName == "")
        {
            warning.SetActive(true);
            // Debug.Log("activated");
        }
        else
        {
            networkManager.GetComponent<NetworkManager>().getRooms();
            roomList.SetActive(true);
        }
    }
    
    

    public void SaveUserName()
    {
        if (userName.text.Trim() != "")
        {
            NetworkManager.mainUserName = userName.text.Trim();
            consoleUserName.GetComponent<TextMeshProUGUI>().text = userName.text.Trim();
            Debug.Log("name is: " + NetworkManager.mainUserName);
        }
        
    }
    
}
    