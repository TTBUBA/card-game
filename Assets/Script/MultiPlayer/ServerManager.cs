using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerManager : MonoBehaviourPunCallbacks
{
    public GameObject LightPlayer;
    public GameObject LightEnemy;
    public Text People_ConnectServer;

    public void Start()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.EnableLobbyStatistics = true;
        UpdatePlayerCount();

        if(PhotonNetwork.IsMasterClient)
        {
            LightPlayer.SetActive(true);
            LightEnemy.SetActive(false);
        }
        else
        {
            LightPlayer.SetActive(false);
            LightEnemy.SetActive(true);
        }
    }
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        UpdatePlayerCount();
    }

    public void UpdatePlayerCount()
    {
        string PeopleCount;
        PeopleCount = PhotonNetwork.CountOfPlayersInRooms.ToString() + "/2";
        People_ConnectServer.text = PeopleCount;
        Debug.Log(PeopleCount);
    }

}
