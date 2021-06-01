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

    //private bool gameIsOver;

    // Start is called before the first frame update
    void Start()
    {
        InitializeUiElements();
        player = this.GetComponent<PlayerStats>().gameObject;
       // gameIsOver = false;
        gameOverUIPanel.SetActive(player.GetComponent<PlayerStats>().IsGameOver());
        subMenuUIPanel.SetActive(false);
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
        //gameIsOver = true;
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
}
