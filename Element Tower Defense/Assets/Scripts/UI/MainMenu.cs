using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject mainMenuPanel;
    private GameObject optionMenuPanel;

    private Slider bgmSlider;
    private Slider sfxSlider;

    private void Start()
    {
        mainMenuPanel = GameObject.Find("Canvas/MainPanel");
        optionMenuPanel = GameObject.Find("Canvas/OptionPanel");
        bgmSlider = optionMenuPanel.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Slider>();
        sfxSlider = optionMenuPanel.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Slider>();

        bgmSlider.value = AudioManager.Instance.GetBMGVolume();
        sfxSlider.value = AudioManager.Instance.GetSFXVolume();

        print($"{bgmSlider} || {sfxSlider}");
        ChangeOptionsUIState(false);
    }

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
        print($"BGM {bgmSlider.value}");
        AudioManager.Instance.SetBMGVolume(bgmSlider.value);
    }

    public void SFXVolume()
    {
        print($"SFX {sfxSlider.value}");
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }
}
