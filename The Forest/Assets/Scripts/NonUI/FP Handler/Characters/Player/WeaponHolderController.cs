using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderController : MonoBehaviour
{
    private Weapon item = null;
    private MainCameraRecoil mainCameraRecoil;
    private WeaponHolderRecoil weaponHolderRecoil;
    private Transform swayHandler;
    private Transform tuckHandler;
    private Transform twistHandler;
    private Transform swingHandler;

    //make private later
    float positionX = 0.25f;
    float positionY = -0.4f;
    float positionZ = 0.5f;

    float rotationX = 0f;
    float rotationY = 3f;
    float rotationZ = 0f;

    void Start()
    {
        swayHandler = this.gameObject.transform.GetChild(0);
        tuckHandler = swayHandler.gameObject.transform.GetChild(0);
        twistHandler = tuckHandler.gameObject.transform.GetChild(0);
        swingHandler = twistHandler.gameObject.transform.GetChild(0);
        mainCameraRecoil = GameObject.FindWithTag("MainCamera").GetComponent<MainCameraRecoil>();
        weaponHolderRecoil = GameObject.FindWithTag("WeaponHolder").GetComponent<WeaponHolderRecoil>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UseItem();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
    }

    public void DropItem()
    {
        if (item)
        {
            item.Drop();
            item.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            item.transform.parent = null;
            item = null;
            mainCameraRecoil.setWeaponHeld(false);
            weaponHolderRecoil.setWeaponHeld(false);
        }
    }

    public void HoldItem(Weapon weapon)
    {
        DropItem();
        item = weapon;
        Transform itemTransform = weapon.transform;
        itemTransform.parent = swingHandler;
        itemTransform.localPosition = new Vector3(positionX, positionY, positionZ);
        itemTransform.localRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        itemTransform.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        mainCameraRecoil.setWeaponHeld(true);
        weaponHolderRecoil.setWeaponHeld(true);
    }

    public void UseItem()
    {
        if (item && item.Use())
        {
            int randomAnimationNumber = UnityEngine.Random.Range(0, 3);
        }
    }

    public bool isHoldingItem()
    {
        return (item != null);
    }
}
