using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Limb
{
    protected override float limbMultiplier
    {
        get => 0.1f;
    }

    protected override bool isTrigger
    {
        get => true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (left) {
            Debug.Log("hit left");
        } else {
            Debug.Log("hit right");
        }
    }
}
