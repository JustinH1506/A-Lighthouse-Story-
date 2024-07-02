using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraStairChanger : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook stairCam;

    private void OnTriggerEnter(Collider other)
    {
        stairCam.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        stairCam.enabled = false;
    }
}
