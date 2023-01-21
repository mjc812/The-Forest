using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistHandler : MonoBehaviour
{
    Quaternion max;
    Quaternion min;
    Quaternion rest;
    Quaternion snapshot;

    float headToMaxDuration = 0.20f;
    float headToMinDuration = 0.20f;
    float headToRestDuration = 0.05f;
    float timeCount = 0.0f;

    bool headToMax = false;
    float percentageToComplete = 1.0f;

    void Awake() {
        max = Quaternion.Euler(new Vector3(0, 0, 4f));
        min = Quaternion.Euler(new Vector3(0, 0, -4f));
        rest = Quaternion.Euler(new Vector3(0, 0, 0));
        snapshot = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            snapshot = transform.localRotation;
            timeCount = 0.0f;
        } else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            snapshot = transform.localRotation;
            timeCount = 0.0f;
        } else if (Input.GetKey(KeyCode.LeftShift)) {
            timeCount = timeCount + Time.deltaTime;
            float percentageComplete;
            if (headToMax) {
                percentageComplete = timeCount / headToMaxDuration;
                transform.localRotation = Quaternion.Lerp(snapshot, max, percentageComplete);
            } else {
                percentageComplete = timeCount / headToMinDuration;
                transform.localRotation = Quaternion.Lerp(snapshot, min, percentageComplete);
            }
            if (percentageComplete >= percentageToComplete) {
                timeCount = 0.0f;
                snapshot = transform.localRotation;
                percentageToComplete = Random.Range(0.7f, 1.0f);
                headToMax = !headToMax;
            }
        } else {
            if (transform.localRotation != rest) {
                timeCount = timeCount + Time.deltaTime;
                float percentageComplete = timeCount / headToRestDuration;
                transform.localRotation = Quaternion.Lerp(snapshot, rest, percentageComplete);
            }
        }
    }
}
