using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using CameraShake;

public class PlayerSprint : MonoBehaviour
{
    private CharacterController characterController;
    private CameraShakerHandler cameraShakerHandler;
    private StatusPage statusPage;
    private Transform head;

    //[SerializeField] BounceShake.Params shakeParams;
    private float positiveYSpeed = 8f;
    float timeCount = 0.0f;
    [SerializeField] float stepTime = 0.5f;

    void Awake()
    {
        head = GameObject.FindWithTag("Head").transform;
        cameraShakerHandler = GameObject.FindWithTag("MainCamera").GetComponent<CameraShakerHandler>();
        characterController = GetComponent<CharacterController>();
        statusPage = GameObject.FindWithTag("StatusPage").GetComponent<StatusPage>();
    }

    void Update()
    {
        screenShake();
    }

    private void screenShake() {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift)) {
            timeCount = 0.0f;
        } else if (Input.GetKey(KeyCode.LeftShift)) {
            timeCount = timeCount + Time.deltaTime;
            if (timeCount >= stepTime) {
                //CameraShaker.Presets.ShortShake2D();
                //CameraShaker.Shake(new BounceShake(shakeParams, head.position));
                timeCount = 0.0f;
            }
        }
    }

    private void sprint() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            cameraShakerHandler.recoil();
        }
    }

    public float getPositiveYSpeed() {
        return positiveYSpeed;
    }

    public bool isPlayerSprinting() {
        return (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxis("Vertical") > 0));
    }

    public bool isSprintEngaged() {
        return ((Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift)) && (Input.GetAxis("Vertical") > 0));
    }
}
