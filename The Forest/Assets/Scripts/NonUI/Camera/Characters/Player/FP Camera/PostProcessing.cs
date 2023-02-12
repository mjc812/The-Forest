using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    private PostProcessVolume postProcessingVolume;
    private Vignette vignette;
    private Health health;

    private float lastUpdateHealth;
    private float transitionSpeed;
    private float timeCount;
    private bool headingToMax;
    private float currentPercentage;
    private float snapshotPercentage;
    private float currentVignetteIntensity;
    private float initialVignetteIntensity;
    //private float maxVignetteIntensityBump;

    void Awake()
    {
        headingToMax = true;
        snapshotPercentage = 0f;
        currentPercentage = 0f;
        timeCount = 0f;
        transitionSpeed = 2f;
        health = GameObject.FindWithTag("Player").GetComponent<Health>();
        postProcessingVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessingVolume.profile.TryGetSettings(out vignette);
        currentVignetteIntensity = vignette.intensity.value;
        initialVignetteIntensity = vignette.intensity.value;
        //maxVignetteIntensityBump = 0.2f;
    }

    private void Update() {
        // Debug.Log(targetPercentage);
        if (Input.GetKeyDown(KeyCode.V)) {
            health.SetHealth(75f);
        }

        float targetPercentage = health.RemainingHealthPercentage();

        if (currentPercentage > targetPercentage) {
            if (headingToMax) {
                snapshotPercentage = currentPercentage;
                headingToMax = false;
                timeCount = 0f;
            }
        } else if (currentPercentage < targetPercentage) {
            if (!headingToMax) {
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
            // headingToMax = true;
            timeCount += Time.deltaTime;
            float percentageComplete = timeCount / transitionSpeed;
            currentPercentage = Mathf.Lerp(snapshotPercentage, targetPercentage, percentageComplete);
        } else if (currentPercentage > targetPercentage) {
            // headingToMax = false;
            float percentageComplete = timeCount / transitionSpeed;
            timeCount += Time.deltaTime;
            currentPercentage = Mathf.Lerp(snapshotPercentage, targetPercentage, percentageComplete);
        } else {

        }

        vignette.intensity.value = 1 * currentPercentage;

        // if (currentVignetteIntensity != initialVignetteIntensity + (maxVignetteIntensityBump * ((100 - health.ReturnHealth()) / 100))) {
        //     Debug.Log("in update");
        //     float percentageComplete = timeCount / transitionSpeed;
        //     timeCount += Time.deltaTime;
        //     currentVignetteIntensity = Mathf.Lerp(
        //         initialVignetteIntensity, 
        //         initialVignetteIntensity + (maxVignetteIntensityBump * ((100 - health.ReturnHealth()) / 100)), 
        //         percentageComplete
        //     );
        //     vignette.intensity.value = currentVignetteIntensity;
        // } else {

        // }
    }
}
