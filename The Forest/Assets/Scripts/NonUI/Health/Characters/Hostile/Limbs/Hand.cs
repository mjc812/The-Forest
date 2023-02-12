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

    protected override bool disableOnStart
    {
        get => true;
    }

    private void OnTriggerEnter(Collider other)
    {
        hostileScript.DealDamage(25f, central, left, right);
    }
}
