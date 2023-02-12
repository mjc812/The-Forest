using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperArm : Limb
{
    protected override float limbMultiplier
    {
        get => 0.3f;
    }

    protected override bool isTrigger
    {
        get => false;
    }
}