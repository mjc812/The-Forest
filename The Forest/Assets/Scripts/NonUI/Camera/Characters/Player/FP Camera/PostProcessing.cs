using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    private PostProcessVolume postProcessingVolume;
    private Health health;

    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    private ColorGrading colorGrading;

    private bool headingToMax;
    private float transitionSpeed;
    private float timeCount;
    private float currentPercentage;
    private float snapshotPercentage;

    private float currentVignetteIntensity;
    private float baseVignetteIntensity;
    private float vignetteIntensityBoost;
    private float currentAberrationIntensity;
    private float baseAberrationIntensity;
    private float aberrationIntensityBoost;

    void Awake()
    {
        headingToMax = true;
        snapshotPercentage = 0f;
        currentPercentage = 0f;
        timeCount = 0f;
        transitionSpeed = 0.1f;

        health = GameObject.FindWithTag("Player").GetComponent<Health>();
        postProcessingVolume = gameObject.GetComponent<PostProcessVolume>();
        
        postProcessingVolume.profile.TryGetSettings(out vignette);
        currentVignetteIntensity = vignette.intensity.value;
        baseVignetteIntensity = vignette.intensity.value;
        vignetteIntensityBoost = 0.15f;

        postProcessingVolume.profile.TryGetSettings(out chromaticAberration);
        currentAberrationIntensity = chromaticAberration.intensity.value;
        baseAberrationIntensity = chromaticAberration.intensity.value;
        aberrationIntensityBoost = 0.7f;

        postProcessingVolume.profile.TryGetSettings(out colorGrading);
        Debug.Log(colorGrading.mixerRedOutRedIn.value);
        Debug.Log(colorGrading.mixerRedOutGreenIn.value);
        Debug.Log(colorGrading.mixerRedOutBlueIn.value);
    }

    private void Update() {
        // Debug.Log(health.RemainingHealthPercentage());
        if (Input.GetKeyDown(KeyCode.V)) {
            health.SetHealth(health.ReturnHealth() + 20f);
            colorGrading.mixerRedOutRedIn.value += 100f;
            colorGrading.mixerRedOutGreenIn.value += 100f;
            colorGrading.mixerRedOutBlueIn.value += 100f;
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
        }

        vignette.intensity.value = baseVignetteIntensity + (vignetteIntensityBoost * currentPercentage);
        chromaticAberration.intensity.value = baseAberrationIntensity + (aberrationIntensityBoost * currentPercentage);
    }
}
