using UnityEngine;

public class CannibalHealth : Health
{
    private Mutant mutant;

    protected override void Start() {
        base.Start();
        mutant = GetComponent<Mutant>();
    }

    protected override int startingHealth
    {
        get => 100;
    }

    protected override bool regenHealth
    {
        get => false;
    }

    protected override void DamageEffects(float amount, bool isCentral, bool isLeft, bool isRight) {
        mutant.Hit(amount, isCentral, isLeft, isRight);
    }
}