using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderRecoil : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 currentPosition;

    // private float xRecoil = 0f;
    // private float yRecoil = 0f;
    public float zRecoil = -0.3f;

    public float recoilSpeed = 125f;
    public float returnSpeed = 50f;

    private Weapon weaponHeld;
    private float timeElapsedSinceRecoil = 0f;

    void Start() {
        currentPosition = transform.localPosition;
        targetPosition = currentPosition;
    }

    void Update()
    {
        if (weaponHeld && weaponHeld.IsReadyForUse() && Input.GetMouseButtonDown(0)) {
            addRecoil();
        }

        if (transform.localPosition != targetPosition) {
            Debug.Log("move ");
            timeElapsedSinceRecoil += Time.deltaTime;
            targetPosition = Vector3.Lerp(targetPosition, Vector3.zero, timeElapsedSinceRecoil / returnSpeed);
            currentPosition = Vector3.Slerp(currentPosition, targetPosition, Time.deltaTime * recoilSpeed);
            transform.localPosition = currentPosition;
        } else {
            Debug.Log("frozen " + transform.localPosition + " " + targetPosition);
            timeElapsedSinceRecoil = 0f;
            targetPosition = Vector3.zero;
        }
    }

    public void addRecoil()
    {
        float x = 0f;
        float y = 0f;
        float z = zRecoil;

        targetPosition += new Vector3(x, y, z);
    }

    public void setWeaponHeld(Weapon weapon) {
        weaponHeld = weapon;
    } 
}
