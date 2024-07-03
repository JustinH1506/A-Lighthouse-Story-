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
        
        /*sandStepList.Add(MusicManager.Instance.sandStepA);
        sandStepList.Add(MusicManager.Instance.sandStepB);
        sandStepList.Add(MusicManager.Instance.sandStepC);
        sandStepList.Add(MusicManager.Instance.sandStepD);
        sandStepList.Add(MusicManager.Instance.sandStepE);
        
        sandStepList.Add(MusicManager.Instance.grassStepA);
        sandStepList.Add(MusicManager.Instance.grassStepB);
        sandStepList.Add(MusicManager.Instance.grassStepC);
        sandStepList.Add(MusicManager.Instance.grassStepD);
        sandStepList.Add(MusicManager.Instance.grassStepE);
        
        sandStepList.Add(MusicManager.Instance.woodStepA);
        sandStepList.Add(MusicManager.Instance.woodStepB);
        sandStepList.Add(MusicManager.Instance.woodStepC);
        sandStepList.Add(MusicManager.Instance.woodStepD);
        sandStepList.Add(MusicManager.Instance.woodStepE);*/
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
            MusicManager.Instance.PlaySFX(sandStepList[Random.Range(0, 5)]);
        }
        
        if (isSand)
        {
            MusicManager.Instance.PlaySFX(grassStepList[Random.Range(0, 5)]);
        }
        
        if (isSand)
        {
            MusicManager.Instance.PlaySFX(woodStepList[Random.Range(0, 5)]);
        }
    }
}
