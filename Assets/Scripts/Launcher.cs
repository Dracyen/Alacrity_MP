using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace Photon.Pun.Demo.PunBasics
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        private byte maxPlayersPerRoom = 2;

        string gameVersion = "1";

        public InputField playerNameField;
        public InputField roomNameField;

        public Text playerStatus;
        public Text connectionStatus;

        public GameObject joinUI;
        public GameObject loadArena;
        public GameObject joinRoom;

        string playerName = "";
        string roomName = "";

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start()
        {
            PlayerPrefs.DeleteAll();

            Debug.Log("Connecting to Photon");

            joinUI.SetActive(false);
            loadArena.SetActive(false);

            ConnectToPhoton();
        }

        public void SetPlayerName(string name)
        {
            playerName = name;
        }

        public void SetRoomName(string name)
        {
            roomName = name;
        }
        
        void ConnectToPhoton()
        {
            connectionStatus.text = "Connecting...";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        public void JoinRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                Debug.Log("Creating or Joining a Room " + roomNameField.text);
                RoomOptions roomOptions = new RoomOptions();
                TypedLobby typedLobby = new TypedLobby(roomName, LobbyType.Default);
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby);
            }
        }

        public void LoadArena()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount >= maxPlayersPerRoom)
            {
                PhotonNetwork.LoadLevel("Main");
            }
            else
            {
                playerStatus.text = "Minimum 2 Players required to Load Arena!";
            }
        }


        public override void OnConnected()
        {
            base.OnConnected();
            connectionStatus.text = "Connected to Photon!";
            connectionStatus.color = Color.green;
            joinUI.SetActive(true);
            loadArena.SetActive(false);
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogError("Disconnected. Please check your Internet connection.");
        }

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                loadArena.SetActive(true);
                joinRoom.SetActive(false);
                playerStatus.text = "Your are Lobby Leader";
            }
            else
            {
                playerStatus.text = "Connected to Lobby";
            }
        }
    }
}
