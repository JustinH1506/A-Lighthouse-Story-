using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    #region Variables

    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform player;

    [SerializeField] private Animator anim;

    private Rigidbody rb;

    private NavMeshAgent _navMeshAgent;

    #endregion

    #region Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        Chase();
    }

    private void Chase()
    {
        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);

        _navMeshAgent.SetDestination(targetPos);

        anim.SetTrigger("isWalking");
        
        anim.SetFloat("speed", -1f);

        //Vector3 direction = (targetPos - transform.position).normalized;

        //transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);

        //rb.MovePosition(transform.position + targetPos * Time.deltaTime * moveSpeed);
    }
    
    #endregion
}
