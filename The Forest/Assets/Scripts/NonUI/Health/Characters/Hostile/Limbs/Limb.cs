using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Limb : MonoBehaviour
{
    public GameObject hostile;
    private Health health;
    private BoxCollider trigger;

    protected abstract float limbMultiplier { get; }
    protected abstract bool isTrigger { get; }
    public bool central;
    public bool left;
    public bool right;

    private void Start() {
        health = hostile.GetComponent<Health>();
        if (isTrigger) {
            trigger = GetComponent<BoxCollider>();
            trigger.enabled = false;
        }
    }

    public void ApplyDamage(float amount)
    {
        health.ApplyDamage(amount * limbMultiplier, central, left, right);
    }

    public bool isCentral() {
        return central;
    }

    public void EnableTrigger() {
        trigger.enabled = true;
    }

    public void DisableTrigger() {
        trigger.enabled = false;
    }

    public bool isLeft() {
        return left;
    }

    public bool isRight() {
        return right;
    }
}
