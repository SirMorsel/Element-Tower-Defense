using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


// This file handles the whole gameplay UI and his submenus
public class GameUI : MonoBehaviour
{
    private PlayerStats player;
    private BuildManager buildManager;
    private WaveSpawner waveSpawner;

    // Main UI Elements
    private GameObject uiElements;
    private Text playerHealthText;
    private Text playerCurrencyText;
    private Text amountOfTowersText;
    private Text waveNumberText;
    private Text nextWaveCountdownText;
    private Button waveSpawnButton;
    private Button quitButton;
    private Button optionsButton;

    // Game Over UI Elements
    private GameObject gameOverUIPanel;
    private Text scoreText;

    // Submenu UI Elements
    private GameObject quitMenuPanel;

    // Options UI Elements
    private GameObject optionsMenuUIPanel;
    private Slider bgmSlider;
    private Slider sfxSlider;

    // Shop UI Elements
    private GameObject shopUIPanel;

    private GameObject fireTowerIcon;
    private GameObject waterTowerIcon;
    private GameObject electroTowerIcon;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.GetComponent<PlayerStats>();
        buildManager = GameManager.Instance.GetComponent<BuildManager>();
        waveSpawner = GameManager.Instance.GetComponent<WaveSpawner>();

        // Initalize UI Elements
        InitializeMainUI();
        InitializeOptionsUI();
        InitializeShopUI();
        InitializeQuitUI();
        InitializeGameOverUI();

        // Hide Submenues
        gameOverUIPanel.SetActive(player.IsGameOver());
        quitMenuPanel.SetActive(false);
        optionsMenuUIPanel.SetActive(false);

        // Set Player start values
        UpdatePlayerCurrencyInUI(player.GetCurrentAmountOfCurrency());
        UpdatePlayerHealthInUI(player.GetCurrentHealth(), player.GetMaxHealth());
        UpdateAmountOfTowerUI(buildManager.GetAmountOfTowers(), buildManager.GetMaxAmountOfTowers());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetCurrentHealth() <= 0 && !player.IsGameOver())
        {
            quitButton.interactable = false;
            optionsButton.interactable = false;
            GameOver();
        }
    }

    // Public Functions
    public void UpdateAmountOfTowerUI(int amountOfBuildedTowers, int maxAmountOfTowers)
    {
        amountOfTowersText.text = $"Amount Towers: {amountOfBuildedTowers} / {maxAmountOfTowers}";
    }

    public void UpdateWaveNumberInUI()
    {
        waveNumberText.text = $"Wave {waveSpawner.GetWaveNumber() - 1}";
    }

    public void UpdatePlayerCurrencyInUI(int currency)
    {
        playerCurrencyText.text = $"Currency: {currency}";
    }

    public void UpdatePlayerHealthInUI(int currentHealth, int maxHealth)
    {
        playerHealthText.text = $"Health: {currentHealth} / {maxHealth}";
    }

    public void GameOver()
    {
        player.SetGameOver();
        gameOverUIPanel.SetActive(player.IsGameOver());
        quitMenuPanel.SetActive(false);
        optionsMenuUIPanel.SetActive(false);
        scoreText.text = $"Reached Wave: {waveSpawner.GetWaveNumber() - 1}";
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
        optionsMenuUIPanel.SetActive(false);
        quitMenuPanel.SetActive(!quitMenuPanel.activeInHierarchy);
    }

    public void ChangeOptionsMenuPanelState()
    {
        quitMenuPanel.SetActive(false);
        optionsMenuUIPanel.SetActive(!optionsMenuUIPanel.activeInHierarchy);
    }

    public bool IsASubmenuActive()
    {
        if (quitMenuPanel.activeInHierarchy || optionsMenuUIPanel.activeInHierarchy || gameOverUIPanel.activeInHierarchy)
        {
            return true;
        }
        return false;
    }

    // Volume Functions
    public void BGMVolume()
    {
        AudioManager.Instance.SetBMGVolume(bgmSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }

    // Private Functions
    private void InitializeMainUI()
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
                    UpdateWaveNumberInUI();
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
                case "OptionsButton":
                    optionsButton = uiElements.transform.GetChild(i).GetComponent<Button>();
                    break;
                default:
                    print("Unassigned element detected"); // Just for debug
                    break;
            }
        }
    }

    private void InitializeOptionsUI()
    {
        optionsMenuUIPanel = GameObject.Find("Canvas/OptionsMenuPanel");

        bgmSlider = optionsMenuUIPanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Slider>();
        sfxSlider = optionsMenuUIPanel.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Slider>();
        bgmSlider.value = AudioManager.Instance.GetBMGVolume();
        sfxSlider.value = AudioManager.Instance.GetSFXVolume();
    }

    private void InitializeShopUI()
    {
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
    }

    private void InitializeQuitUI()
    {
        quitMenuPanel = GameObject.Find("Canvas/QuitMenuPanel");
    }

    private void InitializeGameOverUI()
    {
        gameOverUIPanel = GameObject.Find("Canvas/GameOverPanel");
        scoreText = gameOverUIPanel.transform.GetChild(1).GetComponent<Text>();
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
