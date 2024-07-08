using UnityEngine;

public class FallingCage : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    /// <summary>
    /// Starts credits scene after animation. 
    /// </summary>
    public void StartCredits()
    {
        LoadSceneManager.instance.SwitchScene("Credits", false);
    }

    /// <summary>
    /// Plays cage fall sound. 
    /// </summary>
    public void CageSound()
    {
        MusicManager.Instance.PlayInGameSFX(MusicManager.Instance.cageFallSound);
    }
}
