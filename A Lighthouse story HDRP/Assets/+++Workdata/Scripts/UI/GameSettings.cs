using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    #region Variables
    
    private const string FULLSCREEN_KEY = "FullScreen";
    private const string QUALITY_KEY = "QualitySetting";

    [SerializeField] private Toggle fullScreenToggle;

    [SerializeField] private TMP_Dropdown qualityDropdown;
    
    #endregion

    #region Unity Methods
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

    #endregion

    #region Setting Methods
    
    /// <summary>
    /// if playerpref keys are available set settings or set default values
    /// </summary>
    private void LoadSettings()
    {
        fullScreenToggle.isOn = PlayerPrefs.GetInt(FULLSCREEN_KEY, Screen.fullScreen ? 1 : 0) > 0;
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(QUALITY_KEY, 1), false);
        qualityDropdown.value = PlayerPrefs.GetInt(QUALITY_KEY, 1);
    }

    /// <summary>
    /// sets fullscreen and saves current settings
    /// </summary>
    /// <param name="isFullscreen"></param>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt(FULLSCREEN_KEY, isFullscreen ? 1 : 0);
    }

    /// <summary>
    /// sets quality level and saves current settings
    /// </summary>
    /// <param name="index"></param>
    public void SetQualityLevelDropdown(int index)
    {
        QualitySettings.SetQualityLevel(index, false);
        PlayerPrefs.SetInt(QUALITY_KEY, index);
    }
    
    #endregion
}
