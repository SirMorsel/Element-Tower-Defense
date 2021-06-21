using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private BuildManager buildManager;

    void Start()
    {
        buildManager = GameManager.Instance.gameObject.GetComponent<BuildManager>();
    }

    public void BuyFireTower()
    {
        buildManager.SetTowerToBuild(Elements.FIRE);
    }

    public void BuyWaterTower()
    {
        buildManager.SetTowerToBuild(Elements.WATER);
    }

    public void BuyElectroTower()
    {
        buildManager.SetTowerToBuild(Elements.ELECTRO);
    }
}
