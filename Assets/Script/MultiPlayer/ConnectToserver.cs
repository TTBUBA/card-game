using UnityEngine;
using Photon.Pun;

public class ConnectToserver : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "eu";
        PhotonNetwork.ConnectUsingSettings();

        //DEBUG
        Debug.Log("Regione: " + PhotonNetwork.CloudRegion);
        Debug.Log("Pin: " + PhotonNetwork.GetPing());
        Debug.Log("Connessione al server iniziata.");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connesso al Master Server.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connesso alla lobby.");
    }
}
