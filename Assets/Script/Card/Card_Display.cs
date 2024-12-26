using UnityEngine;
using UnityEngine.UI;

public class Card_Display : MonoBehaviour
{
    public Card_Info Card_Info;
    public CardManager CardManager;
    public int AttackCard;
    public int DefeseCard;
    public int LightCard;
    public bool IsEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LightCard = Card_Info.attack;
        AttackCard = Card_Info.attack;
        DefeseCard = Card_Info.defense;     
    }

}
