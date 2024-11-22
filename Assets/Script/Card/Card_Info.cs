using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card")]
public class Card_Info : ScriptableObject
{
    public string owner;
    public string NameCard;
    public string TypeEra;
    public string DescriptionCard;
    public Image ImageCard;
    public int Light;
    
}
