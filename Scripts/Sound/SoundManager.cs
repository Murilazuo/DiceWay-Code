using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    internal void PlayOneShotAudio(string audio)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audio);
        audioSource.volume = s.volume;

        audioSource.PlayOneShot(s.clip);
    }
    public void StopAudio(string audio)
    {
        audioSource.Stop();
    }
     public void PlayAudio(string audio)
    {
        Sound s = Array.Find(sounds, sound => sound.name == audio);

        audioSource.clip = s.clip;
        audioSource.volume = s.volume;

        audioSource.Play();
    }

   
}