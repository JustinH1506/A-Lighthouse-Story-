using System.Collections;
using UnityEngine;

public static class FadeMusicExtension
{
   public static void FadingInOut(this AudioSource aSource, AudioClip clip, float duration)
   {
      aSource.GetComponentInParent<MonoBehaviour>().StartCoroutine(FadeOutAudio(aSource, clip, duration));
   }

   /*
   public static void FadeIn(this AudioSource aSource, float duration)
   {
      aSource.GetComponentInParent<MonoBehaviour>().StartCoroutine(FadeInAudio(aSource, duration));
   }*/

   private static IEnumerator FadeOutAudio(AudioSource aSource, AudioClip clip, float duration)
   {

      while (aSource.volume > 0)
      {
         aSource.volume -= 0.2f * Time.deltaTime / duration;
         yield return null;
      }
      
      aSource.Stop();
      aSource.clip = clip;
      aSource.volume = 1f;
      aSource.GetComponentInParent<MonoBehaviour>().StartCoroutine(FadeInAudio(aSource, duration));
   }

   private static IEnumerator FadeInAudio(AudioSource aSource, float duration)
   {
      aSource.volume = 0;
      aSource.Play();

      while (aSource.volume < 1.0f)
      {
         aSource.volume += 0.2f * Time.deltaTime / duration;

         yield return null;
      }

      aSource.volume = 1f;
   }
}
