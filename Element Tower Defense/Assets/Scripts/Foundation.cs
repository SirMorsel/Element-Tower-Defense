using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour
{
    private Color defaultColor;

    private GameObject tower;
    


    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {

        if (tower != null)
        {
            // Update tower || sell tower
            print("TOWER FOUND");
            GameManager.Instance.gameObject.GetComponent<BuildManager>().SelectTower(this);
            // UpgradeTowerUI(tower);
            // SellTower(tower);
        }
        else
        {
            if (GameManager.Instance.gameObject.GetComponent<BuildManager>().GetTowerToBuild() != null)
            {
                if (GameManager.Instance.gameObject.GetComponent<BuildManager>().GetAmountOfTowers() <
                    GameManager.Instance.gameObject.GetComponent<BuildManager>().GetMaxAmountOfTowers())
                {
                    // Build tower
                    tower = (GameObject)Instantiate(GameManager.Instance.gameObject.GetComponent<BuildManager>().GetTowerToBuild(),
                                                    transform.position + new Vector3(0f, 1f, 0f), transform.rotation);
                    tower.GetComponent<TowerBehavior>().SetTowerElement(GameManager.Instance.gameObject.GetComponent<BuildManager>().GetTowerToBuildElement());
                    GameManager.Instance.gameObject.GetComponent<BuildManager>().IncreasePlayerTowers();
                }
                else
                {
                    print("Max amount of buildable towers reached!!!");
                }
            }
        }
    }

    public void UpgradeTowerUI(GameObject selectedTower)
    {
        print($"Value of tower {selectedTower.GetComponent<TowerBehavior>().GetTowerValue()}");
        if (GameManager.Instance.gameObject.GetComponent<PlayerStats>().GetCurrentAmountOfCurrency() >= selectedTower.GetComponent<TowerBehavior>().GetTowerValue())
        {
            GameManager.Instance.gameObject.GetComponent<PlayerStats>().DecreaseCurrency(selectedTower.GetComponent<TowerBehavior>().GetTowerValue());
            selectedTower.GetComponent<TowerBehavior>().UpgradeTower();
            print($"Lv of tower is: {selectedTower.GetComponent<TowerBehavior>().GetTowerLv()}");
        }
    }

    public void SellTower(GameObject selectedTower)
    {
        print($"Sell tower for {selectedTower.GetComponent<TowerBehavior>().GetTowerValue() / 2}");
        GameManager.Instance.gameObject.GetComponent<PlayerStats>().CollectCurrency(selectedTower.GetComponent<TowerBehavior>().GetTowerValue() / 2);
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = defaultColor;
    }

}
