using UnityEngine;

public class SmallCrabs : MonoBehaviour
{
   [SerializeField] private Death _death;

   /// <summary>
   /// Stops the player and start death animation. 
   /// </summary>
   /// <param name="other"></param>
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         _death.StartDeathAnimation();
          
          other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
          
          other.gameObject.GetComponent<PlayerMovement>().DisableMovement();
      }
   }
}
