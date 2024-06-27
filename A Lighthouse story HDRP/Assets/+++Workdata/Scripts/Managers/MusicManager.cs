using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Audio Sources")] 
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private AudioSource ambienceAudio;
    [SerializeField] private AudioSource sFXAudio;

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
    public AudioClip grassStepA;
    public AudioClip grassStepB;
    public AudioClip grassStepC;
    public AudioClip grassStepD;
    public AudioClip grassStepE;
    public AudioClip sandStepA;
    public AudioClip sandStepB;
    public AudioClip sandStepC;
    public AudioClip sandStepD;
    public AudioClip sandStepE;
    public AudioClip woodStepA;
    public AudioClip woodStepB;
    public AudioClip woodStepC;
    public AudioClip woodStepD;
    public AudioClip woodStepE;
    
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
        musicAudio.FadeOut(fadeDuration);
        musicAudio.clip = clip;
        musicAudio.FadeIn(fadeDuration);
    }

    /// <summary>
    /// stops current ambience and plays the new clip
    /// </summary>
    /// <param name="clip">audio clip to play</param>
    public void PlayAmbience(AudioClip clip, float fadeDuration)
    {
        ambienceAudio.FadeOut(fadeDuration);
        ambienceAudio.clip = clip;
        ambienceAudio.FadeIn(fadeDuration);
    }

    /// <summary>
    /// plays sfx audio clip one time
    /// </summary>
    /// <param name="clip">audio clip to play</param>
    public void PlaySFX(AudioClip clip)
    {
        sFXAudio.PlayOneShot(clip);
    }
}
