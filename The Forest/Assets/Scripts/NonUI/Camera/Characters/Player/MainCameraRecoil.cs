using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraRecoil : MonoBehaviour
{
    private PlayerSprint playerSprint;

    private Vector3 targetRotation;
    private Vector3 currentRotation;

    private float xRecoil = -10f;
    private float yRecoil = 2f;
    private float zRecoil = 0.2f;

    private float recoilSpeed = 0.25f;
    private float returnSpeed = 25f;

    private Weapon weaponHeld;
    private bool movingToTarget = false;
    private float timeElapsedSinceRecoil = 0f;
    private float timeElapsedSinceRecoilEnd = 0f;

    void Start() {
        playerSprint = GameObject.FindWithTag("Player").GetComponent<PlayerSprint>();
    }

    void Update()
    {
        if (weaponHeld && weaponHeld.IsReadyForUse() && Input.GetMouseButtonDown(0) && !playerSprint.isPlayerSprinting()) {
            addRecoil();
            movingToTarget = true;
        } else if (movingToTarget && (currentRotation != targetRotation)) {
            timeElapsedSinceRecoil = timeElapsedSinceRecoil + Time.deltaTime;
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, timeElapsedSinceRecoil / recoilSpeed);
            transform.localRotation = Quaternion.Euler(currentRotation);
        } else if (!movingToTarget && (currentRotation != targetRotation)) {
            timeElapsedSinceRecoilEnd = timeElapsedSinceRecoilEnd + Time.deltaTime;
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, timeElapsedSinceRecoilEnd / returnSpeed);
            transform.localRotation = Quaternion.Euler(currentRotation);
        } else {
            targetRotation = new Vector3(0f, 0f, 0f);
            movingToTarget = false;
            timeElapsedSinceRecoil = 0f;
            timeElapsedSinceRecoilEnd = 0f;
        }
    }

    public void addRecoil()
    {
        float x = xRecoil;
        float y = Random.Range(-yRecoil, yRecoil);
        float z = Random.Range(-zRecoil, zRecoil);

        targetRotation += new Vector3(x, y, z);
    }

    public void setWeaponHeld(Weapon weapon) {
        weaponHeld = weapon;
    } 
}
