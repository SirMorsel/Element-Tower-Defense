using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // UI Elements
    private GameObject mainMenuPanel;
    private GameObject optionMenuPanel;

    // Volume sliders
    private Slider bgmSlider;
    private Slider sfxSlider;

    void Start()
    {
        // Initalize UI Elements
        mainMenuPanel = GameObject.Find("Canvas/MainPanel");
        InitializeMainMenuOptionsUI();

        // Hide Submenu
        ChangeOptionsUIState(false);
    }

    // Public Functions
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeOptionsUIState(bool state)
    {
        mainMenuPanel.SetActive(!state);
        optionMenuPanel.SetActive(state);
    }

    public void BGMVolume()
    {
        AudioManager.Instance.SetBMGVolume(bgmSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }

    // Private Functions
    private void InitializeMainMenuOptionsUI()
    {
        optionMenuPanel = GameObject.Find("Canvas/OptionPanel");
        bgmSlider = optionMenuPanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Slider>();
        sfxSlider = optionMenuPanel.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Slider>();

        bgmSlider.value = AudioManager.Instance.GetBMGVolume();
        sfxSlider.value = AudioManager.Instance.GetSFXVolume();
    }
}
