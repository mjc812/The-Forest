using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    private GameObject crosshair;

    //private Animator animator;

    float positionX = 0.23f;
    float positionY = -0.1f;
    float positionZ = 0.1f;

    float rotationX = 0f;
    float rotationY = 2.9f;
    float rotationZ = 0f;
    
    void Start()
    {
        crosshair = GameObject.FindWithTag("Crosshair");
        //animator = transform.GetComponent<Animator>();
    }

    public void AimIn()
    {
        transform.localPosition = new Vector3(positionX, positionY, positionZ);
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        //animator.Play("AimIn");
        crosshair.SetActive(false);
    }

    public void AimOut()
    {
        //animator.Play("AimOut");
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        crosshair.SetActive(true);
    }

}
