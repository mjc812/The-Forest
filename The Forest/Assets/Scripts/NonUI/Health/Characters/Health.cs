using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected abstract int startingHealth { get; }
    protected abstract bool regenHealth { get; }

    protected float health;

    protected virtual void Start() {
        health = startingHealth;        
    }

    protected abstract void DamageEffects(float amount, bool isCentral, bool isLeft, bool isRight);

    public void ApplyDamage(float amount, bool isCentral, bool isLeft, bool isRight)
    {
        health -= amount;
        DamageEffects(amount, isCentral, isLeft, isRight);
    }

    public void ApplyRegen(float amount)
    {
        health += amount;
    }

    public bool isDead()
    {
        return health <= 0;
    }
}
