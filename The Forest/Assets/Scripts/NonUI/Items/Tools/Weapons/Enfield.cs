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
        get => new Vector3(0.1f, -0.19f, 0.22f);
    }

    public override Vector3 weaponHolderRotation
    {
        get => new Vector3(0f, 3f, 0f);
    }

    public override Vector3 fpCameraAimPosition
    {
        get => new Vector3(0.0934f, -0.039f, 0.1f);
    }

    public override Vector3 fpCameraAimRotation
    {
        get => new Vector3(0f, 2.96f, 0f);
    }
}