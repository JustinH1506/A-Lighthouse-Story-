using Cinemachine;
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
