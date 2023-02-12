using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thigh : Limb
{
    protected override float limbMultiplier
    {
        get => 0.7f;
    }

    protected override bool isTrigger
    {
        get => true;
    }

    protected override bool disableOnStart
    {
        get => false;
    }
}