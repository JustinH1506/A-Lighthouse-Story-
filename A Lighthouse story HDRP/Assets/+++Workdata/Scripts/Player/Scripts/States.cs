using System;
using Unity.VisualScripting;
using UnityEngine;

public class States : MonoBehaviour
{
    [SerializeField] private LayerMask sand, grass;

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
