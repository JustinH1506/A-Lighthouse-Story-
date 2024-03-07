using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    #region Variables

    protected Rigidbody rb;
    
    #endregion

    #region Methods
    private void Awake()
    {
        //We get the Rigidbody.
        rb = GetComponent<Rigidbody>();
    }
    
    #endregion
}
