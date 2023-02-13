using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (health.isDead()) {
            Debug.Log("Player is dead");
        }
    }
}
