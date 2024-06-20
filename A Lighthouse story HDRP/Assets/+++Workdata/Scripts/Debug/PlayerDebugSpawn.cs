using System;
using UnityEngine;

public class PlayerDebugSpawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    [SerializeField] private Transform debugPoint1;
    [SerializeField] private Transform debugPoint2;
    [SerializeField] private Transform debugPoint3;

    private void Update()
    {
        if (Input.GetKey("1"))
        {
            player.position = debugPoint1.position;
        }
        if (Input.GetKey("2"))
        {
            player.position = debugPoint2.position;
        }
        if (Input.GetKey("3"))
        {
            player.position = debugPoint3.position;
        }
    }
}
