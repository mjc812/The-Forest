using UnityEngine;
using CameraShake;

public class PlayerHealth : Health
{
    private AudioSource audioSource; 

    public float a = 0.7f;
    public float b = 0.3f;

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
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.pitch = a;
        audioSource.volume = b;
        audioSource.Play();
        Vector3 sourcePosition = transform.position;
        if (isLeft) {
            CameraShaker.Shake(new KickShake(leftShakeParams, new CameraShake.Displacement(sourcePosition)));
        } else {
            CameraShaker.Shake(new KickShake(rightShakeParams, new CameraShake.Displacement(sourcePosition)));
        }
    }
}
