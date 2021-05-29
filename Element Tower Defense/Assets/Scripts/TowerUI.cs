using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{

    private Foundation target;
    private GameObject ui;
    private Button upgradeButton;
    private Button sellButton;
    private Text towerInfoText;

    // Start is called before the first frame update
    void Start()
    {
        ui = this.transform.GetChild(0).gameObject;
        towerInfoText = ui.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        upgradeButton = ui.transform.GetChild(0).GetChild(1).GetComponent<Button>();
        sellButton = ui.transform.GetChild(0).GetChild(2).GetComponent<Button>();
        //GetTowerInformation
        upgradeButton.onClick.AddListener(UpgradeButton_OnClick);//subscribe to the onClick even
        sellButton.onClick.AddListener(SellButton_OnClick); //subscribe to the onClick even
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
        transform.position = target.transform.position;
        ui.SetActive(true);
    }

    public void HideUiElement()
    {
        ui.SetActive(false);
    }

    public void UpdateUIText()
    {
        towerInfoText.text = $"{target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerType()} \n " + // show tower element type
            $"{target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerLv()} "; // show tower LV
        upgradeButton.GetComponentInChildren<Text>().text = $"Upgrade ({target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerValue()})";
        sellButton.GetComponentInChildren<Text>().text = $"Sell ({target.GetComponent<Foundation>().GetTowerInformation().GetComponent<TowerBehavior>().GetTowerValue() / 2})";
    }

    private void UpgradeButton_OnClick()
    {
        target.GetComponent<Foundation>().UpgradeTowerUI(target.GetComponent<Foundation>().GetTowerInformation());
    }

    private void SellButton_OnClick()
    {
        target.GetComponent<Foundation>().SellTower(target.GetComponent<Foundation>().GetTowerInformation());
    }

}
