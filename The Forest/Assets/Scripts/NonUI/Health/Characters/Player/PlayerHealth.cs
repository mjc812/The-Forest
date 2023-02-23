using UnityEngine;
using CameraShake;

public class PlayerHealth : Health
{
    private AudioSource audioSource; 

    public AudioClip[] audioClips;
    public KickShake.Params leftShakeParams;
    public KickShake.Params rightShakeParams;

    protected override int startingHealth
    {
        get => 100;
    }

    protected override bool regenHealth
    {
        get => true;
    }

    protected override void Start() {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }
    
    protected override void DamageEffects(float amount, bool isCentral, bool isLeft, bool isRight) {
        Vector3 sourcePosition = transform.position;
        if (isLeft) {
            CameraShaker.Shake(new KickShake(leftShakeParams, new CameraShake.Displacement(sourcePosition)));
        } else {
            CameraShaker.Shake(new KickShake(rightShakeParams, new CameraShake.Displacement(sourcePosition)));
        }
    }
}
