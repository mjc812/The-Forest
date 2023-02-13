using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class FOV : MonoBehaviour
{
    private GameObject player;
    private PlayerSprint playerSprint;

    private bool headingToMax;
    private float transitionSpeed;
    private float returnTransitionSpeed;
    private float timeCount;
    private float currentPercentage;
    private float snapshotPercentage;

    private float baseFOV;
    private float fovBoost;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerSprint = player.GetComponent<PlayerSprint>();

        transitionSpeed = 0.1f;
        returnTransitionSpeed = 0.2f;
        headingToMax = true;
        timeCount = 0f;

        baseFOV = Camera.main.fieldOfView;
        fovBoost = 15f;
        
        currentPercentage = 0f;
        snapshotPercentage = 0f;
    }

    void Update() {
        if (playerSprint.isPlayerSprinting()) {
            if (!headingToMax) {
                snapshotPercentage = currentPercentage;
                headingToMax = true;
                timeCount = 0f;
            }
        } else {
            if (headingToMax) {
                snapshotPercentage = currentPercentage;
                headingToMax = false;
                timeCount = 0f;
            }
        }

        if (playerSprint.isPlayerSprinting()) {
            timeCount += Time.deltaTime;
            float percentageComplete = timeCount / transitionSpeed;
            currentPercentage = Mathf.Lerp(snapshotPercentage, fovBoost, percentageComplete);
        } else {
            float percentageComplete = timeCount / returnTransitionSpeed;
            timeCount += Time.deltaTime;
            currentPercentage = Mathf.Lerp(snapshotPercentage, 0f, percentageComplete);
        }

        Camera.main.fieldOfView = baseFOV + currentPercentage;
    }
}
