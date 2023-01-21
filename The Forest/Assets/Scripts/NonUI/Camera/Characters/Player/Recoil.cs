using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    private Vector3 targetRotation;
    private Vector3 currentRotation;

    private float xRecoil = -2f;
    private float yRecoil = 2f;
    private float zRecoil = 0.35f;

    private float recoilSpeed = 6f;
    private float returnSpeed = 1f;

    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * returnSpeed);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, Time.deltaTime * recoilSpeed);

        gameObject.transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilEffect()
    {
        float x = xRecoil;
        float y = Random.Range(-yRecoil, yRecoil);
        float z = Random.Range(-zRecoil, zRecoil);

        targetRotation += new Vector3(x, y, z);
    }
}
