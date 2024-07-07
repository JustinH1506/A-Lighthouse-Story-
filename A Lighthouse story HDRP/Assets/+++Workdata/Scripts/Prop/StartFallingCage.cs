using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StartFallingCage : MonoBehaviour
{
    [SerializeField] private Transform player;

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
        while (player.position != transform.position)
        {
            player.GetComponent<PlayerMovement>().isDisabled = true;
            
            player.position = Vector3.MoveTowards(player.position, transform.position, 0.25f * Time.deltaTime);
            
            player.GetComponent<Animator>().SetFloat("velocity",player.GetComponent<Rigidbody>().velocity.magnitude);
            
            yield return null;
        }
        
        yield return new WaitUntil(() =>
        {
            player.position = transform.position;
            
            return true;
        });
        
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        player.GetComponent<Animator>().SetFloat("velocity",player.GetComponent<Rigidbody>().velocity.magnitude);
        
        anim.SetTrigger("Start");
    }
}
