using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviourPun 
{
    public GameObject PrefabsCard;
    public GameObject PointSpawnPlayer;
    public GameObject PointSpawnEnemy;

    
    public void Start()
    {
        Invoke("SpawnCards", 0.3f);
    }

    void SpawnCards()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnCard("owner");
        }
        else
        {
            SpawnCard("enemy");
        }
    }
    public void SpawnCard(string owner)
    {
        for(int i = 0; i < 6 ; i++)
        {
            GameObject card = PhotonNetwork.Instantiate(PrefabsCard.name, PointSpawnPlayer.transform.position, Quaternion.identity);
            card.transform.SetParent(PointSpawnPlayer.transform, false);// Imposta il genitore  a PointSpawnPlayer
            card.GetComponent<Card_Display>().test = owner;
        }
    }

    public void Test()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject card = Instantiate(PrefabsCard, PointSpawnPlayer.transform.position , Quaternion.identity);
            card.transform.SetParent(PointSpawnPlayer.transform, false);// Imposta il genitore  a PointSpawnPlayer

        }
    }
}
