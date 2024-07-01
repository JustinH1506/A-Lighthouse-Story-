using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    private const string FULLSCREEN_KEY = "FullScreen";
    private const string QUALITY_KEY = "QualitySetting";

    [SerializeField] private Toggle fullScreenToggle;

    [SerializeField] private TMP_Dropdown qualityDropdown;

    private void Start()
    {
        GameStateManager.instance.onStateChanged += OnStateChange;
        LoadSettings();
    }
    
    private void OnStateChange(GameStateManager.GameState newState)
    {
        if (newState == GameStateManager.GameState.InGame)
        {
            LoadSettings();
        }
    }

    private void LoadSettings()
    {
        fullScreenToggle.isOn = PlayerPrefs.GetInt(FULLSCREEN_KEY, Screen.fullScreen ? 1 : 0) > 0;
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(QUALITY_KEY, 0), false);
        qualityDropdown.value = PlayerPrefs.GetInt(QUALITY_KEY, 0);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(FULLSCREEN_KEY, isFullscreen ? 1 : 0);
    }

    public void SetQualityLevelDropdown(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
        PlayerPrefs.SetInt(QUALITY_KEY, index);
    }
}
