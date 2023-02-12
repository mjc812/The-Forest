using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    private PostProcessVolume postProcessingVolume;
    private Vignette vignette;
    private Health health;

    private float currentVignetteIntensity;
    
    private float targetVignetteIntensity;

    void Awake()
    {
        health = GameObject.FindWithTag("Player").GetComponent<Health>();
        postProcessingVolume = gameObject.GetComponent<PostProcessVolume>();
        postProcessingVolume.profile.TryGetSettings(out vignette);
        currentVignetteIntensity = vignette.intensity.value;
    }

    private void Update() {
        Debug.Log(health.ReturnHealth());
    }
}
