using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCrabs : MonoBehaviour
{
   [SerializeField] private Death _death;

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
