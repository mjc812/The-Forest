using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private WeaponHolderController weaponHolder;
    private GameObject crosshair;
    private Weapon weapon;

    Quaternion rest;
    Quaternion tuck;
    Quaternion rotationalSnapshot;

    Vector3 positionalTuck;
    Vector3 positionalRest;
    Vector3 positionalSnapshot;

    float speed = 6f;
    float timeCount = 0.0f;
    bool tucking;

    void Awake() {
        crosshair = GameObject.FindWithTag("Crosshair");
        weaponHolder = GameObject.FindWithTag("WeaponHolder").GetComponent<WeaponHolderController>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        positionalTuck = new Vector3(0.23f, -0.1f, 0.1f);
        positionalRest = new Vector3(0f, 0f, 0f);
        positionalSnapshot = new Vector3(0f, 0f, 0f);

        tuck = Quaternion.Euler(0, 2.9f, 0);
        rest = Quaternion.Euler(0, 0, 0);
        rotationalSnapshot = rest = Quaternion.Euler(0, 0, 0);

        tucking = false;
    }

    void Update()
    {
        if (weaponHolder.getWeapon() != null) {
            Weapon weapon = weaponHolder.getWeapon();
            if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonDown(1)) {
                positionalSnapshot = transform.localPosition;
                rotationalSnapshot = transform.localRotation;
                timeCount = 0.0f;
            } else if (Input.GetMouseButton(1)) {
                crosshair.SetActive(false);
                if (!tucking) {
                    positionalSnapshot = transform.localPosition;
                    rotationalSnapshot = transform.localRotation;
                    timeCount = 0.0f;
                }
                tucking = true;
                transform.localPosition = Vector3.Slerp(positionalSnapshot, weapon.fpCameraAimPosition, timeCount * speed);
                transform.localRotation = Quaternion.Slerp(rotationalSnapshot, Quaternion.Euler(weapon.fpCameraAimRotation), timeCount * speed);
                timeCount = timeCount + Time.deltaTime;
            } else if (tucking) {
                crosshair.SetActive(true);
                tucking = false;
                positionalSnapshot = transform.localPosition;
                rotationalSnapshot = transform.localRotation;
                timeCount = 0.0f; 
            } else if ((transform.localPosition != positionalRest) || (transform.localRotation != rest)) {
                transform.localPosition = Vector3.Slerp(positionalSnapshot, positionalRest, timeCount * speed);
                transform.localRotation = Quaternion.Slerp(rotationalSnapshot, rest, timeCount * speed);
                timeCount = timeCount + Time.deltaTime;
            }
        }
    }
}
