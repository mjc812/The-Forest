using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Transform playerTransform;
    private Transform headTransform;

    private bool invert = false;
    private float sensivity = 5f;
    private float cameraXMin = -90f;
    private float cameraXMax = 90f;

    private float playerY = 0f;
    private float cameraX = 0f;

    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        headTransform = GameObject.FindWithTag("Head").GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Look(float mouseY, float mouseX) {
        CameraX(mouseY);
        CameraY(mouseX);

        playerTransform.localRotation = Quaternion.Euler(0f, playerY, 0f);
        headTransform.localRotation = Quaternion.Euler(cameraX, 0f, 0f);
    }

    void CameraX(float mouseY)
    {
        cameraX = cameraX + (mouseY * sensivity * (invert ? 1f : -1f));
        cameraX = Mathf.Clamp(cameraX, cameraXMin, cameraXMax);
    }
    void CameraY(float mouseX)
    {
        playerY = playerY + (mouseX * sensivity);        
    }
}
