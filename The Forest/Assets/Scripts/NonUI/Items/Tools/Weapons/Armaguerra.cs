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

    public override Vector3 fpCameraAimPosition
    {
        get => new Vector3(0.23f, -0.1f, 0.1f);
    }

    public override Vector3 fpCameraAimRotation
    {
        get => new Vector3(0f, 2.9f, 0f);
    }
}