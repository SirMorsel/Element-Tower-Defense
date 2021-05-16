using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private GameObject player;
    public Text PlayerHealthText;
    public Text PlayerCurrencyText;

    public bool GameIsOver;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        if (player.GetComponent<PlayerStats>().GetCurrentHealth() <= 0 && !GameIsOver)
        {
            GameOver();
        }
    }

    public void UpdateUI()
    {
        PlayerHealthText.text = $"Health: " +
            $"{player.GetComponent<PlayerStats>().GetCurrentHealth()}/" +
            $"{player.GetComponent<PlayerStats>().GetMaxHealth()}";
        PlayerCurrencyText.text = $"Currency: {player.GetComponent<PlayerStats>().GetCurrentAmountOfCurrency()}";
    }

    public void GameOver()
    {
        GameIsOver = true;
        print("Game over!");
    }
}
