using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyFireTower()
    {
        GameManager.Instance.gameObject.GetComponent<BuildManager>().SetTowerToBuild(Elements.FIRE);
        print("FIRE");
    }

    public void BuyWaterTower()
    {
        GameManager.Instance.gameObject.GetComponent<BuildManager>().SetTowerToBuild(Elements.WATER);
        print("WATER");
    }

    public void BuyElectroTower()
    {
        GameManager.Instance.gameObject.GetComponent<BuildManager>().SetTowerToBuild(Elements.ELECTRO);
        print("ELECTRO");
    }
}
