using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviourPun 
{
    public GameObject PrefabsCard;
    public GameObject[] PointSpawnPlayer;
    public GameObject[] PointSpawnEnemy;

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
       //SpawnCard();
        SpawnCardtest();
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


    //TEST MULTIPLAYER
    public void SpawnCard()
    {
        // Determina se il giocatore è il MasterClient (giocatore principale)
        bool isLocalPlayer = PhotonNetwork.IsMasterClient;

        if (isLocalPlayer)
        {
           
            for (int i = 0; i < 6; i++)
            {
                // Crea la carta e la sincronizza con tutti i giocatori
                GameObject card = PhotonNetwork.Instantiate(PrefabsCard.name, PointSpawnPlayer[i].transform.position, Quaternion.identity);
                card.transform.SetParent(PointSpawnPlayer[i].transform, false); // Assegna il punto di spawn

                // Configura la carta
                Movement_Card movementCard = card.GetComponent<Movement_Card>();
                if (movementCard != null)
                {
                    movementCard.SetCamera(MainCamera);   // Assegna la camera
                    movementCard.SetPositionCard();      // Salva la posizione iniziale
                    movementCard.SetObject(CardManager); // Assegna il CardManager
                }
            }

            
        }
        else if(!isLocalPlayer)
        {
            for (int i = 0; i < 6; i++)
            {
                // Crea la carta e la sincronizza con tutti i giocatori
                GameObject card = PhotonNetwork.Instantiate(PrefabsCard.name, PointSpawnEnemy[i].transform.position, Quaternion.identity);
                card.transform.SetParent(PointSpawnEnemy[i].transform, false); // Assegna il punto di spawn

                // Configura la carta
                Movement_Card movementCard = card.GetComponent<Movement_Card>();
                if (movementCard != null)
                {
                    movementCard.SetCamera(MainCamera);   // Assegna la camera
                    movementCard.SetPositionCard();      // Salva la posizione iniziale
                    movementCard.SetObject(CardManager); // Assegna il CardManager
                }
            }
        }

    }

    //TEST Singleplayer
    public void SpawnCardtest()
    {

            for (int i = 0; i < 6; i++)
            {
                // Crea la carta e la sincronizza con tutti i giocatori
                GameObject card = Instantiate(PrefabsCard, PointSpawnPlayer[i].transform.position, Quaternion.identity);
                card.transform.SetParent(PointSpawnPlayer[i].transform, false); // Assegna il punto di spawn

                // Configura la carta
                Movement_Card movementCard = card.GetComponent<Movement_Card>();
                if (movementCard != null)
                {
                    movementCard.SetCamera(MainCamera);   // Assegna la camera
                    movementCard.SetPositionCard();      // Salva la posizione iniziale
                    movementCard.SetObject(CardManager); // Assegna il CardManager
                }
            }

            for (int i = 0; i < 6; i++)
            {
                // Crea la carta e la sincronizza con tutti i giocatori
                GameObject card = Instantiate(PrefabsCard, PointSpawnEnemy[i].transform.position, Quaternion.identity);
                card.transform.SetParent(PointSpawnEnemy[i].transform, false); // Assegna il punto di spawn
                
                // Configura la carta
                Movement_Card movementCard = card.GetComponent<Movement_Card>();
                Card_Display cardisplay = card.GetComponent<Card_Display>();
                if (movementCard != null)
                {
                    movementCard.SetCamera(MainCamera);   // Assegna la camera
                    movementCard.SetPositionCard();      // Salva la posizione iniziale
                    movementCard.SetObject(CardManager); // Assegna il CardManager
                    cardisplay.IsEnemy = true;
                }
            }
    }
}
