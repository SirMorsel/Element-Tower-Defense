using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject towerToBuild;
    private GameObject towerToBuildBackUp;
    private Elements towerToBuildElement = Elements.WATER;
    private Foundation selectedTower;

    private GameObject towerUI;

    private int currencyValueOfTower = 100;
    private int maxAmountOfTowers = 13;
    private int amountOfPlayerTowers = 0;

    // Start is called before the first frame update
    void Start()
    {
        towerUI = GameObject.Find("TowerUI");
        print($"TEST {towerUI}");
        towerToBuildBackUp = towerToBuild;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetTowerToBuild()
    {
        // towerToBuild.GetComponent<TowerBehavior>().SetTowerElement(towerToBuildElement);
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
    }

    public void DecreasePlayerTowers()
    {
        amountOfPlayerTowers--;
    }
}
