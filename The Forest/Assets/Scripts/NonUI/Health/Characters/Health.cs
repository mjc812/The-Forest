using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected float CurrentHealth;

    public void ApplyDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    public bool isDead()
    {
        return (CurrentHealth <= 0);
    }
}
