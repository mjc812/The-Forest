using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enfield : SingleFire
{
    public override int ID
    {
        get => 5;
    }

    public override string Description
    {
        get => "Enfield";
    }

    public override Sprite Sprite
    {
        get => null;
    }

    public override Vector3 weaponHolderPosition
    {
        get => new Vector3(0.25f, -0.4f, 0.5f);
    }

    public override Vector3 weaponHolderRotation
    {
        get => new Vector3(0f, 3f, 0f);
    }
}