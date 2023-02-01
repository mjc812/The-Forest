using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraShake;

public abstract class Firearm : Weapon
{
    public AudioClip fire;
    public BounceShake.Params shakeParams;

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
