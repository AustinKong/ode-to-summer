using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{ 
    public static AudioManager instance; 
    public AudioSource[] Notes;
    public AudioSource click;
    public AudioSource complete;
    public AudioSource cicadas;

    private void Awake() 
    {
        instance = this;
    }
    

    public void PlayNote(int note)
    {
        Notes[note].Play();
    }

    public void PlayClick()
    {
        click.Play();
    }

    public void PlayComplete()
    {
        complete.Play();
        
    }

    public void Fade()
    {
        StartCoroutine(FadeOut(cicadas,1f));
        StartCoroutine(FadeOut(complete, 1f));
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
