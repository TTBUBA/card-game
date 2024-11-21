using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerManager : MonoBehaviourPunCallbacks
{
    public Text People_ConnectServer;

    public void Start()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.EnableLobbyStatistics = true;
        UpdatePlayerCount();
    }
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        UpdatePlayerCount();
    }

    public void UpdatePlayerCount()
    {
        string PeopleCount;
        PeopleCount = PhotonNetwork.CountOfPlayers.ToString() + "/2";
        People_ConnectServer.text = PeopleCount;
        Debug.Log(PeopleCount);
    }

}
