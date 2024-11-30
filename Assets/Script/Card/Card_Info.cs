using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card")]
public class Card_Info : ScriptableObject
{
    public string NameCard;
    public string TypeEra;
    public string DescriptionCard;
    public int Attack;
    public int Defese;
    public Image ImageCard;
    public int Light;
    public bool IsEnemy;
    public CardAction cardAction;
}
