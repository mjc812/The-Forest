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
        if (health > 0) {
            health -= amount;
            DamageEffects(amount, isCentral, isLeft, isRight);
        }
    }

    public void ApplyHealth(float amount)
    {
        health += amount;
    }

    public void SetHealth(float amount)
    {
        if (amount < 100f) {
            health = amount;
        } else {
            health = 100f;
        }
    }

    public float HealthPercentage() {
        return health / startingHealth;
    }

    public float RemainingHealthPercentage() {
        return 1 - (health / startingHealth);
    }

    public float ReturnHealth()
    {
        return health;
    }

    public float ReturnRemainingHealth()
    {
        return startingHealth - health;
    }

    public bool isDead()
    {
        return health <= 0;
    }

}
