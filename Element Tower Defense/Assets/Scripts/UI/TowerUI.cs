using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{

    private Foundation target;
    private GameObject ui;
    private Button upgradeButton;
    private Button sellButton;
    private Text towerInfoText;
    private Text towerUpgradeInfoText;

    // Start is called before the first frame update
    void Start()
    {
        ui = this.transform.GetChild(0).gameObject;
        towerInfoText = ui.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        towerUpgradeInfoText = ui.transform.GetChild(0).GetChild(1).GetComponent<Text>();
        upgradeButton = ui.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Button>();
        sellButton = ui.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Button>();
        upgradeButton.onClick.AddListener(UpgradeButton_OnClick);
        sellButton.onClick.AddListener(SellButton_OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && ui.activeInHierarchy)
        {
            // send infos to buttons
            UpdateUIText();
        }
    }

    public void SetTarget(Foundation target)
    {
        this.target = target;
        transform.position = target.transform.position + new Vector3(0, 2.4f,0);
        ui.SetActive(true);
    }

    public void HideUiElement()
    {
        ui.SetActive(false);
    }

    public void UpdateUIText()
    {
        towerInfoText.text = $"{target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerType()} " + // show tower element type
            $"Lv: {target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerLv()} "; // show tower LV

        if (target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerLv() <
            target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerMaxLv())
        {
            towerUpgradeInfoText.text = 
                $"Dmg Lv {target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerLv()}: " +
                $" {target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerLv() * target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerDamage()}" +
                $" => " +
                $"Dmg Lv {(target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerLv() + 1)}: " +
                $"<color=#006400ff>{(target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerLv() + 1) * target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerDamage()}</color>";
            upgradeButton.gameObject.SetActive(true);
            upgradeButton.GetComponentInChildren<Text>().text = $"Upgrade ({target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerValue()})";
        } else
        {
            towerUpgradeInfoText.text = "";
            upgradeButton.gameObject.SetActive(false);
        }

        sellButton.GetComponentInChildren<Text>().text = $"Sell ({target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerValue() / 2})";
    }

    private void UpgradeButton_OnClick()
    {
        if (!GameManager.Instance.GetComponent<GameUI>().IsASubmenuActive())
        {
            target.GetComponent<Foundation>().UpgradeTowerUI(target.GetComponent<Foundation>().GetTowerInformation());
        }
    }

    private void SellButton_OnClick()
    {
        if (!GameManager.Instance.GetComponent<GameUI>().IsASubmenuActive())
        {
            target.GetComponent<Foundation>().SellTower(target.GetComponent<Foundation>().GetTowerInformation());
        }
    }

}
