using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Foundation : MonoBehaviour
{
    // Manager informations
    private BuildManager buildManager;
    private GameUI gameUI;
    private PlayerStats player;

    // Foundation and tower informations
    private Color defaultFoundationColor;
    private GameObject tower;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = GameManager.Instance.GetComponent<BuildManager>();
        gameUI = GameManager.Instance.GetComponent<GameUI>();
        player = GameManager.Instance.GetComponent<PlayerStats>();

        defaultFoundationColor = GetComponent<Renderer>().material.color;
    }


    // Public Functions

    // Check if the tower can be upgraded
    public void UpgradeTowerUI(TowerBehavior selectedTower)
    {
        if (player.GetCurrentAmountOfCurrency() >= selectedTower.GetTowerValue())
        {
            player.DecreaseCurrency(selectedTower.GetTowerValue());
            selectedTower.UpgradeTower();
        }
        else
        {
            gameUI.UpdateUserMessage("Oh no! It looks you don't have enough money left. Damn capitalism!");
        }
    }

    public void SellTower(TowerBehavior selectedTower)
    {
        player.CollectCurrency(selectedTower.GetTowerValue() / 2);
        // deselect tower and deactivate tower-UI
        buildManager.DeselectTower();
        buildManager.DecreasePlayerTowers();
        Destroy(selectedTower.gameObject);
    }

    public GameObject GetTowerInformation()
    {
        return tower;
    }

    // Private Functions
    private void OnMouseDown()
    {
        if (!player.IsGameOver() && !gameUI.IsASubmenuActive())
        {
            if (tower != null)
            {
                if (EventSystem.current.IsPointerOverGameObject()) return; // Prevent click through UI
                buildManager.SelectTower(this);
            }
            else
            {
                if (buildManager.GetTowerToBuild() != null)
                {
                    // Check if max amount of buildable towers is reached
                    if (buildManager.GetAmountOfTowers() < buildManager.GetMaxAmountOfTowers())
                    {
                        // Check if player has enough currency
                        if (player.GetCurrentAmountOfCurrency() >= buildManager.GetCurrencyValueOfTower())
                        {
                            // Build tower
                            tower = (GameObject)Instantiate(buildManager.GetTowerToBuild(), transform.position, transform.rotation);
                            tower.GetComponent<TowerBehavior>().SetTowerElement(buildManager.GetTowerToBuildElement());
                            buildManager.IncreasePlayerTowers();
                            // decrease player currency
                            player.DecreaseCurrency(buildManager.GetCurrencyValueOfTower());
                        }
                        else
                        {
                            gameUI.UpdateUserMessage("Oh no! It looks you don't have enough money left. Damn capitalism!");
                        }
                    }
                    else
                    {
                        gameUI.UpdateUserMessage("Max amount of buildable towers reached!");
                    }
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
        ShowTowerRangeCircle(true);
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = defaultFoundationColor;
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
