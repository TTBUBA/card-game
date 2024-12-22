using NUnit.Framework.Internal;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviourPun 
{
    public GameObject PrefabsCard;
    public GameObject[] PointSpawnPlayer;
    public GameObject[] PointSpawnEnemy;
    public List<Card_Info> CardList;

    public RectTransform Poinspawnplayer;
    public RectTransform Pointspawnenemy;

    public Camera MainCamera;
    public CardManager CardManager;
    public void Start()
    {
        Invoke("SpawnCards", 0.3f);
    }

    void SpawnCards()
    {
       SpawnCard();
        //SpawnCardSinglePlayer();
    }

    //TEST MULTIPLAYER
    public void SpawnCard()
    {
        // Determina se il giocatore è il MasterClient (giocatore principale)
        bool isLocalPlayer = PhotonNetwork.IsMasterClient;

        if (isLocalPlayer)
        {
            //spawn carte player
            for (int i = 0; i < 6; i++)
            {
                // Crea la carta e la sincronizza con tutti i giocatori
                GameObject card = Instantiate(PrefabsCard, PointSpawnPlayer[i].transform.position, Quaternion.identity);
                card.transform.SetParent(PointSpawnPlayer[i].transform, false); // Assegna il punto di spawn

                // Configura la carta
                Movement_Card movementCard = card.GetComponent<Movement_Card>();
                Card_Display cardisplay = card.GetComponent<Card_Display>();
                int randomIndex = Random.Range(0, CardList.Count); // crea un valore random da 0 fino al valore che contiene la lista 
                if (movementCard != null)
                {
                    movementCard.SetCamera(MainCamera);   // Assegna la camera
                    movementCard.SetPositionCard();      // Salva la posizione iniziale
                    movementCard.SetObject(CardManager); // Assegna il CardManager
                    cardisplay.IsEnemy = false;
                    cardisplay.Card_Info = CardList[randomIndex]; // imposta una cardInfo in base al valore di randomIndex
                }
            }

            
        }
        else if(!isLocalPlayer)
        {
            //spawn carte nemiche
            for (int i = 0; i < 6; i++)
            {
                // Crea la carta e la sincronizza con tutti i giocatori
                GameObject card = Instantiate(PrefabsCard, PointSpawnEnemy[i].transform.position, Quaternion.identity);
                card.transform.SetParent(PointSpawnEnemy[i].transform, false); // Assegna il punto di spawn

                // Configura la carta
                Movement_Card movementCard = card.GetComponent<Movement_Card>();
                Card_Display cardisplay = card.GetComponent<Card_Display>();
                int randomIndex = Random.Range(0, CardList.Count);
                if (movementCard != null)
                {
                    movementCard.SetCamera(MainCamera);   // Assegna la camera
                    movementCard.SetPositionCard();      // Salva la posizione iniziale
                    movementCard.SetObject(CardManager); // Assegna il CardManager
                    cardisplay.IsEnemy = true;
                    cardisplay.Card_Info = CardList[randomIndex];
                }
            }
        }

    }

    //Test Singleplayer//
    /*
    public void SpawnCardSinglePlayer()
    {
            //spawn carte player
            for (int i = 0; i < 6; i++)
            {
                // Crea la carta e la sincronizza con tutti i giocatori
                GameObject card = Instantiate(PrefabsCard, PointSpawnPlayer[i].transform.position, Quaternion.identity);
                card.transform.SetParent(PointSpawnPlayer[i].transform, false); // Assegna il punto di spawn
                
                // Configura la carta
                Movement_Card movementCard = card.GetComponent<Movement_Card>();
                Card_Display cardisplay = card.GetComponent<Card_Display>();
                int randomIndex = Random.Range(0, CardList.Count); // crea un valore random da 0 fino al valore che contiene la lista 
                if (movementCard != null)
                {
                    movementCard.SetCamera(MainCamera);   // Assegna la camera
                    movementCard.SetPositionCard();      // Salva la posizione iniziale
                    movementCard.SetObject(CardManager); // Assegna il CardManager
                    cardisplay.IsEnemy = false;
                    cardisplay.Card_Info = CardList[randomIndex]; // imposta una cardInfo in base al valore di randomIndex
                }
            }

            //spawn carte nemiche
            for (int i = 0; i < 6; i++)
            {
                // Crea la carta e la sincronizza con tutti i giocatori
                GameObject card = Instantiate(PrefabsCard, PointSpawnEnemy[i].transform.position, Quaternion.identity);
                card.transform.SetParent(PointSpawnEnemy[i].transform, false); // Assegna il punto di spawn
                
                // Configura la carta
                Movement_Card movementCard = card.GetComponent<Movement_Card>();
                Card_Display cardisplay = card.GetComponent<Card_Display>();
                int randomIndex = Random.Range(0, CardList.Count);
                if (movementCard != null)
                {
                    movementCard.SetCamera(MainCamera);   // Assegna la camera
                    movementCard.SetPositionCard();      // Salva la posizione iniziale
                    movementCard.SetObject(CardManager); // Assegna il CardManager
                    cardisplay.IsEnemy = true;
                    cardisplay.Card_Info= CardList[randomIndex];
                }
            }
    }
    */
    //================//
}
