using UnityEngine;
using System.Collections;

public class AudioFadeOut : MonoBehaviour
{
    public static AudioFadeOut instance;

    public void Start()
    {
       instance = this;
    }

    public static IEnumerator StartFadeIenumIn(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);

            yield return null;
        }
        yield break;
    }

    public void StartFadeIn(AudioSource audioSource, float duration, float targetVolume)
    {
        StartCoroutine(StartFadeIenumIn(audioSource, duration, targetVolume));
    }


    public static IEnumerator StartFadeIenumOut(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = 0.2f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);

            yield return null;
        }
        yield break;
    }

    public void StartFadeOut(AudioSource audioSource, float duration, float targetVolume)
    {
        StartCoroutine(StartFadeIenumOut(audioSource, duration, targetVolume));
        //audioSource.Stop();

    }


}