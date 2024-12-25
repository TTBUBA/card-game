using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviourPunCallbacks
{
    public GameObject PrefabsCard;
    public GameObject[] PointSpawnPlayer;
    public GameObject[] PointSpawnEnemy;
    public List<Card_Info> CardList;

    public Camera MainCamera;
    public CardManager CardManager;

    private List<GameObject> spawnedCards = new List<GameObject>();

    public void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnCards();
        }
    }

    private void SpawnCards()
    {
        // Spawn player cards
        for (int i = 0; i < 1; i++)
        {
            GameObject card = PhotonNetwork.Instantiate(PrefabsCard.name, PointSpawnPlayer[i].transform.position, Quaternion.identity);
            ConfigureCard(card, PointSpawnPlayer[i].transform, false);
        }

        // Spawn enemy cards
        for (int i = 0; i < 1; i++)
        {
            GameObject card = PhotonNetwork.Instantiate(PrefabsCard.name, PointSpawnEnemy[i].transform.position, Quaternion.identity);
            ConfigureCard(card, PointSpawnEnemy[i].transform, true);
        }
    }

    private void ConfigureCard(GameObject card, Transform parent, bool isEnemy)
    {
        card.transform.SetParent(parent, false);
        Movement_Card movementCard = card.GetComponent<Movement_Card>();
        Card_Display cardDisplay = card.GetComponent<Card_Display>();

        if (movementCard != null && cardDisplay != null)
        {
            int randomIndex = Random.Range(0, CardList.Count);
            movementCard.SetCamera(MainCamera);
            movementCard.SetPositionCard();
            movementCard.SetObject(CardManager);

            cardDisplay.IsEnemy = isEnemy;
            cardDisplay.Card_Info = CardList[randomIndex];
        }

        spawnedCards.Add(card);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SyncCardsWithNewPlayer(newPlayer);
        }
    }

    private void SyncCardsWithNewPlayer(Player newPlayer)
    {
        foreach (GameObject card in spawnedCards)
        {
            Card_Display cardDisplay = card.GetComponent<Card_Display>();
            Vector3 position = card.transform.position;
            int cardId = cardDisplay.Card_Info.cardId; 
            bool isEnemy = cardDisplay.IsEnemy;

            photonView.RPC("SyncCard", newPlayer, position, cardId, isEnemy);
        }
    }

    [PunRPC]
    public void SyncCard(Vector3 position, int cardId, bool isEnemy)
    {
        GameObject card = Instantiate(PrefabsCard, position, Quaternion.identity);
        Card_Display cardDisplay = card.GetComponent<Card_Display>();
        cardDisplay.IsEnemy = isEnemy;
        cardDisplay.Card_Info = CardList.Find(c => c.cardId == cardId); 

        // Usa i punti di spawn corretti per il player o nemico
        Transform parent = isEnemy ? PointSpawnEnemy[0].transform : PointSpawnPlayer[0].transform; // Usa il primo punto di spawn
        card.transform.SetParent(parent, false);

        spawnedCards.Add(card);
    }
}
