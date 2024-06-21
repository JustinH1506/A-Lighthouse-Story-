using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    #region Scripts
    
    [SerializeField] private EnemyPatrol _enemyPatrol;
    
    #endregion
    
    #region Variables

    [Header("View Radius and Angle.")]
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    [Header("Layer Masks.")]
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    
    [Header("List of Visible Targets.")]
    public List<Transform> visibleTargets = new List<Transform>();

    #endregion

    #region Methods
    
    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            
            Debug.Log("Works here");
            
            FindInvisibleTargets();
        }
    }

    void FindInvisibleTargets()
    {
        visibleTargets.Clear();
        
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        
        for( int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;

            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);

                    _enemyPatrol.foundPlayer = true;
                }
            }
        }
    }
    
    public Vector3 DirFromAngle(float angleInDegrees,bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    
    #endregion
}