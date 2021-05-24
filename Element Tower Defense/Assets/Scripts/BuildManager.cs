using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject towerToBuild;
    private Elements towerToBuildElement = Elements.WATER;

    private int maxAmountOfTowers = 13;
    private int amountOfPlayerTowers = 0;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void SetTowerToBuild(Elements towerType)
    {
        towerToBuildElement = towerType;
    }

    public Elements GetTowerToBuildElement()
    {
        return towerToBuildElement;
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
