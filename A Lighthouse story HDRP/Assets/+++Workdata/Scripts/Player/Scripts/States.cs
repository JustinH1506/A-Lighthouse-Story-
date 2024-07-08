using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class States : PlayerBase
{
    [SerializeField] private LayerMask sand, grass, wood;

    private bool isSand;
    
    private bool isWood;
    
    private bool isGrass;

    private float waitUntilLookIdle;
    
    List<AudioClip> sandStepList, grassStepList, woodStepList;

    /// <summary>
    /// Set amount of time to wait to start other idle. 
    /// </summary>
    private void Start()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            waitUntilLookIdle = Random.Range(3f, 5f);
        }
    }

    /// <summary>
    /// Waits to start other idle and sets it. 
    /// </summary>
    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            waitUntilLookIdle -= Time.deltaTime;

            if (waitUntilLookIdle <= 0f)
            {
                waitUntilLookIdle = Random.Range(3f, 5f);
            }
        }
        else
        {
            waitUntilLookIdle = Random.Range(3f, 5f);
        }
        
        anim.SetFloat("lokkingTime", waitUntilLookIdle);
    }

    /// <summary>
    /// Checks which ground the player is on. 
    /// </summary>
    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, Mathf.Infinity, sand))
        {
            isSand = true;

            isGrass = false;

            isWood = false;
            
            //MusicManager.Instance.PlayAmbience(MusicManager.Instance.beachAmbience, 1f);
        }
        
        if (Physics.Raycast(transform.position, Vector3.down, Mathf.Infinity, grass))
        {
            isGrass = true;
            
            isSand = false;

            isWood = false;
            
            //MusicManager.Instance.PlayAmbience(MusicManager.Instance.forestAmbience, 1f);
        }
        
        if (Physics.Raycast(transform.position, Vector3.down, Mathf.Infinity, wood))
        {
            isWood = true;
            
            isSand = false;

            isGrass = false;
        }
    }
    
    /// <summary>
    /// Handles the step audio depending on which ground we are. 
    /// </summary>
    public void StepAudio()
    {
        if (isSand)
        {
            MusicManager.Instance.PlayUISFX(MusicManager.Instance.sandSteps[Random.Range(0, 5)]);
        }
        
        if (isGrass)
        {
            MusicManager.Instance.PlayUISFX(MusicManager.Instance.grassSteps[Random.Range(0, 5)]);
        }
        
        if (isWood)
        {
            MusicManager.Instance.PlayUISFX(MusicManager.Instance.woodSteps[Random.Range(0, 5)]);
        }
    }
}
