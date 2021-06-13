using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Foundation : MonoBehaviour
{
    private Color defaultColor;

    private GameObject tower;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;
    }

    private void OnMouseDown()
    {
        if (!GameManager.Instance.GetComponent<PlayerStats>().IsGameOver() && !GameManager.Instance.GetComponent<GameUI>().IsASubmenuActive())
        {
            if (tower != null)
            {
                if (EventSystem.current.IsPointerOverGameObject()) return; // Prevent click through UI
                print("TOWER FOUND");
                GameManager.Instance.gameObject.GetComponent<BuildManager>().SelectTower(this);
            }
            else
            {
                if (GameManager.Instance.gameObject.GetComponent<BuildManager>().GetTowerToBuild() != null)
                {
                    // check if max amount of buildable towers is reached
                    if (GameManager.Instance.gameObject.GetComponent<BuildManager>().GetAmountOfTowers() <
                        GameManager.Instance.gameObject.GetComponent<BuildManager>().GetMaxAmountOfTowers())
                    {
                        // check if player has enoughth currency
                        if (GameManager.Instance.gameObject.GetComponent<PlayerStats>().GetCurrentAmountOfCurrency() >=
                            GameManager.Instance.gameObject.GetComponent<BuildManager>().GetCurrencyValueOfTower())
                        {
                            // Build tower
                            tower = (GameObject)Instantiate(GameManager.Instance.gameObject.GetComponent<BuildManager>().GetTowerToBuild(),
                                                            transform.position, transform.rotation);
                            tower.GetComponent<TowerBehavior>().SetTowerElement(GameManager.Instance.gameObject.GetComponent<BuildManager>().GetTowerToBuildElement());
                            GameManager.Instance.gameObject.GetComponent<BuildManager>().IncreasePlayerTowers();
                            // decrease player currency
                            GameManager.Instance.gameObject.GetComponent<PlayerStats>().DecreaseCurrency(GameManager.Instance.gameObject.GetComponent<BuildManager>().GetCurrencyValueOfTower());
                        }
                        else
                        {
                            print($"Oh no! It looks you don't have enoughth money left. Damn capitalism!");
                        }
                    }
                    else
                    {
                        print("Max amount of buildable towers reached!!!");
                    }
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
        
        // deselect tower and deactivate tower-UI
        GameManager.Instance.gameObject.GetComponent<BuildManager>().DeselectTower();
        GameManager.Instance.gameObject.GetComponent<BuildManager>().DecreasePlayerTowers();
        Destroy(selectedTower);
    }

    public GameObject GetTowerInformation()
    {
        return tower;
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
        ShowTowerRangeCircle(true);
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = defaultColor;
        ShowTowerRangeCircle(false);
    }

    private void ShowTowerRangeCircle(bool showRangeCircle)
    {
        if (tower != null)
        {
            tower.GetComponent<TowerBehavior>().ChangeRangeCircleState(showRangeCircle);
        }
    }

}
