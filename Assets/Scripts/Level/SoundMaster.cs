using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaster : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSource => GetComponent<AudioSource>();

   public void PlaySound(AudioClip clip, float volume = 1f, bool loop = false, float p1 = 0.85f, float p2 = 1.2f)
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.pitch = Random.Range(p1, p2);
        audioSource.volume = volume;

        audioSource.Play();
    }

    public void StopSound(){
        audioSource.Stop();
    }
}
