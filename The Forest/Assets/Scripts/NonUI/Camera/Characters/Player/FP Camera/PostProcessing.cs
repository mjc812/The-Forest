using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    private PostProcessVolume postProcessingVolume;
    private Vignette vignette;
    private Health health;

    private float transitionSpeed;
    private float timeCount;
    private float currentVignetteIntensity;
    private float initialVignetteIntensity;
    private float maxVignetteIntensityBump;

    void Awake()
    {
        timeCount = 0f;
        transitionSpeed = 0.25f;
        health = GameObject.FindWithTag("Player").GetComponent<Health>();
        postProcessingVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessingVolume.profile.TryGetSettings(out vignette);
        currentVignetteIntensity = vignette.intensity.value;
        initialVignetteIntensity = vignette.intensity.value;
        maxVignetteIntensityBump = 0.2f;
    }

    private void Update() {
        if (currentVignetteIntensity != initialVignetteIntensity + (maxVignetteIntensityBump * ((100 - health.ReturnHealth()) / 100))) {
            float percentageComplete = timeCount / transitionSpeed;
            timeCount += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(
                currentVignetteIntensity, 
                initialVignetteIntensity + (maxVignetteIntensityBump * ((100 - health.ReturnHealth()) / 100)), 
                percentageComplete
            );
        } else {

        }
    }
}
