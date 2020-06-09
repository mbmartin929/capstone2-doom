using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    public string name;
    public int health;

    public GameObject bloodSplashGo;
    public ParticleSystem bloodParticleSystem;

    void Start()
    {

    }

    void Update()
    {

    }

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
        Destroy(gameObject);
    }

    public void PlayParticleSystem()
    {
        //if ()
        bloodParticleSystem.Play();
    }
}
