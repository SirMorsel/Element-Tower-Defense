﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private int maxHealth = 25;
    private int health;
    private int currency = 100;

    // UI Elements

    // Start is called before the first frame update
    void Start()
    {
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
        health--;
    }

    public void CollectCurreny()
    {
        currency += 25;
    }


}