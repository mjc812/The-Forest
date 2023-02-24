using UnityEngine;
using CameraShake;

public class PlayerHealth : Health
{
    private AudioSource audioSource; 
    private AudioSource heartbeatAudioSource; 
    private AudioSource damageAudioSource; 

    public float a = 0.7f;
    public float b = 0.3f;

    public AudioClip[] audioClips;
    public AudioClip heartbeat;
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
        heartbeatAudioSource = transform.Find("Heartbeat Audio").GetComponent<AudioSource>();
        damageAudioSource = transform.Find("Damage Audio").GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (health < 100) {
            heartbeatAudioSource.volume = (100f - health) / 100f;
            heartbeatAudioSource.clip = heartbeat;
            if (!heartbeatAudioSource.isPlaying) {
                heartbeatAudioSource.Play();
            }
        } else {
            if (heartbeatAudioSource.isPlaying) {
                heartbeatAudioSource.Stop();
            }
        }
    }
    
    protected override void DamageEffects(float amount, bool isCentral, bool isLeft, bool isRight) {
        damageAudioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        damageAudioSource.pitch = a;
        damageAudioSource.volume = b;
        damageAudioSource.Play();
        Vector3 sourcePosition = transform.position;
        if (isLeft) {
            CameraShaker.Shake(new KickShake(leftShakeParams, new CameraShake.Displacement(sourcePosition)));
        } else {
            CameraShaker.Shake(new KickShake(rightShakeParams, new CameraShake.Displacement(sourcePosition)));
        }
    }
}
