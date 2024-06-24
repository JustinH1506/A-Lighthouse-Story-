using UnityEngine;

public class EnemyChaseStop : MonoBehaviour
{
    [SerializeField] private EnemyChase _enemyChase;

    /// <summary>
    /// Activate enemyChase and set the gameObject false when walking into the collider. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        _enemyChase.enabled = false;
        
        gameObject.SetActive(false);
    }
}
