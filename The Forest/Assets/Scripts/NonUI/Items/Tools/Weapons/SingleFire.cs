using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFire : Firearm
{
    public override int ID
    {
        get => 5;
    }

    public override string Description
    {
        get => "Enfield";
    }

    public override Sprite Sprite
    {
        get => null;
    }
    
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
