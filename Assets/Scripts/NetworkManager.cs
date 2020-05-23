using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NetworkManager : MonoBehaviour
{

    public static NetworkManager instance;
    public SocketIOComponent socket;
    public GameObject roomButton;
    public GameObject instantiatedPlayerName;
    public GameObject createRoomContainer;
    public GameObject waitingRoomContainer;
    private GameObject instantiatedRoom;
    private GameObject instantPlName;
    public GameObject waitingRoom;
    public GameObject gameController;
    
    [HideInInspector]
    public string newRoomName;
    [HideInInspector]
    public int newRoomMax;
    [HideInInspector]
    public PlayerJSON mainPlayer;

    public RoomsJSON currentGameRoom;
    
    public static int maxPlayers;
    public static string enteredRoomId;
    public static string turnPlayerId;

    [HideInInspector] public static string mainUserName = "";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)    
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(socket);
    }

        private void Start()
        {
            newRoomMax = 2;

            currentGameRoom = new RoomsJSON();
            
            socket.On("receive-rooms", OnReceiveRooms);
            socket.On("created-room", OnCreatedRoom);
            socket.On("room-response", OnRoomResponse);
            socket.On("update-room", OnRoomUpdate);
            socket.On("my-cards", OnMyCards);
            socket.On("move-order", OnMoveOrder);
            // socket.On("active-players", OnActivePlayers);
            
            
            
        }

        public void ReceiveRoomId2()
        {
            EnterRoom(enteredRoomId);
        }

        public void SetNewRoomName(string name)
        {
            newRoomName = name;
        }
        
        public void SetNewRoomMax(int max)
        {
            max += 2;
            newRoomMax = max;
        }
        
        #region Commands
    
        // Tels server to send back the rooms
        public void getRooms()
        {
            socket.Emit("get-rooms");
           
        }

        public void CreateRoom()
        {
            if (newRoomName != "")
            {
                Debug.Log(newRoomName + newRoomMax);
                string data = "{\"roomName\" : " +  "\"" + newRoomName + "\"" + ", \"maxPlayers\" : " + "\"" + newRoomMax + "\"" + "}";
                socket.Emit("new-room", new JSONObject(data));
            }
        }
        
        public void EnterRoom(string roomId)
        {
            string data = "{\"roomId\" : " +  "\"" + roomId + "\"" + ", \"nickName\" : " + "\"" + mainUserName + "\"" + "}";
            socket.Emit("enter-room", new JSONObject(data));
            currentGameRoom.id = roomId;
            currentGameRoom.maxPlayers = maxPlayers;
        }

        #endregion

        #region Listening
        
        // Receives rooms from server
        void OnReceiveRooms(SocketIOEvent socketIoEvent)
        {
            // Debug.Log("On Receive rooms called");
            foreach (Transform child in createRoomContainer.transform)
            {
                GameObject.Destroy(child.transform.gameObject);
            }
            
            string response = socketIoEvent.data.ToString();
            JSONResponse serverResponse = JSONResponse.CreateFromJSON(response);

            foreach (RoomsJSON room in serverResponse.data)
            {
                instantiatedRoom = Instantiate(roomButton, createRoomContainer.transform);
                instantiatedRoom.GetComponentInChildren<TextMeshProUGUI>().text = room.players.Count+ "/" + room.maxPlayers + ":    " + room.name;
                instantiatedRoom.GetComponentInChildren<Text>().text = room.id;
                instantiatedRoom.transform.Find("maxPlayer").gameObject.GetComponent<Text>().text =
                    room.maxPlayers.ToString();    
                if (room.id == currentGameRoom.id)
                {
                    currentGameRoom = room;
                }
            }
        }        
        
        // Receives new created room object
        void OnCreatedRoom(SocketIOEvent socketIoEvent)
        {
            // Debug.Log("On Created rooms called");
            string response = socketIoEvent.data.ToString();
            JSONResponse2 serverResponse = JSONResponse2.CreateFromJSON(response);
            // Debug.Log(serverResponse.data.ToString());
            RoomsJSON room = serverResponse.data;
            EnterRoom(room.id);
            enteredRoomId = room.id;
            currentGameRoom = room;
            waitingRoom.SetActive(true);
            instantPlName = Instantiate(instantiatedPlayerName, waitingRoomContainer.transform);
            instantPlName.GetComponent<TextMeshProUGUI>().text = mainUserName;

        }        
        
        // Receives new created player object
        void OnRoomResponse(SocketIOEvent socketIoEvent)
        {
            // Debug.Log("On Room response called");
            string response = socketIoEvent.data.ToString();
            JSONRoomResponse serverResponse = JSONRoomResponse.CreateFromJSON(response);
            mainPlayer = serverResponse.data;
            Debug.Log(mainPlayer.nickname);

        }

        void OnRoomUpdate(SocketIOEvent socketIoEvent)
        {
            Debug.Log("On Room UPDATE called");
            string response = socketIoEvent.data.ToString();
            JSONUpdateResponse serverResponse = JSONUpdateResponse.CreateFromJSON(response);
            
            foreach (Transform child in waitingRoomContainer.transform)
            { 
                GameObject.Destroy(child.transform.gameObject);
            }
            // Debug.Log("On Room UPDATE called2");
            currentGameRoom.players = new List<PlayerJSON>();
            // Debug.Log("On Room UPDATE called3");

            foreach (PlayerJSON player in serverResponse.data)
            {
                // Debug.Log("On Room UPDATE called5");

                instantPlName = Instantiate(instantiatedPlayerName, waitingRoomContainer.transform);
                instantPlName.GetComponent<TextMeshProUGUI>().text = player.nickname;
                currentGameRoom.players.Add(player);
                
                // Debug.Log("On Room UPDATE called6");

            }
            Debug.Log(currentGameRoom.players.Count + " " + currentGameRoom.maxPlayers);
            
            if (currentGameRoom.players.Count == currentGameRoom.maxPlayers)
            {
                Debug.Log("Game Sarted");
                LoadLevel(1);
            }

        }


        void OnMyCards(SocketIOEvent socketIoEvent)
        {
            Debug.Log("On My Cards called");
            string response = socketIoEvent.data.ToString();
            JSONMyCardsResponse serverResponse = JSONMyCardsResponse.CreateFromJSON(response);
            mainPlayer.cards = serverResponse.data;

        }        
        
        void OnMoveOrder(SocketIOEvent socketIoEvent)
        {
            Debug.Log("On Move Order called");
            string response = socketIoEvent.data.ToString();
            Debug.Log(response);
            JSONIdResponse serverResponse = JSONIdResponse.CreateFromJSON(response);
            Debug.Log(serverResponse.data);
            GameControl.turnPlayerId = serverResponse.data;
            gameController.GetComponent<GameControl>().TurnPermission(serverResponse.data);
            turnPlayerId = serverResponse.data;
        }

        // void OnActivePlayers(SocketIOEvent socketIoEvent)
        // {
        //     Debug.Log("On Active Players called");
        //     string response = socketIoEvent.data.ToString();
        //     JSONUpdateResponse serverResponse = JSONUpdateResponse.CreateFromJSON(response);
        //     gameController.GetComponent<GameControl>().EnemyMove(serverResponse.data);
        //
        // }
        
        
        

        #endregion

        #region JSONMessageClasses

        [Serializable]
        public class JSONResponse
        {
            public string status;
            public string message;
            public List<RoomsJSON> data;
            public static JSONResponse CreateFromJSON(string response)
            {
                return JsonUtility.FromJson<JSONResponse>(response);
            }
            
        }
        
        [Serializable]
        public class JSONResponse2
        {
            public string status;
            public string message;
            public RoomsJSON data;
            public static JSONResponse2 CreateFromJSON(string response)
            {
                return JsonUtility.FromJson<JSONResponse2>(response);
            }
            
        }
        
        [Serializable]
        public class JSONRoomResponse
        {
            public string status;
            public string message;
            public PlayerJSON data;
            
            public static JSONRoomResponse CreateFromJSON(string response)
            {
                return JsonUtility.FromJson<JSONRoomResponse>(response);
            }
            
        }        
        
        [Serializable]
        public class JSONUpdateResponse
        {
            public string status;
            public string message;
            public List<PlayerJSON> data;
            
            public static JSONUpdateResponse CreateFromJSON(string response)
            {
                return JsonUtility.FromJson<JSONUpdateResponse>(response);
            }
            
        }        
        
        [Serializable]
        public class JSONMyCardsResponse
        {
            public string status;
            public string message;
            public List<CardsJSON> data;
            
            public static JSONMyCardsResponse CreateFromJSON(string response)
            {
                return JsonUtility.FromJson<JSONMyCardsResponse>(response);
            }
            
        }        
        
        [Serializable]
        public class JSONIdResponse
        {
            public string status;
            public string message;
            public string data;
            
            public static JSONIdResponse CreateFromJSON(string response)
            {
                return JsonUtility.FromJson<JSONIdResponse>(response);
            }
            
        }
        
        

        [Serializable]
        public class RoomsJSON
        {
            public string id;
            public string name;
            public List<PlayerJSON> players;
            public string status;
            public int maxPlayers;

            public static RoomsJSON CreateFromJSON(string data)
            {
                return JsonUtility.FromJson<RoomsJSON>(data);
            }
        }
        
        [Serializable]
        public class PlayerJSON
        {
            public string id;
            public string nickname;
            public string points;
            public string socketId;
            public List<CardsJSON> cards;
            public List<CardsJSON> discardedCards;
            public string @protected;

            public static PlayerJSON CreateFromJSON(string data)
            {
                return JsonUtility.FromJson<PlayerJSON>(data);
            }
        }

        [Serializable]
        public class CardsJSON
        {
            public string id;
            public string name;
            public string power;
            public string playerId;
            
            public static CardsJSON CreateFromJSON(string data)
            {
                return JsonUtility.FromJson<CardsJSON>(data);
            }
        }


        #endregion
        
        
        
        // Loads game scene
        public void LoadLevel(int sceneIndex)
        {
            StartCoroutine(LoadAsynchron(sceneIndex));
        }
    
        IEnumerator LoadAsynchron(int sceneIndex)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!operation.isDone)
            {
                yield return null;
            }
            gameController = GameObject.Find("GameController");
        
        }
}

