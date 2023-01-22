using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderRecoil : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 currentPosition;

    private float xRecoil = 0f;
    private float yRecoil = 0f;
    private float zRecoil = -0.3f;

    private float recoilSpeed = 50f;
    private float returnSpeed = 10f;

    private bool weaponHeld = false;

    void Update()
    {
        if (weaponHeld && Input.GetMouseButtonDown(0)) {
            addRecoil();
        } 
        if (currentPosition != targetPosition) {
            targetPosition = Vector3.Lerp(targetPosition, Vector3.zero, Time.deltaTime * returnSpeed);
            currentPosition = Vector3.Slerp(currentPosition, targetPosition, Time.deltaTime * recoilSpeed);
            transform.localPosition = currentPosition;
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
