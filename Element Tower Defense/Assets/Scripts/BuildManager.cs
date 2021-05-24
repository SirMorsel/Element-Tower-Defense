using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject towerToBuild;
    private Elements towerToBuildElement = Elements.WATER;

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
}
