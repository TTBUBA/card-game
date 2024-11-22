using UnityEngine;
using UnityEngine.UI;

public class Card_Display : MonoBehaviour
{
    public Card_Info Card_Info;

    public int LightCard;
    public string test;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Card_Info.owner = test;
        LightCard = Card_Info.Light;
    }

}
