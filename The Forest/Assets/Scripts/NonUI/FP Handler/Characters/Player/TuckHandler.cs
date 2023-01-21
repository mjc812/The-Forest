using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuckHandler : MonoBehaviour
{
    private PlayerMovement playerMovement;

    Quaternion rest;
    Quaternion tuck;
    Quaternion rotationalSnapshot;

    Vector3 positionalTuck;
    Vector3 positionalRest;
    Vector3 positionalSnapshot;

    float speed = 6f;
    float timeCount = 0.0f;
    bool tucking;

    void Awake() {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        positionalTuck = new Vector3(0f, -0.1f, 0f);
        positionalRest = new Vector3(0f, 0f, 0f);
        positionalSnapshot = new Vector3(0f, 0f, 0f);

        rest = Quaternion.Euler(0, 0, 0);
        tuck = Quaternion.Euler(10, -25, 25);
        rotationalSnapshot = rest = Quaternion.Euler(0, 0, 0);

        tucking = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift)) {
            positionalSnapshot = transform.localPosition;
            rotationalSnapshot = transform.localRotation;
            timeCount = 0.0f;
        } else if (Input.GetKey(KeyCode.LeftShift) && playerMovement.isPlayerSprinting()) {
            if (!tucking) {
                positionalSnapshot = transform.localPosition;
                rotationalSnapshot = transform.localRotation;
                timeCount = 0.0f;
            }
            tucking = true;
            transform.localPosition = Vector3.Slerp(positionalSnapshot, positionalTuck, timeCount * speed);
            transform.localRotation = Quaternion.Slerp(rotationalSnapshot, tuck, timeCount * speed);
            timeCount = timeCount + Time.deltaTime;
        } else if (tucking) {
            tucking = false;
            positionalSnapshot = transform.localPosition;
            rotationalSnapshot = transform.localRotation;
            timeCount = 0.0f; 
        } else if ((transform.localPosition != positionalRest) || (transform.localRotation != rest)) {
            transform.localPosition = Vector3.Slerp(positionalSnapshot, positionalRest, timeCount * speed);
            transform.localRotation = Quaternion.Slerp(rotationalSnapshot, rest, timeCount * speed);
            timeCount = timeCount + Time.deltaTime;
        }
    }
}
