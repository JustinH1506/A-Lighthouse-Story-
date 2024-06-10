using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    [SerializeField] private EnemyChase enemyChase;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyChase.enabled = true;
        }
    }
}
