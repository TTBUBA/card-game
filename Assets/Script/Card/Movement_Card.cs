using ExitGames.Client.Photon;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Movement_Card : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Camera Camera;
    public Vector3 LastPosition; // Ultima posizione valida della carta
    public RectTransform rectTransform;
    public bool CardRelase;


    [SerializeField] private Card_Display card_Display;
    [SerializeField] private CardManager cardManager;
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
        //cardManager.cardSelect = card_Display.Card_Info;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 PosMouse = Camera.ScreenToWorldPoint(Input.mousePosition); // Ottiene la posizione del mouse
        this.transform.position = PosMouse; // Sposta la carta alla posizione del mouse
        PosMouse.z = 0;

        string NameCard = card_Display.Card_Info.name;

        cardManager.cardSelect = card_Display.Card_Info;    
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 PosMouse = Camera.ScreenToWorldPoint(Input.mousePosition); // Ottiene la posizione del mouse 
        Collider2D hitcollider = Physics2D.OverlapPoint(PosMouse); // Controlla se c'è un oggetto sotto il mouse 

        if (hitcollider != null && hitcollider.CompareTag("BoxPlaceCard")) 
        {
            this.transform.position = hitcollider.transform.position; // Allinea la carta alla posizione del collider 
            CardRelase = true;
            cardManager.DescreseLight();
        }
        else 
        {
            this.transform.position = LastPosition;
            //card_Display = null;
        }
    }
}
