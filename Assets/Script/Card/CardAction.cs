using UnityEngine;

//Classe astratta che rappresenta un'azione generica della carta
public abstract class CardAction : ScriptableObject
{
    public abstract void Execute(Card_Info card, CardManager cardManager); // Riceve le informazioni della carta e cardManager della carte come parametri
}

[CreateAssetMenu(fileName = "IncreseLife")]
public class IncreseLive : CardAction
{
    public int lifeIncrese = 10;

    // Esegue l'azione
    public override void Execute(Card_Info card, CardManager cardManager)
    {
        cardManager.Life_Player += lifeIncrese;
        Debug.Log($"La carta {card.cardName} vita recupera{lifeIncrese}");
    }
}


