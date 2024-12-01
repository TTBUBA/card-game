using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card")]
public class Card_Info : ScriptableObject
{
    [Header("Card Basic Information")]
    public string cardName;
    public string cardDescription;
    public Sprite cardImage;
    public string cardType;

    [Header("Card Stats")]
    public int attack;
    public int defense;
    public int light;
    public bool isEnemy;

    [Header("Card Ability")]
    public CardAction cardAction;

}

