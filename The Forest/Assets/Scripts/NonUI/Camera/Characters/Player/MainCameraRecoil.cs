using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraRecoil : MonoBehaviour
{
    private Vector3 targetRotation;
    private Vector3 currentRotation;

    private float xRecoil = -15f;
    private float yRecoil = 4f;
    private float zRecoil = 2f;

    private float recoilSpeed = 25f;
    private float returnSpeed = 1f;

    private bool weaponHeld = false;

    void Update()
    {
        if (weaponHeld && Input.GetMouseButtonDown(0)) {
            addRecoil();
        } 
        if (currentRotation != targetRotation) {
            targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * returnSpeed);
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, Time.deltaTime * recoilSpeed);
            gameObject.transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }

    public void addRecoil()
    {
        float x = xRecoil;
        float y = Random.Range(-yRecoil, yRecoil);
        float z = Random.Range(-zRecoil, zRecoil);

        targetRotation += new Vector3(x, y, z);
    }

    public void setWeaponHeld(bool set) {
        weaponHeld = set;
    } 
}
