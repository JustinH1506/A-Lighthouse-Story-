using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor.Searcher;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook playerCam;
    [SerializeField] private CinemachineFreeLook moonCam;

    private void OnTriggerEnter(Collider other)
    {
        moonCam.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        moonCam.enabled = false;
    }
}
