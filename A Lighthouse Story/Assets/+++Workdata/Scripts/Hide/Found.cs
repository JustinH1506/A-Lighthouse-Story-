using UnityEngine;

public class Found : MonoBehaviour
{
    #region Tansform
    
    [SerializeField] private Transform safePosition;
    
    #endregion

    #region Methods

    public void GameOver(Transform target)
    {
        target.position = safePosition.position;
    }

    #endregion
}
