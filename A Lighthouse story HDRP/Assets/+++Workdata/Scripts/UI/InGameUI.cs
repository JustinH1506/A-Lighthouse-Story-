using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameUI : MonoBehaviour
{
    public static InGameUI Instance { get; private set; }
    
    //all panels in under the pause menu
    [SerializeField] private CanvasGroup inGameMenu;
    [SerializeField] private CanvasGroup inGameOptions;
    [SerializeField] private CanvasGroup demoEndScreen;

    //searching eye animator
    [SerializeField] private Animator eyeAnim;

    private PlayerInputManager playerInput;

    private PlayerControllerMap inputActions;

    //bool to check if player may open menu
    public bool menuActive = true;

    private void Awake()
    {
        Instance = this;
        inputActions = new PlayerControllerMap();
        inGameMenu.HideCanvasGroup();
        inGameOptions.HideCanvasGroup();
        demoEndScreen.HideCanvasGroup();
    }

    private void Start()
    {
        GameStateManager.instance.onStateChanged += OnStateChange;
        if(GameStateManager.instance.currentState == GameStateManager.GameState.InMainMenu)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.UI.PauseGame.performed += PauseGame;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        inputActions.UI.PauseGame.performed -= PauseGame;
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        if (context.performed && menuActive &&
            (GameStateManager.instance.currentState != GameStateManager.GameState.InMainMenu) &&
            (inGameMenu.interactable == false) && inGameOptions.interactable == false)
        {
            OpenInGameUI();
        }
        else if (context.performed &&
                 (GameStateManager.instance.currentState != GameStateManager.GameState.InMainMenu) &&
                 (inGameMenu.interactable == true || inGameOptions.interactable == true))
        {
            CloseInGameUI();
        }
    }

    private void OnStateChange(GameStateManager.GameState newState)
    {
        //we toggle the availability of the inGame menu whenever the game state chaneges
        bool isInGame = newState != GameStateManager.GameState.InMainMenu;
        gameObject.SetActive(isInGame);
    }

    /// <summary>
    /// opens in game menu
    /// </summary>
    public void OpenInGameUI()
    {
        if (GameStateManager.instance.currentState == GameStateManager.GameState.InGame)
        {
            playerInput = FindObjectOfType<PlayerInputManager>().gameObject.GetComponent<PlayerInputManager>();
            playerInput.enabled = false;
        }
        
        inGameMenu.ShowCanvasGroup();
        inGameOptions.HideCanvasGroup();
        Time.timeScale = 0f;
    }

    /// <summary>
    /// close in game menu
    /// </summary>
    public void CloseInGameUI()
    {
        inGameMenu.HideCanvasGroup();
        inGameOptions.HideCanvasGroup();
        Time.timeScale = 1f;

        if (GameStateManager.instance.currentState == GameStateManager.GameState.InGame)
            playerInput.enabled = true;
    }

    /// <summary>
    /// opens the in game option menu
    /// </summary>
    public void OpenOptionsMenu()
    {
        inGameMenu.HideCanvasGroup();
        inGameOptions.ShowCanvasGroup();
    }

    /// <summary>
    /// opens the main pause menu
    /// </summary>
    public void OpenMenu()
    {
        inGameMenu.ShowCanvasGroup();
        inGameOptions.HideCanvasGroup();
    }

    /// <summary>
    /// shows the demo end screen
    /// </summary>
    public void ShowEndofDemo()
    {
        demoEndScreen.ShowCanvasGroup();
    }

    /// <summary>
    /// loads main menu
    /// </summary>
    public void LoadMainMenu()
    {
        GameStateManager.instance.GoToMainMenu();
        CloseInGameUI();

        Time.timeScale = 1f;
    }

    /// <summary>
    /// quits the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// activates the searching eye UI
    /// </summary>
    public void ActivateSearchingEye()
    {
        eyeAnim.SetTrigger("FadeIn");
    }

    /// <summary>
    /// sets the searching eye animation to search 
    /// </summary>
    public void EyeSearchingState()
    {
        eyeAnim.SetBool("detectedPlayer", false);
    }

    /// <summary>
    /// sets the searching eye animation to detected
    /// </summary>
    public void EyeDetectedState()
    {
        eyeAnim.SetBool("detectedPlayer", true);
    }

    public void DeactivateSearchingEye()
    {
        eyeAnim.SetTrigger("FadeOut");
        eyeAnim.SetBool("detectedPlayer", false);
    }
}
