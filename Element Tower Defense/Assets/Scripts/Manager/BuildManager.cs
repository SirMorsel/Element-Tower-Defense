﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject towerToBuild;
    private GameObject towerToBuildBackUp;
    private Elements towerToBuildElement = Elements.WATER;
    private Foundation selectedTower;

    private GameObject towerUI;
    private GameUI gameUI;


    private int currencyValueOfTower = 100;
    private int maxAmountOfTowers = 13;
    private int amountOfPlayerTowers = 0;
    private int towerBaseValue = 100;

    // Start is called before the first frame update
    void Start()
    {
        towerUI = GameObject.Find("TowerUI");
        gameUI = GameManager.Instance.GetComponent<GameUI>();
        print($"TEST {towerUI}");
        towerToBuildBackUp = towerToBuild;
        towerToBuild = null;
    }

    public GameObject GetTowerToBuild()
    {
        return towerToBuild;
    }

    public void SelectTower(Foundation foundation)
    {
        if (selectedTower == foundation)
        {
            DeselectTower();
        }
        else
        {
            selectedTower = foundation;
            towerToBuild = null;
            towerUI.GetComponent<TowerUI>().SetTarget(foundation);
        }

    }

    public void DeselectTower()
    {
        if (selectedTower != null)
        {
            selectedTower.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().ChangeRangeCircleState(false); ;
        }
        selectedTower = null;
        towerToBuild = towerToBuildBackUp; // refactor
        towerUI.GetComponent<TowerUI>().HideUiElement();

    }

    public void SetTowerToBuild(Elements towerType)
    {
        DeselectTower();
        towerToBuildElement = towerType;
    }

    public Elements GetTowerToBuildElement()
    {
        return towerToBuildElement;
    }

    public int GetCurrencyValueOfTower()
    {
        return currencyValueOfTower;
    }


    public int GetAmountOfTowers()
    {
        return amountOfPlayerTowers;
    }

    public int GetMaxAmountOfTowers()
    {
        return maxAmountOfTowers;
    }

    public void IncreasePlayerTowers()
    {
        amountOfPlayerTowers++;
        gameUI.UpdateAmountOfTowerUI(amountOfPlayerTowers, maxAmountOfTowers);
    }

    public void DecreasePlayerTowers()
    {
        amountOfPlayerTowers--;
        gameUI.UpdateAmountOfTowerUI(amountOfPlayerTowers, maxAmountOfTowers);
    }

    public int GetTowerBaseValue()
    {
        return towerBaseValue;
    }
}
