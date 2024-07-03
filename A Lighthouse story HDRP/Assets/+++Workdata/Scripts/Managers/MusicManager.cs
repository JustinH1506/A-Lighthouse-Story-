using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Audio Sources")] 
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private AudioSource ambienceAudio;
    [SerializeField] private AudioSource uiSFXAudio;
    [SerializeField] private AudioSource inGameSFXAudio;

    [Header("Music")] 
    public AudioClip mainMenuMusic;
    public AudioClip beachMusic;
    public AudioClip crabChaseMusic;
    public AudioClip crabSearchMusic;
    public AudioClip creditMusic;

    [Header("Ambience")] 
    public AudioClip beachAmbience;
    public AudioClip forestAmbience;

    [Header("Player Footsteps")] 
    public AudioClip[] grassSteps = new AudioClip[5];
    public AudioClip[] sandSteps = new AudioClip[5];
    public AudioClip[] woodSteps = new AudioClip[5];
    
    
    [Header("InGame SFX")]
    public AudioClip seagullScreams;
    public AudioClip obstacleMoving;

    [Header("UI SFX")] 
    public AudioClip turnPages;
    public AudioClip buttonHover;
    public AudioClip buttonPress;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicAudio.clip = mainMenuMusic;
        musicAudio.Play();
    }

    /// <summary>
    /// stops current music and plays the new clip
    /// </summary>
    /// <param name="clip">audio clip to play</param>
    public void PlayMusic(AudioClip clip, float fadeDuration)
    {
        musicAudio.FadingInOut(clip, fadeDuration);
        
    }

    /// <summary>
    /// stops current ambience and plays the new clip
    /// </summary>
    /// <param name="clip">audio clip to play</param>
    public void PlayAmbience(AudioClip clip, float fadeDuration)
    {
        ambienceAudio.FadingInOut(clip, fadeDuration);
    }

    /// <summary>
    /// plays ui sfx audio clip one time
    /// </summary>
    /// <param name="clip">audio clip to play</param>
    public void PlayUISFX(AudioClip clip)
    {
        uiSFXAudio.PlayOneShot(clip);
    }

    /// <summary>
    /// plays in game sfx audio clip one time
    /// </summary>
    /// <param name="clip">audio clip to play</param>
    public void PlayInGameSFX(AudioClip clip)
    {
        inGameSFXAudio.PlayOneShot(clip);
    }
}
