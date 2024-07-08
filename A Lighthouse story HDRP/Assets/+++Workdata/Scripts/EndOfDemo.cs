using UnityEngine;

public class EndOfDemo : MonoBehaviour
{
    /// <summary>
    /// Deactivates the searching eye and ends the demo. 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InGameUI.Instance.DeactivateSearchingEye();
            
            InGameUI.Instance.ShowEndofDemo();
        }
    }
}
