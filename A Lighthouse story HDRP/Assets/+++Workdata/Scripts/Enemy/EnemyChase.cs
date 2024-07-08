using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    #region Variables

    [SerializeField] private Transform player;

    private Animator anim;

    private NavMeshAgent _navMeshAgent;

    [SerializeField] private Death _death;

    #endregion

    #region Methods

    /// <summary>
    /// Get the Rigidbody, Animator and NavMeshAgent. 
    /// </summary>
    private void Awake()
    {
        anim = GetComponent<Animator>();

        _navMeshAgent = GetComponent<NavMeshAgent>();

        anim.enabled = true;
    }

    /// <summary>
    /// Call Chase method. 
    /// </summary>
    private void FixedUpdate()
    {
        Chase();
    }

    /// <summary>
    /// Start the Chase to the player and enable the Animations. 
    /// </summary>
    private void Chase()
    {
        Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);

        _navMeshAgent.SetDestination(targetPos);
        
        anim.SetFloat("speed", -1f);
        
        anim.SetTrigger("walk");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _death.StartDeathAnimation();
        }
    }
    
    public void CrabSteps()
    {
        MusicManager.Instance.PlayInGameSFX(MusicManager.Instance.crabSteps[Random.Range(0,9)]);
    }

    #endregion
}
