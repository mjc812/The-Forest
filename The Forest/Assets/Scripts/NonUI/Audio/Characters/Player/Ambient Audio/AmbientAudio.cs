using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudio : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip ambience;

    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
}
