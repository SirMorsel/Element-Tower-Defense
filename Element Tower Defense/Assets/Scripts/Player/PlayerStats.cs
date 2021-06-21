using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // Manager informations
    private GameUI gameUI;
    private Town playerTown;

    // Player informations
    private int maxHealth = 25;
    private int health;
    private int currency = 500;

    // Player state
    private bool gameIsOver = false;

    // Start is called before the first frame update
    void Awake()
    {
        gameUI = GameManager.Instance.GetComponent<GameUI>();
        playerTown = GameObject.Find("PlayerTown").GetComponent<Town>();
        health = maxHealth;
    }

    // Public Functions
    public int GetCurrentHealth()
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetCurrentAmountOfCurrency()
    {
        return currency;
    }

    public void TakeDamage()
    {
        if (!gameIsOver)
        {
            health--;
            gameUI.UpdatePlayerHealthInUI(health, maxHealth);
            playerTown.UpdatePlayerTownStatus(health);
        }
    }

    public void CollectCurrency(int amountOfEarnedCurrency)
    {
        currency += amountOfEarnedCurrency;
        gameUI.UpdatePlayerCurrencyInUI(currency);
    }

    public void DecreaseCurrency(int costs)
    {
        currency -= costs;
        gameUI.UpdatePlayerCurrencyInUI(currency);
    }

    public bool IsGameOver()
    {
        return gameIsOver;
    }

    public void SetGameOver()
    {
        gameIsOver = true;
    }
}
