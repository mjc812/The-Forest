using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerArm : Limb
{
    protected override float limbMultiplier
    {
        get => 0.1f;
    }

    protected override bool isTrigger
    {
        get => false;
    }
}