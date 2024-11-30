using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;


public class Movement_Card : MonoBehaviourPun , IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Camera Camera;
    public Vector3 LastPosition; // Ultima posizione valida della carta
    public RectTransform rectTransform;
    public bool CardRelase;


    [SerializeField] private Card_Display card_Display;
    [SerializeField] private CardManager cardManager;
    [SerializeField] private PhotonView photonview;
    public void Start()
    {
        photonview = GetComponent<PhotonView>();
    }
    public void SetObject(CardManager CardManager)
    {
        card_Display = GetComponent<Card_Display>();
        cardManager = CardManager;
    }

    // Metodo per impostare la posizione iniziale della carta in termini di coordinate globali
    public void SetPositionCard()
    {
        rectTransform = GetComponent<RectTransform>(); // Ottieni il RectTransform associato alla carta
        Vector3 worldPosition = rectTransform.TransformPoint(Vector3.zero); // Converte la posizione locale in posizione globale
        worldPosition.z = 0; 
        LastPosition = worldPosition; // Salva la posizione della carta
    }

    public void SetCamera(Camera camera)
    {
        Camera = camera;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        cardManager.cardSelect = card_Display.Card_Info;
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*MULTIPLAYER
        if (photonview.IsMine)
        {
            Vector3 PosMouse = Camera.ScreenToWorldPoint(Input.mousePosition); // Ottiene la posizione del mouse
            this.transform.position = PosMouse; // Sposta la carta alla posizione del mouse
            PosMouse.z = 0;

            string NameCard = card_Display.Card_Info.name;

            cardManager.cardSelect = card_Display.Card_Info;    
        }
        */

        //SINGLEPLAYER
        Vector3 PosMouse = Camera.ScreenToWorldPoint(Input.mousePosition); // Ottiene la posizione del mouse
        this.transform.position = PosMouse; // Sposta la carta alla posizione del mouse
        PosMouse.z = 0;

        string NameCard = card_Display.Card_Info.name;

        cardManager.cardSelect = card_Display.Card_Info;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        /*MULTIPLAYER
        if(photonview.IsMine)
        {
            if(cardManager.allLightsOff == false)
            {
                Vector2 PosMouse = Camera.ScreenToWorldPoint(Input.mousePosition); // Ottiene la posizione del mouse 
                Collider2D hitcollider = Physics2D.OverlapPoint(PosMouse); // Controlla se c'è un oggetto sotto il mouse 

                if (hitcollider != null && hitcollider.CompareTag("BoxPlaceCard"))
                {
                    this.transform.position = hitcollider.transform.position; // Allinea la carta alla posizione del collider 
                    CardRelase = true;
                    cardManager.DescreseLight();
                    cardManager.DecreseLife();
                }
                else
                {
                    this.transform.position = LastPosition;
                    //card_Display = null;
                }
            }
            else
            {
                this.transform.position = LastPosition;
                //card_Display = null;
            }
        }
        */

        //SinglePlayer
        if (cardManager.allLightsOff == false)
        {
            Vector2 PosMouse = Camera.ScreenToWorldPoint(Input.mousePosition); // Ottiene la posizione del mouse 
            Collider2D hitcollider = Physics2D.OverlapPoint(PosMouse); // Controlla se c'è un oggetto sotto il mouse 

            Debug.Log(hitcollider.name);
            if (hitcollider != null && hitcollider.CompareTag("BoxPlaceCard"))
            {
                this.transform.position = hitcollider.transform.position; // Allinea la carta alla posizione del collider 
                CardRelase = true;
                cardManager.DescreseLight();
                cardManager.DecreseLife();
                cardManager.cardSelect.cardAction.Execute(cardManager.cardSelect, cardManager);// Esegui L'effeto della carta
            }
            else
            {
                this.transform.position = LastPosition;
            }
        }
        else
        {
            this.transform.position = LastPosition;
            //card_Display = null;
        }
    }


}
