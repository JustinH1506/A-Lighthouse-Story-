using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    #region Things
    [SerializeField] private LayerMask terrainLayer = default;
    [SerializeField] private Transform body = default;
    [SerializeField] private IKFootSolver otherFoot = default;
    #endregion

    #region MyRegion

    [Header("Movement")]
    [SerializeField] private float speed = 1;
    [SerializeField] private float stepDistance = 4;
    [SerializeField] private float stepLength = 4;
    [SerializeField] private float stepHeight = 1;
    
    private float footSpacing;
    private float lerp;

    #endregion
    
    #region Vector3
    
    [SerializeField] private Vector3 footOffset = default;
    private Vector3 oldPosition, currentPosition, newPosition;
    private Vector3 oldNormal, currentNormal, newNormal;
    
    #endregion

    #region Methods
    private void Start()
    {
        footSpacing = transform.localPosition.x;
        currentPosition = newPosition = oldPosition = transform.position;

        currentNormal = newNormal = oldNormal = transform.up;
        lerp = 1;
    }

    void Update()
    {
        transform.position = currentPosition;
        transform.up = currentNormal;

        Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit info, 10, terrainLayer.value))
        {
            Debug.Log("Works");
            
            Debug.Log(lerp <= 1);
            if(Vector3.Distance(newPosition, info.point) > stepDistance && !otherFoot.IsMoving() && lerp >= 1)
            {
                Debug.Log("Works as well");
                lerp = 0;
                int direction = body.InverseTransformPoint(info.point).z > body.InverseTransformPoint(newPosition).z ? 1 : -1;
                newPosition = info.point + (body.forward * stepLength * direction) + footOffset;
                newNormal = info.normal;
            }
        }
       

        if (lerp < 1)
        {
            Debug.Log("Look here");
            Vector3 tempPosition = Vector3.Lerp(oldPosition, newPosition, lerp);
            tempPosition.y = Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPosition = tempPosition;
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * speed;
        }
        else
        {
            Debug.Log("or here");
            oldPosition = newPosition;
            oldNormal = newNormal;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(newPosition, 0.01f);
    }

    public bool IsMoving()
    {
        return lerp < 1;
    }
    
    #endregion
}
