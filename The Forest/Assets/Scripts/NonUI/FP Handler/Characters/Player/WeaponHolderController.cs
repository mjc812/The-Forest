using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderController : MonoBehaviour
{
    private Weapon item = null;
    //private Animator animator;
    private Recoil mainCameraRecoil;
    private Transform swayHandler;
    private Transform tuckHandler;
    private Transform twistHandler;
    private Transform swingHandler;

    //make private later
    public float positionX = 0.2f;
    public float positionY = -0.35f;
    public float positionZ = 0.9f;

    public float rotationX = 0f;
    public float rotationY = -182f;
    public float rotationZ = 0f;

    void Start()
    {
        swayHandler = this.gameObject.transform.GetChild(0);
        tuckHandler = swayHandler.gameObject.transform.GetChild(0);
        twistHandler = tuckHandler.gameObject.transform.GetChild(0);
        swingHandler = twistHandler.gameObject.transform.GetChild(0);
        mainCameraRecoil = GameObject.FindWithTag("MainCamera").GetComponent<Recoil>();
        //animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
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
    }

    public void UseItem()
    {
        if (item && item.Use())
        {
            mainCameraRecoil.RecoilEffect();
            int randomAnimationNumber = UnityEngine.Random.Range(0, 3);
            //animator.SetTrigger("Fire" + randomAnimationNumber);
        }
    }
}
