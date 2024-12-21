using JetBrains.Annotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [Header("Player")]
    public Card_Info cardSelectPlayer;
    public GameObject[] LightCardPlayer;
    public bool allLightsOffPlayer;

    [Header("Enemy")]
    public Card_Info cardSelectEnemy;
    public GameObject[] LightCardEnemy;
    public bool allLightsOffEnemy;

    [Header("UI")]
    public Text Text_ValueLifePlayer;
    public Text Text_ValueLifeEnemy;

    [Header("Animation")]
    public Animation WinPlayer;
    public Animation WinEnemy;
    public Animation Parita;

    public int Life_Player = 20;
    public int Life_Enemy = 20;

    public int CardRelese = 0;

    
    public void Update()
    {
        UpdateUi();
    }

    // Metodo per modificare lo stato delle luci (spegnere un certo numero di luci attive)
    public void ModifyLight(GameObject[] lights, int lightAmount, bool allLightsOff)
    {
        foreach (var light in lights)
        {
            if (lightAmount <= 0) break; // Interrompe il ciclo se non ci sono più luci da spegnere

            if (light.activeSelf) // Controlla se la luce è attiva
            {
                light.SetActive(false); // Spegne la luce
                lightAmount--; // Riduce il numero di luci da spegnere
            }
        }

        // Conta quante luci sono spente e aggiorna lo stato `allLightsOff`
        int deactivatedLightCount = lights.Count(obj => obj != null && !obj.activeSelf);
        allLightsOff = deactivatedLightCount >= lights.Length;
    }

    // Riduce il numero di luci del giocatore 
    public void DescreseLightPlayer()
    {
        ModifyLight(LightCardPlayer, cardSelectPlayer.light, allLightsOffPlayer); // Spegne le luci
        cardSelectPlayer.light = Mathf.Max(0, cardSelectPlayer.light - LightCardEnemy.Count(obj => obj != null && !obj.activeSelf));
    }

    // Riduce il numero di luci del nemico 
    public void DescreseLightEnemy()
    {
        ModifyLight(LightCardEnemy, cardSelectEnemy.light, allLightsOffEnemy); // Spegne le luci
        cardSelectEnemy.light = Mathf.Max(0, cardSelectEnemy.light - LightCardPlayer.Count(obj => obj != null && !obj.activeSelf));
    }

    // Metodo per aumentare le luci 
    public void IncreseLight()
    {
        for (int i = 0; i < 10; i++)
        {
            // Logica per riattivare le luci, attualmente commentata
            // LightCard[i].SetActive(true);
            // allLightsOff = false;
        }
    }

    // Metodo per ridurre la vita di giocatore e nemico
    public void CompareCardsAndApplyDamage()
    {

        // Calcola il valore totale di ogni carta
        int playerCardTotal = cardSelectPlayer.attack + cardSelectPlayer.defense;
        int enemyCardTotal = cardSelectEnemy.attack + cardSelectEnemy.defense;
        Debug.Log(playerCardTotal);
        Debug.Log(enemyCardTotal);

        //determina il vincitore 
        if (playerCardTotal > enemyCardTotal)
        {
            //player vince 
            int damage = playerCardTotal - enemyCardTotal;
            Life_Enemy = Mathf.Max(0, Life_Enemy - damage);
            WinPlayer.Play();
        }
        else if(enemyCardTotal > playerCardTotal)
        {
            //Nemico Vince vince 
            int damage = enemyCardTotal - playerCardTotal;
            Life_Player = Mathf.Max(0, Life_Player - damage);
            WinEnemy.Play();
        }
        else
        {
            Parita.Play();
            Debug.Log("Pari");
        }

        CardRelese = -1;
        UpdateUi();
    }

    public void OnbothCardPlace()
    {
        CompareCardsAndApplyDamage();
    }
    // Metodo privato per aggiornare i valori della UI con la vita corrente
    private void UpdateUi()
    {
        Text_ValueLifeEnemy.text = Life_Enemy.ToString(); // Aggiorna il testo della vita del nemico
        Text_ValueLifePlayer.text = Life_Player.ToString(); // Aggiorna il testo della vita del giocatore
    }
}
