using Cinemachine;
using UnityEngine;

public class CameraStairChanger : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook stairCam;

    /// <summary>
    /// Enables stairCam. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            stairCam.enabled = true;
        }
    }

    /// <summary>
    /// Disables Stair cam.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            stairCam.enabled = false;
        }
    }
}
