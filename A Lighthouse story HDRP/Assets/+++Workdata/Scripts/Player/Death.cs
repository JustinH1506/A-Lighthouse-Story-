using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private Animator anim;

    /// <summary>
    /// Gets animator.
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Enables the animator. 
    /// </summary>
    public void StartDeathAnimation()
    {
        anim.enabled = true;
    }

    /// <summary>
    /// Reloads the scene. 
    /// </summary>
    public void ReloadScene()
    {
        LoadSceneManager.instance.SwitchScene("Vertical Slice");
    }
}
