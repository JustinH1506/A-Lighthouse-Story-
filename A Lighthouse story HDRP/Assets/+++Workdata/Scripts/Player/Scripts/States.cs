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

    private void Start()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            waitUntilLookIdle = Random.Range(3f, 5f);
        }
    }

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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == sand)
        {
            isSand = true;

            isGrass = false;

            isWood = false;
            
            MusicManager.Instance.PlayAmbience(MusicManager.Instance.beachAmbience, 1f);
        }

        if (other.gameObject.layer == grass)
        {
            isGrass = true;
            
            isSand = false;

            isWood = false;
            
            MusicManager.Instance.PlayAmbience(MusicManager.Instance.forestAmbience, 1f);
        }
        
        if (other.gameObject.layer == wood)
        {
            isWood = true;
            
            isSand = false;

            isGrass = false;
        }
    }

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
