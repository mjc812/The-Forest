using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armaguerra : SingleFire
{
    public override int ID
    {
        get => 5;
    }

    public override string Description
    {
        get => "Armaguerra";
    }

    public override Sprite Sprite
    {
        get => null;
    }

    public override Vector3 weaponHolderPosition
    {
        get => new Vector3(0.2f, -0.85f, 1.3f);
    }

    public override Vector3 weaponHolderRotation
    {
        get => new Vector3(0f, -85f, 0f);
    }
}