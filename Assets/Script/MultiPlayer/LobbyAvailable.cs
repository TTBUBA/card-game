using Photon.Pun;        
using Photon.Realtime;   
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gestisce la lobby multiplayer usando Photon Network.
/// Permette di visualizzare, aggiornare e unirsi alle stanze disponibili.
/// </summary>
public class Lobby : MonoBehaviourPunCallbacks
{
    public GameObject roomListText;

    public Transform ContainerText;

    public string gameSceneName = "Game";

    // Dictionary per tenere traccia dei bottoni creati per ogni stanza
    // Key: Nome della stanza, Value: GameObject del bottone
    private Dictionary<string, GameObject> roomButtons = new Dictionary<string, GameObject>();

    private bool isJoining = false;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// Callback chiamato quando la connessione al server master è stabilita.
    /// Entra automaticamente nella lobby.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    /// <summary>
    /// Callback chiamato quando la lista delle stanze viene aggiornata.
    /// Gestisce la creazione e l'aggiornamento dei bottoni per ogni stanza.
    /// </summary>
    /// <param name="roomList">Lista delle stanze disponibili</param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo room in roomList)
        {
            // Gestione rimozione stanze non più disponibili
            if (room.RemovedFromList)
            {
                if (roomButtons.ContainsKey(room.Name))
                {
                    Destroy(roomButtons[room.Name]);
                    roomButtons.Remove(room.Name);
                }
                continue;
            }

            // Creazione nuovo bottone per stanze non ancora in lista
            if (!roomButtons.ContainsKey(room.Name))
            {
                // Istanzia e posiziona il nuovo bottone nel container
                GameObject NexButtonRoom = Instantiate(roomListText, ContainerText.transform.position, Quaternion.identity);
                NexButtonRoom.transform.SetParent(ContainerText.transform, false);

                // Configura il testo del bottone
                Text textComponent = NexButtonRoom.GetComponent<Text>();
                textComponent.text = $"Stanza: {room.Name} - Giocatori: {room.PlayerCount}/2";

                // Configura il listener per il click
                Button buttonJoin = NexButtonRoom.GetComponent<Button>();
                string RoomName = room.Name;  // Variabile locale per la closure
                buttonJoin.onClick.AddListener(() => JoinRoom(RoomName));

                // Disabilita il bottone se la stanza è piena
                buttonJoin.interactable = room.PlayerCount < 2;

                // Aggiunge il bottone al dictionary
                roomButtons.Add(room.Name, NexButtonRoom);
            }
        }
    }

    /// <summary>
    /// Gestisce il tentativo di unirsi a una stanza.
    /// Previene tentativi multipli di join e disabilita i bottoni durante il processo.
    /// </summary>
    /// <param name="roomName">Nome della stanza a cui unirsi</param>
    private void JoinRoom(string roomName)
    {
        if (!isJoining)
        {
            isJoining = true;
            Debug.Log($"Tentativo di unirsi alla stanza: {roomName}");
            PhotonNetwork.JoinRoom(roomName);

            // Disabilita tutti i bottoni durante il processo di join
            foreach (GameObject buttonObj in roomButtons.Values)
            {
                buttonObj.GetComponent<Button>().interactable = false;
            }
        }
    }

    /// <summary>
    /// Callback chiamato quando l'ingresso in una stanza è avvenuto con successo.
    /// Carica la scena di gioco.
    /// </summary>
    public override void OnJoinedRoom()
    {
        Debug.Log($"Joined room successfully. Loading game scene: {gameSceneName}");
        PhotonNetwork.LoadLevel(gameSceneName);
    }

    /// <summary>
    /// Callback chiamato quando il tentativo di unirsi a una stanza fallisce.
    /// Riabilita i bottoni e aggiorna la lobby.
    /// </summary>
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Failed to join room: {message}");
        isJoining = false;
        RefreshLobby();
    }

    /// <summary>
    /// Callback chiamato in caso di disconnessione dal server.
    /// Pulisce la lista delle stanze e resetta lo stato.
    /// </summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnesso dal server: {cause}");
        ClearRoomList();
        isJoining = false;
    }

    /// <summary>
    /// Pulisce la lista delle stanze distruggendo tutti i bottoni.
    /// </summary>
    private void ClearRoomList()
    {
        foreach (GameObject buttonObj in roomButtons.Values)
        {
            Destroy(buttonObj);
        }
        roomButtons.Clear();
    }

    /// <summary>
    /// Metodo pubblico per aggiornare manualmente la lobby.
    /// Può essere chiamato da un bottone nell'UI.
    /// </summary>
    public void RefreshLobby()
    {
        PhotonNetwork.JoinLobby();
    }
}