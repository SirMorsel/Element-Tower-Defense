using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    private float bgmValue = 0.5f;
    private float sfxVolume = 0.5f;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public float GetBMGVolume()
    {
        return bgmValue;
    }

    public void SetBMGVolume(float volume)
    {
        bgmValue = volume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }
}
