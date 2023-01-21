using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    private GameObject crosshair;

    private Animator animator;
    
    void Start()
    {
        crosshair = GameObject.FindWithTag("Crosshair");
        animator = transform.GetComponent<Animator>();
    }

    public void AimIn()
    {
        animator.Play("AimIn");
        crosshair.SetActive(false);
    }

    public void AimOut()
    {
        animator.Play("AimOut");
        crosshair.SetActive(true);
    }

}
