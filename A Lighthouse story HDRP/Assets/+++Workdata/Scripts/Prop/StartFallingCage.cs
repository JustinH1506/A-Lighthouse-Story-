using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StartFallingCage : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private Transform hat;

    [SerializeField] private Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            StartCoroutine(WaitForCageFall());
        }
    }

    private IEnumerator WaitForCageFall()
    {
        while (Vector3.Distance(player.position, transform.position) > 0.02f)
        {
            player.GetComponent<PlayerMovement>().isDisabled = true;
            
            player.position = Vector3.MoveTowards(player.position, transform.position, 0.25f * Time.deltaTime);
            
            player.GetComponent<Animator>().SetFloat("velocity",0.06f);
            
            player.LookAt(hat);
            
            yield return null;
        }
        
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        player.GetComponent<Animator>().SetFloat("velocity", 0f);

        anim.SetTrigger("Start");
    }
}
