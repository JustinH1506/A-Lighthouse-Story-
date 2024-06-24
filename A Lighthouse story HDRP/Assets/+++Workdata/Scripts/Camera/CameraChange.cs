using Cinemachine;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook playerCam;
    [SerializeField] private CinemachineFreeLook moonCam;

    /// <summary>
    /// We enable the moonCam when walking into the collider.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        moonCam.enabled = true;
    }

    /// <summary>
    /// We disable the moonCam when exiting the collider.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        moonCam.enabled = false;
    }
}
