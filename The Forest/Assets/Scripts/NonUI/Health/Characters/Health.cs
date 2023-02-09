using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected abstract int startingHealth { get; }
    protected abstract bool regenHealth { get; }

    protected float health;

    void Start() {
        health = startingHealth;        
    }

    public void ApplyDamage(float amount)
    {
        Debug.Log(health);
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
