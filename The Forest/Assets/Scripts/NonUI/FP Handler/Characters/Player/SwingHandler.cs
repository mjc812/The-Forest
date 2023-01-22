using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingHandler : MonoBehaviour
{
    Quaternion rotationalMax;
    Quaternion rotationalMin;
    Quaternion rotationalRest;
    Quaternion rotationalSnapshot;

    Vector3 positionalRight;
    Vector3 positionalLeft;
    Vector3 positionalRest;
    Vector3 positionalSnapshot;

    float sprintMultiplier;

    float forwardWalkDivider = 1.5f;
    float forwardSprintDivider = 0.85f;
    float backwardWalkDivider = 3f;
    float horizontalWalkDivider = 1.2f;

    float headToMaxDuration = 0.25f;
    float headToMinDuration = 0.25f;
    float headToRestDuration = 0.15f;
    float timeCount = 0.0f;

    bool headToYMax;
    bool headToXMax;
    float percentageToComplete = 1.0f;

    void Awake() {
        positionalRight = new Vector3(0.01f, 0.01f, 0.005f);
        positionalLeft = new Vector3(-0.01f, 0.01f, 0.005f);
        positionalRest = new Vector3(0f, 0f, 0f);
        positionalSnapshot = transform.localPosition;

        rotationalMax = Quaternion.AngleAxis(1f, new Vector3(0f, 0.1f, 0f));
        rotationalMin = Quaternion.AngleAxis(1f, new Vector3(0f, -0.1f, 0f));
        rotationalRest = Quaternion.AngleAxis(1f, new Vector3(0f, 0f, 0f));
        rotationalSnapshot = Quaternion.AngleAxis(1f, new Vector3(0f, 0f, 0f));

        sprintMultiplier = 5f;

        headToYMax = false;
        headToXMax = false;
    }

    void Update()
    {
        Swing();
    }

    private void Swing() {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyUp(KeyCode.A)
         || Input.GetKeyDown(KeyCode.D) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyUp(KeyCode.S)) {
            positionalSnapshot = transform.localPosition;
            rotationalSnapshot = transform.localRotation;
            timeCount = 0.0f;
        } else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) ||Input.GetKey(KeyCode.D)) {
            updateTime();
            float percentageComplete;
            if (headToYMax) {
                percentageComplete = timeCount / headToMaxDuration;
                if (headToXMax) {
                    if (Input.GetKey(KeyCode.LeftShift)) {
                        transform.localPosition = Vector3.Slerp(positionalSnapshot, positionalRight * sprintMultiplier, percentageComplete);
                        transform.localRotation = Quaternion.Slerp(rotationalSnapshot, rotationalMin, percentageComplete);
                    } else {
                        transform.localPosition = Vector3.Slerp(positionalSnapshot, positionalRight, percentageComplete);
                        transform.localRotation = Quaternion.Slerp(rotationalSnapshot, rotationalMin, percentageComplete);
                    }
                } else {
                    if (Input.GetKey(KeyCode.LeftShift)) {
                        transform.localPosition = Vector3.Slerp(positionalSnapshot, positionalLeft * sprintMultiplier, percentageComplete);
                        transform.localRotation = Quaternion.Slerp(rotationalSnapshot, rotationalMax, percentageComplete);
                    } else {
                        transform.localPosition = Vector3.Slerp(positionalSnapshot, positionalLeft, percentageComplete);
                        transform.localRotation = Quaternion.Slerp(rotationalSnapshot, rotationalMax, percentageComplete);
                    }
                }
            } else {
                percentageComplete = timeCount / headToMinDuration;
                transform.localPosition = Vector3.Slerp(positionalSnapshot, positionalRest, percentageComplete);
                transform.localRotation = Quaternion.Slerp(rotationalSnapshot, rotationalRest, percentageComplete);
            }
            if (percentageComplete >= percentageToComplete) {
                percentageComplete = 0f;
                timeCount = 0.0f;
                positionalSnapshot = transform.localPosition;
                rotationalSnapshot = transform.localRotation;
                percentageToComplete = Random.Range(0.7f, 1.0f);
                if (!headToYMax) {
                    headToXMax = !headToXMax;
                }
                headToYMax = !headToYMax;
            }
        } else {
            if ((transform.localPosition != positionalRest)|| (transform.localRotation != rotationalRest)) {
                updateTime();
                float percentageComplete = timeCount / headToRestDuration;
                transform.localPosition = Vector3.Slerp(positionalSnapshot, positionalRest, percentageComplete);
                transform.localRotation = Quaternion.Slerp(rotationalSnapshot, rotationalRest, percentageComplete);
            }
        }
    }

    private void updateTime() {
        if (Input.GetKey(KeyCode.W)) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                timeCount = timeCount + (Time.deltaTime / forwardSprintDivider);
            } else {
                timeCount = timeCount + (Time.deltaTime / forwardWalkDivider);
            }
        } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            timeCount = timeCount + (Time.deltaTime / horizontalWalkDivider);
        } else if (Input.GetKey(KeyCode.S)) {
            timeCount = timeCount + (Time.deltaTime / backwardWalkDivider);
        } else {
            timeCount = timeCount + Time.deltaTime;
        }
    }
}
