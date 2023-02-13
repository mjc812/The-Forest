using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Limb : MonoBehaviour
{
    public GameObject hostile;
    public Mutant hostileScript;
    private Health health;
    private Collider col;

    protected abstract float limbMultiplier { get; }
    protected abstract bool isTrigger { get; }
    protected abstract bool disableOnStart { get; }
    public bool central;
    public bool left;
    public bool right;

    private void Start() {
        health = hostile.GetComponent<Health>();
        hostileScript = hostile.GetComponent<Mutant>();
        col = GetComponent<Collider>();
        if (disableOnStart) {
            col.enabled = false;
        }
        if (isTrigger) {
            col.isTrigger = true;
        } else {
            col.isTrigger = false;
        }
    }

    public void ApplyDamage(float amount)
    {
        health.ApplyDamage(amount * limbMultiplier, central, left, right);
    }

    public bool isCentral() {
        return central;
    }

    public void Enable() {
        col.enabled = true;
    }

    public void Disable() {
        col.enabled = false;
    }

    public void EnableTrigger() {
        col.isTrigger = true;
    }

    public void DisableTrigger() {
        col.isTrigger = false;
    }

    public bool isLeft() {
        return left;
    }

    public bool isRight() {
        return right;
    }
}
