using System.Collections;
using UnityEngine;

public static class FadeMusicExtension
{
   public static void FadeOut(this AudioSource aSource, float duration)
   {
      aSource.GetComponent<MonoBehaviour>().StartCoroutine(FadeOutAudio(aSource, duration));
   }

   public static void FadeIn(this AudioSource aSource, float duration)
   {
      aSource.GetComponent<MonoBehaviour>().StartCoroutine(FadeInAudio(aSource, duration));
   }

   private static IEnumerator FadeOutAudio(AudioSource aSource, float duration)
   {
      float startVolume = aSource.volume;

      while (aSource.volume > 0)
      {
         aSource.volume -= startVolume * Time.deltaTime / duration;
         yield return null;
      }
      
      aSource.Stop();
      aSource.volume = startVolume;
   }

   private static IEnumerator FadeInAudio(AudioSource aSource, float duration)
   {
      float startVolume = 0.2f;

      aSource.volume = 0;
      aSource.Play();

      while (aSource.volume < 1.0f)
      {
         aSource.volume += startVolume * Time.deltaTime / duration;

         yield return null;
      }

      aSource.volume = 1f;
   }
}
