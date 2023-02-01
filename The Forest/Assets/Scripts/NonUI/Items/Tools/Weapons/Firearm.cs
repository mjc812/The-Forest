using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraShake;

public class Firearm : Weapon
{
    public AudioClip fire;
    public BounceShake.Params shakeParams;

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

    public override bool Use()
    {
        if ((IsReadyForUse()) && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 80f))
            {
                RaycastTargetHit(hit);
            }
            //muzzleFlash.Play();
            audioSource.PlayOneShot(fire, 1f);
            nextTimeToFire = Time.time + 1f / fireRate;
            
            Vector3 sourcePosition = transform.position;
            CameraShaker.Shake(new BounceShake(shakeParams, sourcePosition));

            return true;
        } else
        {
            return false;
        }
    }
}
