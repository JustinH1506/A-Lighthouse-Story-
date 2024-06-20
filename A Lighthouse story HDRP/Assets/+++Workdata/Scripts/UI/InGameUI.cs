using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup inGameMenu;
    [SerializeField] private CanvasGroup inGameOptions;
    
    private PlayerInputManager playerInput;

    private PlayerControllerMap inputActions;

    public bool menuActive = true;

    private void Awake()
    {
        inputActions = new PlayerControllerMap();
        inGameMenu.HideCanvasGroup();
        inGameOptions.HideCanvasGroup();
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
            (inGameMenu.interactable == false))
        {
            OpenInGameUI();
        }
        else if (context.performed &&
                 (GameStateManager.instance.currentState != GameStateManager.GameState.InMainMenu) &&
                 (inGameMenu.interactable == true))
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

    public void CloseInGameUI()
    {
        inGameMenu.HideCanvasGroup();
        inGameOptions.HideCanvasGroup();
        Time.timeScale = 1f;

        if (GameStateManager.instance.currentState == GameStateManager.GameState.InGame)
            playerInput.enabled = true;
    }

    public void OpenOptionsMenu()
    {
        inGameMenu.HideCanvasGroup();
        inGameOptions.ShowCanvasGroup();
    }

    public void OpenMenu()
    {
        inGameMenu.ShowCanvasGroup();
        inGameOptions.HideCanvasGroup();
    }

    public void LoadMainMenu()
    {
        GameStateManager.instance.GoToMainMenu();
    }
}
