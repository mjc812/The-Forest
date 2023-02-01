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

    public float recoilSpeed = 75f;
    public float returnSpeed = 20f;

    private bool weaponHeld = false;
    private float timeElapsedSinceRecoil = 0f;

    void Update()
    {
        if (weaponHeld && Input.GetMouseButtonDown(0)) {
            addRecoil();
        }

        if (currentPosition != targetPosition) {
            timeElapsedSinceRecoil += Time.deltaTime;
            targetPosition = Vector3.Lerp(targetPosition, Vector3.zero, timeElapsedSinceRecoil / returnSpeed);
            currentPosition = Vector3.Slerp(currentPosition, targetPosition, Time.deltaTime * recoilSpeed);
            transform.localPosition = currentPosition;
        } else {
            timeElapsedSinceRecoil = 0f;
        }
    }

    public void addRecoil()
    {
        float x = 0f;
        float y = 0f;
        float z = zRecoil;

        targetPosition += new Vector3(x, y, z);
    }

    public void setWeaponHeld(bool set) {
        weaponHeld = set;
    } 
}
