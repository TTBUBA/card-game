using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;


public class Movement_Card : MonoBehaviourPun , IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Camera Camera;
    public Vector3 LastPosition; // Ultima posizione valida della carta
    public RectTransform rectTransform;
    public bool CardRelase;
    public Card_Info CardSelection;

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
        if (card_Display.IsEnemy)
        {
            cardManager.cardSelectEnemy = card_Display.Card_Info;
        }
        else
        {
            cardManager.cardSelectPlayer = card_Display.Card_Info;
        }  
    }

    public void OnDrag(PointerEventData eventData)
    {
        //MULTIPLAYER
        if (photonview.IsMine)
        {
            Vector3 PosMouse = Camera.ScreenToWorldPoint(Input.mousePosition); // Ottiene la posizione del mouse
            this.transform.position = PosMouse; // Sposta la carta alla posizione del mouse
            PosMouse.z = 0;

            string NameCard = card_Display.Card_Info.name;

            cardManager.cardSelectPlayer = card_Display.Card_Info;    
        }
        else if (!photonview.IsMine)
        {
            Vector3 PosMouse = Camera.ScreenToWorldPoint(Input.mousePosition); // Ottiene la posizione del mouse
            this.transform.position = PosMouse; // Sposta la carta alla posizione del mouse
            PosMouse.z = 0;

            string NameCard = card_Display.Card_Info.name;

            cardManager.cardSelectEnemy = card_Display.Card_Info;
        }


        /*SINGLEPLAYER
        Vector3 PosMouse = Camera.ScreenToWorldPoint(Input.mousePosition); // Ottiene la posizione del mouse
        this.transform.position = PosMouse; // Sposta la carta alla posizione del mouse
        PosMouse.z = 0;

        string NameCard = card_Display.Card_Info.name;


        if (card_Display.IsEnemy)
        {
            cardManager.cardSelectEnemy = card_Display.Card_Info;
        }
        else
        {
            cardManager.cardSelectPlayer = card_Display.Card_Info;
        }
        */
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(card_Display.IsEnemy)
        {
            if (cardManager.allLightsOffEnemy)
            {
                ResetCardPosition();
                return;
            }
        }
        else
        {
            if (cardManager.allLightsOffPlayer)
            {
                ResetCardPosition();
                return;
            }
        }
  
        Vector2 PosMouse = Camera.ScreenToWorldPoint(Input.mousePosition); // Ottiene la posizione del mouse 
        string Target = card_Display.IsEnemy ? "PlaceCardEnemy" : "PlaceCardPlayer";

        if(tryPositionCard(PosMouse, Target))
        {
            ExecuteCardAction();
            cardManager.CardRelese++;
        }
        else
        {
            ResetCardPosition();
        }
    }

    private bool tryPositionCard(Vector2 position, string TargetType)
    {
        Collider2D hitcollider = Physics2D.OverlapPoint(position); // Controlla se c'è un oggetto sotto il mouse 

        if (hitcollider != null && hitcollider.CompareTag(TargetType))
        {
            transform.position = hitcollider.transform.position;
            CardRelase = true;
            return true;
        }

        return false;
    }
    private void ExecuteCardAction()
    {
        if (!card_Display.IsEnemy)
        {
            cardManager.DescreseLightPlayer();
            if (cardManager.cardSelectPlayer.cardAction != null)
            {
                cardManager.cardSelectPlayer.cardAction.Execute(cardManager.cardSelectPlayer, cardManager);// Esegue L'effeto della carta quando e il player
            }

            if (cardManager.CardRelese >= 1)
            {
                Debug.Log("Both cards placed, calling OnbothCardPlace()");
                cardManager.OnbothCardPlace();
            }
        }
        else
        {
            cardManager.DescreseLightEnemy();
            if (cardManager.cardSelectEnemy.cardAction != null)
            {
                cardManager.cardSelectEnemy.cardAction.Execute(cardManager.cardSelectEnemy, cardManager);// Esegue L'effeto della carta quando e l'avversario
            }

            if(cardManager.CardRelese >= 1)
            {
                Debug.Log("Both cards placed, calling OnbothCardPlace()");
                cardManager.OnbothCardPlace();
            }
        }
    }
    private void ResetCardPosition()
    {
        transform.position = LastPosition;
    }
}
