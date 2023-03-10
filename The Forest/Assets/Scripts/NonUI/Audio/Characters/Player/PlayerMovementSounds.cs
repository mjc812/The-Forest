using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraShake;

public class PlayerMovementSounds : MonoBehaviour
{
    private AudioSource audioSource;
    private PlayerMovement playerMovement;

    public BounceShake.Params walkingShakeParams;
    public BounceShake.Params sprintingShakeParams;
    private Vector3 sourcePosition;

    public AudioClip[] forwardStepAudioClip;
    public AudioClip[] forwardSprintStepAudioClip;
    public AudioClip leftFootStep;
    public AudioClip rightFootStep;
    private AudioClip footStepToPlay; 
    public AudioClip[] backwardStepAudioClip;
    public AudioClip[] horizontalStepAudioClip;

    private float forwardStepAudioPitch = 0.8f;
    private float forwardStepTime = 0.7f;
    private float forwardStepTimeTotal = 0f;

    private float forwardSprintStepAudioPitch = 0.8f;
    private float forwardSprintStepTime = 0.33f;
    private float forwardSprintStepTimeTotal = 0f;

    private float backwardStepAudioPitch = 0.6f;
    private float backwardStepTime = 1.1f;
    private float backwardStepTimeTotal = 0f;

    private float horizontalStepAudioPitch = 1f;
    private float horizontalStepTime = 0.6f;
    private float horizontalbackwardStepTimeTotal = 0f;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        audioSource = transform.GetComponent<AudioSource>();
        footStepToPlay = leftFootStep;
    }

    void Update()
    {
        bool movingVertically = Input.GetAxis("Vertical") != 0;
        bool movingHorizontally = Input.GetAxis("Horizontal") != 0;

        if (movingVertically) {
            updateVerticalStep();
        } else if (movingHorizontally) {
            updateHorizontalStep();
        }
    }

    private void updateHorizontalStep() {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.D)) {
            horizontalbackwardStepTimeTotal = 0f;
        } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            if (horizontalbackwardStepTimeTotal > horizontalStepTime) {
                horizontalbackwardStepTimeTotal = 0f;
                audioSource.clip = horizontalStepAudioClip[Random.Range(0, horizontalStepAudioClip.Length)];
                audioSource.pitch = horizontalStepAudioPitch;
                audioSource.Play();
                sourcePosition = transform.position;
                CameraShaker.Shake(new BounceShake(walkingShakeParams, sourcePosition));
            }
            horizontalbackwardStepTimeTotal += Time.deltaTime;
        }
    }

    private void updateVerticalStep() {
        if (Input.GetAxis("Vertical") > 0) {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.W)) {
                forwardStepTimeTotal = 0f;
                forwardSprintStepTimeTotal = 0f;
            } else if (Input.GetKey(KeyCode.W)) {
                if (Input.GetKey(KeyCode.LeftShift)) {
                    if (forwardSprintStepTimeTotal > forwardSprintStepTime) {
                        forwardSprintStepTimeTotal = 0f;
                        audioSource.pitch = forwardSprintStepAudioPitch;
                        if (footStepToPlay == leftFootStep) {
                            audioSource.clip = leftFootStep;
                            footStepToPlay = rightFootStep;
                        } else {
                            audioSource.clip = rightFootStep;
                            footStepToPlay = leftFootStep;
                        }
                        audioSource.Play();
                        sourcePosition = transform.position;
                        CameraShaker.Shake(new BounceShake(sprintingShakeParams, sourcePosition));
                    }
                    forwardSprintStepTimeTotal += Time.deltaTime;
                } else {
                    if (forwardStepTimeTotal > forwardStepTime) {
                        forwardStepTimeTotal = 0f;
                        audioSource.clip = forwardStepAudioClip[Random.Range(0, forwardStepAudioClip.Length)];
                        audioSource.pitch = forwardStepAudioPitch;
                        audioSource.Play();
                        sourcePosition = transform.position;
                        CameraShaker.Shake(new BounceShake(walkingShakeParams, sourcePosition));
                    }
                    forwardStepTimeTotal += Time.deltaTime;
                }
            }
        } else {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyUp(KeyCode.S)) {
                backwardStepTimeTotal = 0f;
            } else if (Input.GetKey(KeyCode.S)) {
                if (backwardStepTimeTotal > backwardStepTime) {
                    backwardStepTimeTotal = 0f;
                    audioSource.clip = backwardStepAudioClip[Random.Range(0, backwardStepAudioClip.Length)];
                    audioSource.pitch = backwardStepAudioPitch;
                    audioSource.Play();
                    sourcePosition = transform.position;
                    CameraShaker.Shake(new BounceShake(walkingShakeParams, sourcePosition));
                }
                backwardStepTimeTotal += Time.deltaTime;
            }
        }
    }
}
