using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectAudio : MonoBehaviour
{
    public AudioClip audioClip;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    public void Play(float volume = 1f, float pitch = 1f) 
    {
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
    }
}
