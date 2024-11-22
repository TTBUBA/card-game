using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviourPun 
{
    public GameObject PrefabsCard;
    public GameObject[] PointSpawnPlayer;
    public GameObject PointSpawnEnemy;
    public Camera MainCamera;
    public CardManager CardManager;
    public void Start()
    {
        Invoke("SpawnCards", 0.3f);
    }

    void SpawnCards()
    {
        SpawnCard();

        /*
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnCard_Player("owner");
        }
        else
        {
            SpawnCard_Enemy("enemy");
        }
        */
    }

    /*
    public void SpawnCard_Player(string owner)
    {
        for(int i = 0; i < 6 ; i++)
        {
            GameObject card = PhotonNetwork.Instantiate(PrefabsCard.name, PointSpawnPlayer[i].transform.position, Quaternion.identity);
            card.transform.SetParent(PointSpawnPlayer[i].transform, false);// Imposta il genitore  a PointSpawnPlayer
            card.GetComponent<Card_Display>().test = owner;
        }
    }
   

    public void SpawnCard_Enemy(string owner)
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject card = PhotonNetwork.Instantiate(PrefabsCard.name, PointSpawnEnemy.transform.position, Quaternion.identity);
            card.transform.SetParent(PointSpawnEnemy.transform, false);// Imposta il genitore  a PointSpawnPlayer
            card.GetComponent<Card_Display>().test = owner;
        }
    }
    */


    //TEST SINGLEPLAYER
    public void SpawnCard()
    {
        for (int i = 0; i < 6; i++)
        {
            
            GameObject card = Instantiate(PrefabsCard, PointSpawnPlayer[i].transform.position, Quaternion.identity);
            card.transform.SetParent(PointSpawnPlayer[i].transform, false);// Imposta il genitore  a PointSpawnPlayer

            // Ottieni lo script Movement_Card
            Movement_Card movementCard = card.GetComponent<Movement_Card>();
            CardManager cardmanager = card.GetComponent<CardManager>();

            if (movementCard != null)
            {
                movementCard.SetCamera(MainCamera);//imposta la camera alla singola carta
                movementCard.SetPositionCard();//Salva la posizione della carta alla sua creazione
                movementCard.SetObject(CardManager); 


            }
        }
    }

}
