using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    private GameObject player;

    // Main UI Elements
    private GameObject uiElements;
    private Text playerHealthText;
    private Text playerCurrencyText;
    private Text amountOfTowersText;
    private Text waveNumberText;
    private Text nextWaveCountdownText;
    private Button waveSpawnButton;
    private Button quitButton;

    // Game Over UI Elements
    private GameObject gameOverUIPanel;
    private Text scoreText;

    // Submenu UI Elements
    private GameObject subMenuUIPanel;

    // Shop UI Elements
    private GameObject shopUIPanel;

    private GameObject fireTowerIcon;
    private GameObject waterTowerIcon;
    private GameObject electroTowerIcon;

    // Start is called before the first frame update
    void Start()
    {
        InitializeUiElements();
        player = this.GetComponent<PlayerStats>().gameObject;
        gameOverUIPanel.SetActive(player.GetComponent<PlayerStats>().IsGameOver());
        subMenuUIPanel.SetActive(false);

        //TEST WIP!!!!!!!!!!!!!!!
        
        print($"BGM: {AudioManager.Instance.GetBMGVolume()} SFX: {AudioManager.Instance.GetSFXVolume()}");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerStats>().GetCurrentHealth() <= 0 && !player.GetComponent<PlayerStats>().IsGameOver())
        {
            quitButton.interactable = false;
            GameOver();
        }
        else
        {
            UpdateUI();
        }
    }

    private void InitializeUiElements()
    {
        uiElements = GameObject.Find("Canvas");
        for (int i = 0; i < uiElements.transform.childCount; i++)
        {
            switch (uiElements.transform.GetChild(i).name)
            {
                case "PlayerHealthText":
                    playerHealthText = uiElements.transform.GetChild(i).GetComponent<Text>();
                    break;
                case "PlayerCurrencyAmountText":
                    playerCurrencyText = uiElements.transform.GetChild(i).GetComponent<Text>();
                    break;
                case "WaveAmountText":
                    waveNumberText = uiElements.transform.GetChild(i).GetComponent<Text>();
                    break;
                case "AmountOfTowerText":
                    amountOfTowersText = uiElements.transform.GetChild(i).GetComponent<Text>();
                    break;
                case "WaveCountdownText":
                    nextWaveCountdownText = uiElements.transform.GetChild(i).GetComponent<Text>();
                    break;
                case "TriggerWaveButton":
                    waveSpawnButton = uiElements.transform.GetChild(i).GetComponent<Button>();
                    break;
                case "QuitButton":
                    quitButton = uiElements.transform.GetChild(i).GetComponent<Button>();
                    break;
                default:
                    print("Unassigned element detected"); // Just for debug
                    break;
            }
        }

        gameOverUIPanel = GameObject.Find("Canvas/GameOverPanel");
        subMenuUIPanel = GameObject.Find("Canvas/SubMenuPanel");

        shopUIPanel = GameObject.Find("Canvas/BuildTurretPanel");
        for (int i = 0; i < shopUIPanel.transform.childCount; i++)
        {
            switch (shopUIPanel.transform.GetChild(i).name)
            {
                case "TurretFireButton":
                    fireTowerIcon = shopUIPanel.transform.GetChild(i).gameObject;
                    fireTowerIcon.GetComponent<Button>().onClick.AddListener(FireTowerShopButton_OnClick);
                    ChangeAlphaOfImage(fireTowerIcon.GetComponent<Image>(), 0.5f);
                    break;
                case "TurretWaterButton":
                    waterTowerIcon = shopUIPanel.transform.GetChild(i).gameObject;
                    waterTowerIcon.GetComponent<Button>().onClick.AddListener(WaterTowerShopButton_OnClick);
                    ChangeAlphaOfImage(waterTowerIcon.GetComponent<Image>(), 0.5f);
                    break;
                case "TurretElectroButton":
                    electroTowerIcon = shopUIPanel.transform.GetChild(i).gameObject;
                    electroTowerIcon.GetComponent<Button>().onClick.AddListener(ElectroTowerShopButton_OnClick);
                    ChangeAlphaOfImage(electroTowerIcon.GetComponent<Image>(), 0.5f);
                    break;
                default:
                    print("Unassigned element detected"); // Just for debug
                    break;
            }
        }
        scoreText = gameOverUIPanel.transform.GetChild(1).GetComponent<Text>();
    }

    public void UpdateUI()
    {
        waveNumberText.text = $"Wave {this.GetComponent<WaveSpawner>().GetWaveNumber() - 1}";
        playerHealthText.text = $"Health: " +
            $"{player.GetComponent<PlayerStats>().GetCurrentHealth()}/" +
            $"{player.GetComponent<PlayerStats>().GetMaxHealth()}";
        playerCurrencyText.text = $"Currency: {player.GetComponent<PlayerStats>().GetCurrentAmountOfCurrency()}";
        amountOfTowersText.text = $"Amount Towers: {GameManager.Instance.gameObject.GetComponent<BuildManager>().GetAmountOfTowers()} /" +
                                  $"{GameManager.Instance.gameObject.GetComponent<BuildManager>().GetMaxAmountOfTowers()}";

    }

    public void GameOver()
    {
        player.GetComponent<PlayerStats>().SetGameOver();
        gameOverUIPanel.SetActive(player.GetComponent<PlayerStats>().IsGameOver());
        scoreText.text = $"Reached Wave: {this.GetComponent<WaveSpawner>().GetWaveNumber() - 1}";
    }

    public void TimerTextUpdate(float countdown)
    {
        nextWaveCountdownText.text = "Next wave in: " + countdown.ToString("F0");
        if (countdown < 10)
        {
            nextWaveCountdownText.color = Color.red;
        } else
        {
            nextWaveCountdownText.color = Color.white;
        }
    }

    public void ChangeWaveSpawnBtnStage(bool state)
    {
        waveSpawnButton.interactable = state;
    }

    public void ChangeCountdownTextStat(bool state)
    {
        nextWaveCountdownText.enabled = state;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeSubMenuPanelState()
    {
        subMenuUIPanel.SetActive(!subMenuUIPanel.activeInHierarchy);
    }

    private void ChangeAlphaOfImage(Image img, float alphaValue)
    {
        var tempColor = img.color;
        tempColor.a = alphaValue;
        img.GetComponent<Image>().color = tempColor;
    }

    // Button Events
    private void FireTowerShopButton_OnClick()
    {
        ChangeAlphaOfImage(fireTowerIcon.GetComponent<Image>(), 1f);
        ChangeAlphaOfImage(waterTowerIcon.GetComponent<Image>(), 0.5f);
        ChangeAlphaOfImage(electroTowerIcon.GetComponent<Image>(), 0.5f);
        shopUIPanel.GetComponent<Shop>().BuyFireTower();
    }

    private void WaterTowerShopButton_OnClick()
    {
        ChangeAlphaOfImage(fireTowerIcon.GetComponent<Image>(), 0.5f);
        ChangeAlphaOfImage(waterTowerIcon.GetComponent<Image>(), 1f);
        ChangeAlphaOfImage(electroTowerIcon.GetComponent<Image>(), 0.5f);
        shopUIPanel.GetComponent<Shop>().BuyWaterTower();
    }

    private void ElectroTowerShopButton_OnClick()
    {
        ChangeAlphaOfImage(fireTowerIcon.GetComponent<Image>(), 0.5f);
        ChangeAlphaOfImage(waterTowerIcon.GetComponent<Image>(), 0.5f);
        ChangeAlphaOfImage(electroTowerIcon.GetComponent<Image>(), 1f);
        shopUIPanel.GetComponent<Shop>().BuyElectroTower();
    }
}
