using System;
using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    /// <summary>
    /// Starts beach Music.
    /// </summary>
    private void Start()
    {
        MusicManager.Instance.PlayMusic(MusicManager.Instance.beachMusic, 0.1f);
    }
}
