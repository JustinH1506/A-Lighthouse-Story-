using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void StartDeathAnimation()
    {
        anim.enabled = true;
    }

    public void ReloadScene()
    {
        LoadSceneManager.instance.SwitchScene("Vertical Slice");
    }
}
