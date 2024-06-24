using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider ambienceSlider;
    [SerializeField] private Slider sFXSlider;

    public const string MASTER_KEY = "masterVolume";

    public const string MUSIC_KEY = "musicVolume";

    public const string AMBIENCE_KEY = "ambienceVolume";

    public const string SFX_KEY = "sfxVolume";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(MUSIC_KEY))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicSlider();
            SetAmbienceSlider();
            SetSFXSlider();
        }
    }

    private void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat(MASTER_KEY);
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_KEY);
        ambienceSlider.value = PlayerPrefs.GetFloat(AMBIENCE_KEY);
        sFXSlider.value = PlayerPrefs.GetFloat(SFX_KEY);
        
        SetMasterVolume();
        SetMusicSlider();
        SetAmbienceSlider();
        SetSFXSlider();
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        mixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MASTER_KEY, volume);
    }

    public void SetMusicSlider()
    {
        float volume = musicSlider.value;
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
    }

    public void SetAmbienceSlider()
    {
        float volume = ambienceSlider.value;
        mixer.SetFloat("AmbienceVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(AMBIENCE_KEY, volume);
    }

    public void SetSFXSlider()
    {
        float volume = sFXSlider.value;
        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(SFX_KEY, volume);
    }
}
