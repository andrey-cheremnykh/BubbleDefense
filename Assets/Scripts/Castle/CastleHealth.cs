using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour
{
    float maxHealth = 200;
    float health;

    public event Action onDestroy;

    bool isDestroyed = false;
    [SerializeField] Slider healthBar;

    private void Start()
    {
        SetupUpgradesInLevel setup = FindObjectOfType<SetupUpgradesInLevel>();
        maxHealth = setup.BaseHealth;
        health = maxHealth;
        onDestroy += DestroyCastle;
        healthBar.value = health / maxHealth;
    }

    public void GetDamage(float damage)
    {
        if (isDestroyed) return;
        health -= damage;
        healthBar.value = health / maxHealth;
        if (health <= 0.0001f)
        {
            onDestroy?.Invoke();
        }

    }

    void DestroyCastle()
    {
        print("Your castle is destroyed");
        isDestroyed = true;
    }

}
