using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviourPunCallbacks
{
    public GameObject roomListText; // Prefab per i bottoni delle stanze
    public Transform ContainerText; // Contenitore per i bottoni

    public string gameSceneName = "Game"; // Nome della scena di gioco

    // Dizionario per memorizzare i bottoni delle stanze
    private Dictionary<string, GameObject> roomButtons = new Dictionary<string, GameObject>();

    private bool isJoining = false; // Flag per evitare tentativi multipli di join

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Connessione al server Photon
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(); // Entra nella lobby dopo la connessione
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            // Rimuove stanze non più disponibili
            if (room.RemovedFromList)
            {
                if (roomButtons.ContainsKey(room.Name))
                {
                    Destroy(roomButtons[room.Name]);
                    roomButtons.Remove(room.Name);
                }
                continue;
            }

            // Aggiunge nuove stanze
            if (!roomButtons.ContainsKey(room.Name))
            {
                GameObject NexButtonRoom = Instantiate(roomListText, ContainerText.transform.position, Quaternion.identity);
                NexButtonRoom.transform.SetParent(ContainerText.transform, false);

                // Aggiorna il testo del bottone
                Text textComponent = NexButtonRoom.GetComponent<Text>();
                textComponent.text = $"Stanza: {room.Name} - Giocatori: {room.PlayerCount}/2";

                // Configura il click del bottone
                Button buttonJoin = NexButtonRoom.GetComponentInChildren<Button>();
                string RoomName = room.Name; // Variabile locale per il nome della stanza
                buttonJoin.onClick.AddListener(() => JoinRoom(RoomName));

                // Disabilita il bottone se la stanza è piena
                buttonJoin.interactable = room.PlayerCount < 2;

                // Aggiunge il bottone al dizionario
                roomButtons.Add(room.Name, NexButtonRoom);
            }
        }
    }

    private void JoinRoom(string roomName)
    {
        if (!isJoining)
        {
            isJoining = true;
            Debug.Log($"Tentativo di unirsi alla stanza: {roomName}");
            PhotonNetwork.JoinRoom(roomName);

            // Disabilita i bottoni mentre si unisce
            foreach (GameObject buttonObj in roomButtons.Values)
            {
                buttonObj.GetComponent<Button>().interactable = false;
            }
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Unito alla stanza. Caricamento della scena: {gameSceneName}");
        PhotonNetwork.LoadLevel(gameSceneName); // Carica la scena di gioco
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Errore durante l'unione alla stanza: {message}");
        isJoining = false;
        RefreshLobby(); // Ricarica la lobby
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnesso: {cause}");
        ClearRoomList(); // Pulisce la lista delle stanze
        isJoining = false;
    }

    private void ClearRoomList()
    {
        foreach (GameObject buttonObj in roomButtons.Values)
        {
            Destroy(buttonObj);
        }
        roomButtons.Clear();
    }

    public void RefreshLobby()
    {
        PhotonNetwork.JoinLobby();
    }
}
