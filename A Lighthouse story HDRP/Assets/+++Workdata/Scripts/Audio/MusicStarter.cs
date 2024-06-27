using System;
using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    private void Start()
    {
        MusicManager.Instance.PlayMusic(MusicManager.Instance.beachMusic, 1f);
    }
}
