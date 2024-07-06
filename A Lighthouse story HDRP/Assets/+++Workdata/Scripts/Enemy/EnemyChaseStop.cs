using UnityEngine;

public class EnemyChaseStop : MonoBehaviour
{
    [SerializeField] private EnemyChase _enemyChase;

    [SerializeField] private Animator anim;

    /// <summary>
    /// Activate enemyChase and set the gameObject false when walking into the collider. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        _enemyChase.enabled = false;

        anim.enabled = false;
        
        MusicManager.Instance.PlayMusic(MusicManager.Instance.beachMusic, 0.5f);
        
        MusicManager.Instance.PlayAmbience(MusicManager.Instance.lighthouseAmbience, 0.5f);
        
        gameObject.SetActive(false);
    }
}
