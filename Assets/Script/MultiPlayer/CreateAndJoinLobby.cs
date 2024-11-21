using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public InputField CreateRoomInput;
    public InputField JoinRoomInput;


    public Text TextErrorRoom;
    public void CreateRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom(CreateRoomInput.text , new RoomOptions { MaxPlayers = 2}); 
        }
        else
        {

            Debug.LogError("Client non è connesso al Master Server.");
        }
    }

    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinRoom(JoinRoomInput.text);
        }
        else
        {
            Debug.LogError("Client non è connesso al Master Server.");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Unito alla stanza: " + PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene("Game");
    }
}
