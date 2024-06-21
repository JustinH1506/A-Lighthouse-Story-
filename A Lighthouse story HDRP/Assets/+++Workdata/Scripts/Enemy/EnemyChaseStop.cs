using UnityEngine;

public class EnemyChaseStop : MonoBehaviour
{
    [SerializeField] private EnemyChase _enemyChase;

    private void OnTriggerEnter(Collider other)
    {
        _enemyChase.enabled = false;
        
        gameObject.SetActive(false);
    }
}
