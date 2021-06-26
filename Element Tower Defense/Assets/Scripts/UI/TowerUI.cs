using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    // Manager informations
    private GameUI gameUI;

    // Building informations
    private Foundation foundation;
    private TowerBehavior tower;

    // UI Elements
    private GameObject ui;
    private Button upgradeButton;
    private Button sellButton;
    private Text towerInfoText;
    private Text towerUpgradeInfoText;

    // Start is called before the first frame update
    void Start()
    {
        gameUI = GameManager.Instance.GetComponent<GameUI>();
        InitializeTowerUI();
    }

    // Public Functions
    public void InitializeTowerUI()
    {
        ui = transform.GetChild(0).gameObject;
        towerInfoText = ui.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        towerUpgradeInfoText = ui.transform.GetChild(0).GetChild(1).GetComponent<Text>();

        upgradeButton = ui.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Button>();
        sellButton = ui.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>();

        // Add events to buttons
        upgradeButton.onClick.AddListener(UpgradeButton_OnClick);
        sellButton.onClick.AddListener(SellButton_OnClick);
    }

    public void SetTarget(Foundation target)
    {
        foundation = target;
        transform.position = target.transform.position + new Vector3(0, 2.4f,0);
        ui.SetActive(true);
        UpdateUIText();
    }

    public void HideUiElement()
    {
        ui.SetActive(false);
    }

    public void UpdateUIText()
    {
        tower = foundation.GetTowerInformation().GetComponent<TowerBehavior>();
        towerInfoText.text = $"{tower.GetTowerType()} Lv: {tower.GetTowerLv()} \n " + // show tower lv and type
                             $"{TowerEffectivenessInformation(tower.GetTowerType())}"; // show tower effectivnsess

        // Current information text / Update information text
        if (tower.GetTowerLv() < tower.GetTowerMaxLv())
        {
            towerUpgradeInfoText.text = 
                $"Dmg Lv {tower.GetTowerLv()}: {tower.GetTowerLv() * tower.GetTowerDamage()}" +
                $" => " +
                $"Dmg Lv {(tower.GetTowerLv() + 1)}: <color=#006400ff>{(tower.GetTowerLv() + 1) * tower.GetTowerDamage()}</color>";
            upgradeButton.gameObject.SetActive(true);
            upgradeButton.GetComponentInChildren<Text>().text = $"Upgrade (${tower.GetTowerValue()})";
        } else
        {
            towerUpgradeInfoText.text = $"Dmg Lv {tower.GetTowerLv()}: {tower.GetTowerLv() * tower.GetTowerDamage()}";
            upgradeButton.gameObject.SetActive(false);
        }

        sellButton.GetComponentInChildren<Text>().text = $"Sell (${tower.GetTowerValue() / 2})";
    }

    // Private Functions
    private string TowerEffectivenessInformation(Elements towerType)
    {
        const string colorTextElectro ="<color=#aa00aaff>Electro</color>";
        const string colorTextFire = "<color=#ff0000ff>Fire</color>";
        const string colorTextWater = "<color=#0000ffff>Water</color>";

        string towerInfoText = "";
        switch (towerType)
        {
            case Elements.ELECTRO:
                towerInfoText = $" Effective against {colorTextWater} \n Not effecitve against {colorTextFire} \n No damage against {colorTextElectro}";
                break;
            case Elements.FIRE:
                towerInfoText = $" Effective against {colorTextElectro} \n Not effecitve against {colorTextWater} \n No damage against {colorTextFire}";
                break;
            case Elements.WATER:
                towerInfoText = $" Effective against {colorTextFire} \n Not effecitve against {colorTextElectro} \n No damage against {colorTextWater}";
                break;
            default:
                break;
        }

        return towerInfoText;
    }

    // Button Events
    private void UpgradeButton_OnClick()
    {
        if (!gameUI.IsASubmenuActive())
        {
            foundation.UpgradeTowerUI(tower);
        }
        UpdateUIText();
    }

    private void SellButton_OnClick()
    {
        if (!gameUI.IsASubmenuActive())
        {
            foundation.SellTower(tower);
        }
    }
}
