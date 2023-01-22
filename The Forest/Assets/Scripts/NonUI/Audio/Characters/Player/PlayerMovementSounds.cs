using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSounds : MonoBehaviour
{
    private AudioSource audioSource;
    private PlayerMovement playerMovement;

    public AudioClip[] forwardStepAudioClip;
    public AudioClip[] forwardSprintStepAudioClip;
    public AudioClip[] backwardStepAudioClip;
    public AudioClip[] horizontalStepAudioClip;

    private float forwardStepAudioPitch = 0.8f;
    private float forwardStepTime = 0.7f;
    private float forwardStepTimeTotal = 0f;

    private float forwardSprintStepAudioPitch = 1f;
    private float forwardSprintStepTime = 0.35f;
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
                        audioSource.clip = forwardSprintStepAudioClip[Random.Range(0, forwardSprintStepAudioClip.Length)];
                        audioSource.pitch = forwardSprintStepAudioPitch;
                        audioSource.Play();
                    }
                    forwardSprintStepTimeTotal += Time.deltaTime;
                } else {
                    if (forwardStepTimeTotal > forwardStepTime) {
                        forwardStepTimeTotal = 0f;
                        audioSource.clip = forwardStepAudioClip[Random.Range(0, forwardStepAudioClip.Length)];
                        audioSource.pitch = forwardStepAudioPitch;
                        audioSource.Play();
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
                }
                backwardStepTimeTotal += Time.deltaTime;
            }
        }
    }
}
