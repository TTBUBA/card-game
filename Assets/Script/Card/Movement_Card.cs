using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement_Card : MonoBehaviourPunCallbacks, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Camera Camera;
    public Vector3 LastPosition;
    public bool CardRelase;
    public Card_Info CardSelection;
    [SerializeField] private Card_Display card_Display;
    [SerializeField] private CardManager cardManager;
    private PhotonView photonView;
    private RectTransform rectTransform;
    private bool isDragging = false;

    public void Start()
    {
        photonView = GetComponent<PhotonView>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetObject(CardManager CardManager)
    {
        card_Display = GetComponent<Card_Display>();
        cardManager = CardManager;
    }

    public void SetPositionCard()
    {
        rectTransform = GetComponent<RectTransform>();
        Vector3 worldPosition = rectTransform.TransformPoint(Vector3.zero);
        worldPosition.z = 0;
        LastPosition = worldPosition;
    }

    public void SetCamera(Camera camera)
    {
        Camera = camera;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!photonView.IsMine) return;

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
        if (!photonView.IsMine) return;

        isDragging = true;
        Vector3 posMouse = Camera.ScreenToWorldPoint(Input.mousePosition);
        posMouse.z = 0;

        // Aggiorna la posizione localmente
        transform.position = posMouse;

        // Sincronizza la posizione con altri client
        photonView.RPC("SyncPosition", RpcTarget.All, posMouse);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!photonView.IsMine) return;

        isDragging = false;

        if (card_Display.IsEnemy && cardManager.allLightsOffEnemy ||
            !card_Display.IsEnemy && cardManager.allLightsOffPlayer)
        {
            ResetCardPosition();
            return;
        }

        Vector2 posMouse = Camera.ScreenToWorldPoint(Input.mousePosition);
        if (tryPositionCard(posMouse, card_Display.IsEnemy ? "PlaceCardEnemy" : "PlaceCardPlayer"))
        {
            // Sincronizza la posizione finale e l'azione della carta
            photonView.RPC("SyncFinalPosition", RpcTarget.All, transform.position);
            photonView.RPC("SyncCardAction", RpcTarget.All);
        }
        else
        {
            ResetCardPosition();
            photonView.RPC("SyncPosition", RpcTarget.All, LastPosition);
        }
    }

    [PunRPC]
    private void SyncPosition(Vector3 newPosition)
    {
        if (!photonView.IsMine)
        {
            transform.position = newPosition;
        }
    }

    [PunRPC]
    private void SyncFinalPosition(Vector3 finalPosition)
    {
        transform.position = finalPosition;
        CardRelase = true;
    }

    [PunRPC]
    private void SyncCardAction()
    {
        if (!photonView.IsMine)
        {
            ExecuteCardAction();
        }
    }

    private bool tryPositionCard(Vector2 position, string TargetType)
    {
        Collider2D hitcollider = Physics2D.OverlapPoint(position);
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
            cardManager.cardSelectPlayer.cardAction?.Execute(cardManager.cardSelectPlayer, cardManager);
        }
        else
        {
            cardManager.DescreseLightEnemy();
            cardManager.cardSelectEnemy.cardAction?.Execute(cardManager.cardSelectEnemy, cardManager);
        }

        if (cardManager.CardRelese >= 1)
        {
            cardManager.OnbothCardPlace();
        }
        cardManager.CardRelese++;
    }

    private void ResetCardPosition()
    {
        transform.position = LastPosition;
        photonView.RPC("SyncPosition", RpcTarget.All, LastPosition);
    }
}