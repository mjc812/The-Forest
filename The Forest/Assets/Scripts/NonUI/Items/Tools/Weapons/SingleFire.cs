using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingleFire : Firearm
{
    void Start()
    {
        fireRate = 3f;
        nextTimeToFire = 0;
        boxCollider = transform.GetComponent<BoxCollider>();
        audioSource = transform.GetComponent<AudioSource>();
        particleEffects = transform.Find("Particle Effects");
        muzzleFlash = particleEffects.Find("Muzzle Flash").GetComponent<ParticleSystem>();
    }
}
