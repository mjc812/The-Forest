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
}
