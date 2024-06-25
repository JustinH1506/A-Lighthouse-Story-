using UnityEngine;

public class FPSCap : MonoBehaviour
{
   [SerializeField] private int target = 60;

   /// <summary>
   /// Sets targetFrameRate. 
   /// </summary>
   private void Awake()
   {
      QualitySettings.vSyncCount = 0;

      Application.targetFrameRate = target;
   }

   /// <summary>
   /// Changes TargetFrameRate.
   /// </summary>
   private void Update()
   {
      if (Application.targetFrameRate > target)
         Application.targetFrameRate = target;
   }
}
