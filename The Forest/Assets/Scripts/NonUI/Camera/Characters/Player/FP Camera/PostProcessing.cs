using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    private PostProcessVolume postProcessingVolume;
    private Vignette vignette;
    private Health health;

    private float lastUpdateTarget;
    private float transitionSpeed;
    private float timeCount;
    private bool headingToMax;
    private float currentPercentage;
    private float snapshotPercentage;
    private float currentVignetteIntensity;
    private float initialVignetteIntensity;

    void Awake()
    {
        health = GameObject.FindWithTag("Player").GetComponent<Health>();
        postProcessingVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessingVolume.profile.TryGetSettings(out vignette);
        currentVignetteIntensity = vignette.intensity.value;
        initialVignetteIntensity = vignette.intensity.value;
        headingToMax = true;
        snapshotPercentage = 0f;
        currentPercentage = 0f;
        lastUpdateTarget = 0f;
        timeCount = 0f;
        transitionSpeed = 4f;
    }

    private void Update() {
        // Debug.Log(health.RemainingHealthPercentage());
        // if (Input.GetKeyDown(KeyCode.V)) {
        //     health.SetHealth(100f);
        // }

        float targetPercentage = health.RemainingHealthPercentage();

        if (currentPercentage > targetPercentage) {
            if (headingToMax) {
                snapshotPercentage = currentPercentage;
                headingToMax = false;
                timeCount = 0f;
            }
        } else if (currentPercentage < targetPercentage) {
            if (!headingToMax) {
                snapshotPercentage = currentPercentage;
                headingToMax = true;
                timeCount = 0f;
            }
        } else {
            timeCount = 0f;
        }

        if (currentPercentage == targetPercentage) {
            timeCount = 0f;
            snapshotPercentage = currentPercentage;
        } else if (currentPercentage < targetPercentage) {
            timeCount += Time.deltaTime;
            float percentageComplete = timeCount / transitionSpeed;
            currentPercentage = Mathf.Lerp(snapshotPercentage, targetPercentage, percentageComplete);
        } else if (currentPercentage > targetPercentage) {
            float percentageComplete = timeCount / transitionSpeed;
            timeCount += Time.deltaTime;
            currentPercentage = Mathf.Lerp(snapshotPercentage, targetPercentage, percentageComplete);
        } else {

        }

        vignette.intensity.value = 1.5f * currentPercentage;
    }
}
