using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    #region Variables

    public Rigidbody rb;
    
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
