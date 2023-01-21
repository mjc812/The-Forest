using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPage : MonoBehaviour
{
    private bool active;
    private GameObject inventoryDisplay;

    void Awake() {
        active = false;
        inventoryDisplay = GameObject.FindWithTag("InventoryDisplay");
    }

    void Start()
    {
        inventoryDisplay.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Toggle();
        }
    }

    public bool isActive() {
        return active;
    }

    public void Toggle()
    {
        if (active)
        {
            Cursor.lockState = CursorLockMode.Locked;
            inventoryDisplay.SetActive(false);
        } else
        {
            Cursor.lockState = CursorLockMode.None;
            inventoryDisplay.SetActive(true);
        }
        active = !active;
    }
}
