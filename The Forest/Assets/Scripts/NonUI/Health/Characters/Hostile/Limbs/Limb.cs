using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Limb : MonoBehaviour
{
    public GameObject hostile;
    private Health health;

    protected abstract float limbMultiplier { get; }

    private void Start() {
        health = hostile.GetComponent<Health>();
    }

    public void ApplyDamage(float amount)
    {
        health.ApplyDamage(amount * limbMultiplier);
    }
}
