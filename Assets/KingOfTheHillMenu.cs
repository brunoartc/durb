using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfTheHillMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject findOpponentPanel;
    [SerializeField] private GameObject waitingStatusPanel;
    [SerializeField] private TMPro.TextMeshProUGUI waitingStatusText;

    private bool isConnecting = false;

    private const string GameVersion = "0.1";

    private const int MaxPlayersPerRoom = 2;

    private void Awake() => PhotonNetwork.AutomaticallySyncScene = true;

    public void FindOpponent()
    {
        isConnecting = true;
        waitingStatusText.text = "Searching";

        findOpponentPanel.SetActive(false);
        waitingStatusPanel.SetActive(true);

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");

        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        base.OnConnectedToMaster();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        waitingStatusPanel.SetActive(false);
        findOpponentPanel.SetActive(true);

        Debug.Log($"Disconnected {cause}");


    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client joined");
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount != MaxPlayersPerRoom)
        {
            waitingStatusText.text = "waiting opponent";
            Debug.Log("Client is waitin an opponent");


        } else
        {
            waitingStatusText.text = "Found opoonent";
            Debug.Log("Get Ready");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            waitingStatusText.text = "oponent fount";

            Debug.Log("Match going on");

            PhotonNetwork.LoadLevel("KingOfTheHill");
        }
    }

}
