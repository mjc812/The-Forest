using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Limb : MonoBehaviour
{
    public GameObject hostile;
    private Health health;

    protected abstract float limbMultiplier { get; }
    public bool central;
    public bool left;
    public bool right;

    private void Start() {
        health = hostile.GetComponent<Health>();
    }

    public void ApplyDamage(float amount)
    {
        health.ApplyDamage(amount * limbMultiplier, central, left, right);
    }

    public bool isCentral() {
        return central;
    }

    public bool isLeft() {
        return left;
    }

    public bool isRight() {
        return right;
    }
}
