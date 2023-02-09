using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected abstract int startingHealth { get; }
    protected abstract bool regenHealth { get; }

    protected float health;

    public void ApplyDamage(float amount)
    {
        health -= amount;
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
