using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    #region Components
    
    protected Rigidbody rb;

    protected Animator anim;
    
    #endregion

    #region Methods
    private void Awake()
    {
        //We get the Rigidbody.
        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();
    }
    
    #endregion
}
