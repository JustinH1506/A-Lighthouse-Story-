using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Story : MonoBehaviour
{
    #region Variables
    
    [SerializeField] private GameObject skipText;

    [SerializeField] private TextMeshProUGUI storyText;
     
    private PlayerControllerMap inputActions;

    //in game menu script
    private GameObject inGameUI;

    //bool to check if player can skip the story
    private bool allowSkip;
    
    #endregion

    #region Unity Methods
    private void Awake()
    {
        inputActions = new PlayerControllerMap();
        if (GameStateManager.instance.currentState != GameStateManager.GameState.InMainMenu)
        {
            inGameUI = GameObject.FindGameObjectWithTag("InGameHUD").gameObject;
            inGameUI.SetActive(false);
        }

        skipText.SetActive(false);
        StartCoroutine(WaitForSkip());
    }
    
    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.UI.SkipCredits.performed += SkipCredits;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        inputActions.UI.SkipCredits.performed -= SkipCredits;
    }
    
    #endregion

    #region Story Methods
    //if the allowSkip is true, loads the level and set allowSkip back to false
    private void SkipCredits(InputAction.CallbackContext context)
    {
        if (context.performed && allowSkip)
        {
            allowSkip = false;
            StartLevel();
        }
    }

    public void StartLevel()
    {
        GameStateManager.instance.StartNewGame();
    }

    public void StoryText1()
    {
        storyText.text = "My little family lived in a peaceful house that we had built from a drift wood.";
    }

    public void StoryText2()
    {
        storyText.text = "Humans knew about us. They prey on us, burn our houses and put us in cages.";
    }

    public void StoryText3()
    {
        storyText.text =
            "I watched my daughter being taken away, she screamed for my help. She couldn't hide like I did.";
    }

    public void StoryText4()
    {
        storyText.text =
            "After they had taken everything I owned, I decided to follow them and save my daughter, whatever the cost.";
    }

    public void StoryText5()
    {
        storyText.text =
            "After a while, I saw a lighthouse. I know this is where they ship us off, after you leave this island you’re sure to never return.";
    }
    
    /// <summary>
    /// waits 2 seconds and turn allowSkip to true
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForSkip()
    {
        yield return new WaitForSeconds(2f);
        
        allowSkip = true;
        skipText.SetActive(true);
    }
    #endregion
}
