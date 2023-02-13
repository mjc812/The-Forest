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
    private Bloom bloom;

    private bool headingToMax;
    private float transitionSpeed;
    private float returnTransitionSpeed;
    private float timeCount;
    private float currentPercentage;
    private float snapshotPercentage;

    private float currentVignetteIntensity;
    private float baseVignetteIntensity;
    private float vignetteIntensityBoost;

    private float currentAberrationIntensity;
    private float baseAberrationIntensity;
    private float aberrationIntensityBoost;

    private float currentBloomIntensity;
    private float baseBloomIntensity;
    private float bloomIntensityBoost;

    private float currentRedOutRedInIntensity;
    private float baseRedOutRedInIntensity;
    private float redOutRedInIntensityBoost;

    private float currentGreenOutGreenInIntensity;
    private float baseGreenOutGreenInIntensity;
    private float greenOutGreenInIntensityBoost;

    private float currentBlueOutBlueInIntensity;
    private float baseBlueOutBlueInIntensity;
    private float blueOutBlueInIntensityBoost;

    void Awake()
    {
        headingToMax = true;
        snapshotPercentage = 0f;
        currentPercentage = 0f;
        timeCount = 0f;
        transitionSpeed = 0.1f;
        returnTransitionSpeed = 2f;

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

        postProcessingVolume.profile.TryGetSettings(out bloom);
        currentBloomIntensity = bloom.intensity.value;
        baseBloomIntensity = bloom.intensity.value;
        bloomIntensityBoost = 10f;

        postProcessingVolume.profile.TryGetSettings(out colorGrading);
        currentRedOutRedInIntensity = colorGrading.mixerRedOutRedIn.value;
        baseRedOutRedInIntensity = colorGrading.mixerRedOutRedIn.value;
        redOutRedInIntensityBoost = 100f;

        currentGreenOutGreenInIntensity = colorGrading.mixerGreenOutGreenIn.value;
        baseGreenOutGreenInIntensity = colorGrading.mixerGreenOutGreenIn.value;
        greenOutGreenInIntensityBoost = -100f;

        currentBlueOutBlueInIntensity = colorGrading.mixerBlueOutBlueIn.value;
        baseBlueOutBlueInIntensity = colorGrading.mixerBlueOutBlueIn.value;
        blueOutBlueInIntensityBoost = -100f;
    }

    private void Update() {
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
            float percentageComplete = timeCount / returnTransitionSpeed;
            timeCount += Time.deltaTime;
            currentPercentage = Mathf.Lerp(snapshotPercentage, targetPercentage, percentageComplete);
        }

        vignette.intensity.value = baseVignetteIntensity + (vignetteIntensityBoost * currentPercentage);
        chromaticAberration.intensity.value = baseAberrationIntensity + (aberrationIntensityBoost * currentPercentage);
        bloom.intensity.value = baseBloomIntensity + (bloomIntensityBoost * currentPercentage);
        colorGrading.mixerRedOutRedIn.value = baseRedOutRedInIntensity + (redOutRedInIntensityBoost * currentPercentage);
        colorGrading.mixerGreenOutGreenIn.value = baseGreenOutGreenInIntensity + (greenOutGreenInIntensityBoost * currentPercentage);
        colorGrading.mixerBlueOutBlueIn.value = baseBlueOutBlueInIntensity + (blueOutBlueInIntensityBoost * currentPercentage);
    }
}
