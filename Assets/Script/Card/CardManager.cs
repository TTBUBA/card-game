using JetBrains.Annotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public Card_Info cardSelect;
    public Card_Display cardSelectEnemy;
    public GameObject[] LightCard;
    public bool allLightsOff;

    [Header("UI")]
    public Text Text_ValueLifePlayer;
    public Text Text_ValueLifeEnemy;


    public int Life_Player = 20;
    public int Life_Enemy = 20;


    public void Update()
    {
        UpdateUi();
    }
    public void DescreseLight()
    {
        
        int LucidaDissativare = cardSelect.Light;
   
        foreach (var light in LightCard)
        {
            
            if(LucidaDissativare <= 0) break;

            if (light.activeSelf)
            {
                light.SetActive(false);
                LucidaDissativare--;
            }
        }
        // Conta quanti oggetti nell'array sono disattivati.
        int deactivatedLightCount = LightCard.Count(obj  => obj != null && !obj.activeSelf);
        if( deactivatedLightCount >= 10)
        {
            allLightsOff = true;
        }
        // Aggiorna il valore di Light della carta selezionata
        cardSelect.Light = Mathf.Max(0 , cardSelect.Light - LucidaDissativare);


        //DEBUG
        //Debug.Log(LucidaDissativare);
        Debug.Log(deactivatedLightCount);
    }

    public void IncreseLight()
    {
        
        for (int i = 0; i < 10; i++)
        {
            LightCard[i].SetActive(true);
            allLightsOff = false;
        }
    }

    public void DecreseLife()
    {

       int LifePlayer = cardSelect.Attack + cardSelect.Defese;
       int LifeEnemy = cardSelectEnemy.AttackCard + cardSelectEnemy.DefeseCard;
       int Life_Emey_Player = LifePlayer - LifeEnemy;
       int TotalLife = Life_Enemy - Life_Emey_Player;


       Text_ValueLifeEnemy.text = TotalLife.ToString();

       /*DEBUG
        Debug.Log($"AttackPlayer:{cardSelect.Attack} DefesePlayer:{cardSelect.Defese}");
        Debug.Log($"AttackEnemy:{cardSelectEnemy.AttackCard}DefeseEnemy:{cardSelectEnemy.DefeseCard}");
        Debug.Log(LifePlayer);
        Debug.Log(LifeEnemy);
        Debug.Log(TotalLife);
        */
    }

    private void UpdateUi()
    {
        Text_ValueLifeEnemy.text = Life_Enemy.ToString();
        Text_ValueLifePlayer.text = Life_Player.ToString(); 
    }
}
