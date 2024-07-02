using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class States : PlayerBase
{
    [SerializeField] private LayerMask sand, grass;

    private float waitUntilLookIdle;

    private void Start()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            waitUntilLookIdle = Random.Range(3f, 5f);
        }
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            waitUntilLookIdle -= Time.deltaTime;

            if (waitUntilLookIdle <= 0f)
            {
                waitUntilLookIdle = Random.Range(3f, 5f);
            }
        }
        
        anim.SetFloat("lokkingTime", waitUntilLookIdle);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == sand)
        {
            Debug.Log("Bitch");
            MusicManager.Instance.PlayAmbience(MusicManager.Instance.beachAmbience, 1f);
        }

        if (other.gameObject.layer == grass)
        {
            MusicManager.Instance.PlayAmbience(MusicManager.Instance.forestAmbience, 1f);
        }
    }
}
