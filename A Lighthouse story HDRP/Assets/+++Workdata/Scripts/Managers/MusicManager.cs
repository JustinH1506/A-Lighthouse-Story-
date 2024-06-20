using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Audio Sources")] 
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private AudioSource sFXAudio;

    [Header("Audio Clips Music")] 
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip beachMusic;
    [SerializeField] private AudioClip crabChaseMusic;
    [SerializeField] private AudioClip crabSearchMusic;
    [SerializeField] private AudioClip creditMusic;

    [Header("Audio Clips InGame SFX")] 
    [SerializeField] private AudioClip windAmbience;
    [SerializeField] private AudioClip seagullScreams;
    [SerializeField] private AudioClip sandFootsteps;
    [SerializeField] private AudioClip grassFootsteps;
    [SerializeField] private AudioClip obstacleMoving;
    [SerializeField] private AudioClip crabWalk;

    [Header("Audio Clip UI SFX")] 
    [SerializeField] private AudioClip turnPages;
    [SerializeField] private AudioClip buttonHover;
    [SerializeField] private AudioClip buttonPress;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicAudio.clip = mainMenuMusic;
        musicAudio.Play();
    }
}
