using System.Text.RegularExpressions;
using Unity.Mathematics;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public Card_Info cardSelect;
    public GameObject[] LightCard;

    public void DescreseLight()
    {

        int LucidaDissativare = cardSelect.Light;
        Debug.Log(LucidaDissativare);
        foreach (var light in LightCard)
        {
            if(LucidaDissativare <= 0) break;

            if (light.activeSelf)
            {
                light.SetActive(false);
                LucidaDissativare--;
            }
        }

        // Aggiorna il valore di Light della carta selezionata
        cardSelect.Light = Mathf.Max(0 , cardSelect.Light - LucidaDissativare);
    }

    public void IncreseLight()
    {
        
        for (int i = 0; i < 10; i++)
        {
            LightCard[i].SetActive(true);
        }
    }
}
