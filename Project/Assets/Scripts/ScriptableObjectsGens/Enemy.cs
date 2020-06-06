using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy", order = 0)]
public class Enemy : ScriptableObject
{
    public string name;
    public int health;
    public GameObject prefab;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(prefab);
    }
}

