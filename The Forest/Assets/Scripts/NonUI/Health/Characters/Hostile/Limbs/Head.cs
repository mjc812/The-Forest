using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : Limb
{
    protected override float limbMultiplier
    {
        get => 1.0f;
    }

    protected override bool isTrigger
    {
        get => false;
    }

    protected override bool disableOnStart
    {
        get => false;
    }
}
