using UnityEngine;

public class PlayerHealth : Health
{
    protected override int startingHealth
    {
        get => 100;
    }

    protected override bool regenHealth
    {
        get => true;
    }

    protected override void DamageEffects(float amount, bool isCentral, bool isLeft, bool isRight) {
        Debug.Log("player hit");
    }
}
