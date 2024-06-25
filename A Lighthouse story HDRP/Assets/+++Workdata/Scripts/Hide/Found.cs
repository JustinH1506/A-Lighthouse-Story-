using UnityEngine;

public class Found : MonoBehaviour
{
    #region Tansform
    
    [SerializeField] private Transform safePosition;
    
    #endregion

    #region Methods

    /// <summary>
    /// Sets target to safePosition. 
    /// </summary>
    /// <param name="target"></param>
    public void GameOver(Transform target)
    {
        target.position = safePosition.position;
    }

    #endregion
}
