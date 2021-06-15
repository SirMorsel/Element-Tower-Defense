using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private int maxHealth = 25;
    private int health;
    private int currency = 500;

    private bool gameIsOver = false;

    private GameObject playerTown;

    // UI Elements

    // Start is called before the first frame update
    void Awake()
    {
        playerTown = GameObject.Find("PlayerTown");
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

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
            playerTown.GetComponent<Town>().UpdatePlayerTownStatus(health);
        }
    }

    public void CollectCurrency(int amountOfEarnedCurrency)
    {
        currency += amountOfEarnedCurrency;
    }

    public void DecreaseCurrency(int costs)
    {
        currency -= costs;
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
