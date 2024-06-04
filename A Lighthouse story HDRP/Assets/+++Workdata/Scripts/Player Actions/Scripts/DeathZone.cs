using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeathZone : MonoBehaviour
{
    #region Components
    
    private Animator anim;
    
    #endregion
    
    #region Classes
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Death");
        }
    }
    #endregion
}
