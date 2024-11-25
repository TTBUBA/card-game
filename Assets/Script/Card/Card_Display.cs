using UnityEngine;
using UnityEngine.UI;

public class Card_Display : MonoBehaviour
{
    public Card_Info Card_Info;
    public int AttackCard;
    public int DefeseCard;
    public int LightCard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LightCard = Card_Info.Light;
        AttackCard = Card_Info.Attack;
        DefeseCard = Card_Info.Defese;
    }

}
