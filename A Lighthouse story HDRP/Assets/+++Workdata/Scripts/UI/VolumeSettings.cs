using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    
    [SerializeField] private string volumeKey;

    private Slider slider;

    private void Start()
    {
        GameStateManager.instance.onStateChanged += OnStateChange;
        slider = GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat(volumeKey, 1f);
    }

    private void OnStateChange(GameStateManager.GameState newState)
    {
        if (newState == GameStateManager.GameState.InGame)
        {
            slider.value = PlayerPrefs.GetFloat(volumeKey, 1f);
        }
    }

    public void SetVolume(float volume)
    {
        volume = slider.value;
        mixer.SetFloat(volumeKey, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(volumeKey, volume);
    }
}
