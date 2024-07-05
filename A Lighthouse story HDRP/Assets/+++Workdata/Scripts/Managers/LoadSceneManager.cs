using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The SceneLoader unloads and loads scenes.
/// It has always one current scene that will be automatically unloaded when a new scene is to be loaded
/// The ManagerScene, however, will never be unloaded.
/// </summary>
public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup loadingScreen;
    
    public static LoadSceneManager instance;
    private string currentScene; //this saves whatever the current loaded main scene is.

    public bool sceneLoaded = true;
    
    public bool goToLevel = false;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// load the new scene and unload current scene
    /// </summary>
    /// <param name="newScene"></param>
    /// <param name="showLoadingScreen">with or without loadingscreen</param>
    public void SwitchScene(string newScene, bool showLoadingScreen = true)
    {
        //when a scene is to be switched, we start a coroutine, so that we can first unload the current scene,
        //then load the next scene, while being able to do it asynchronous (here we could, for example, show a loading
        //screen animation)
        StartCoroutine(LoadNewSceneCoroutine(newScene, showLoadingScreen));
    }

    private IEnumerator LoadNewSceneCoroutine(string newSceneName, bool showLoadingScreen)
    {
        //first, show the loading screen, so that the player does not have to see elements plopping in and out of the scene
        if (showLoadingScreen)
        {
            ShowLoadingScreen();
        }

        sceneLoaded = false;
        
        //if the current scene is actually loaded, we first unload it
        var scene = SceneManager.GetSceneByName(currentScene);
        if (scene.isLoaded)
        {
            //by yielding for loading or unloading a scene, we can wait until the loading process is actually finished
            yield return SceneManager.UnloadSceneAsync(currentScene);
        }
        
        //then, when the scene we want to load is not yet loaded, we load it
        Scene newScene = SceneManager.GetSceneByName(newSceneName);
        if (!newScene.isLoaded)
        {
            yield return SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);
        }
        
        //all instantiated objects get added to the active scene.
        //this way, all bullets and VFX elements we instantiate will be cleaned up
        //when we unload the scene again later on.
        yield return null;
        newScene = SceneManager.GetSceneByName(newSceneName);
        SceneManager.SetActiveScene(newScene);

        if (showLoadingScreen)
        {
            yield return new WaitForSeconds(2f);
        }
        sceneLoaded = true;
        showLoadingScreen = true;
        //lastly, we disable the loading screen and set the current scene accordingly
        HideLoadingScreen();
        currentScene = newSceneName;
    }
    
    
    public void ShowLoadingScreen()
    {
        loadingScreen.ShowCanvasGroup();
    }

    public void HideLoadingScreen()
    {
        loadingScreen.HideCanvasGroup();
    }
}
