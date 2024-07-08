using UnityEngine;

public class FallingCage : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    public void StartCredits()
    {
        LoadSceneManager.instance.SwitchScene("Credits", false);
    }

    public void CageSound()
    {
        MusicManager.Instance.PlayInGameSFX(MusicManager.Instance.cageFallSound);
    }
}
