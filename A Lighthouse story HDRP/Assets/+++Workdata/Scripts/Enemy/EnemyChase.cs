using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    #region Variables

    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform player;

    #endregion

    #region Methods
    
    private void FixedUpdate()
    {
        Chase();
    }

    private void Chase()
    {
        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
    }
    
    #endregion
}
