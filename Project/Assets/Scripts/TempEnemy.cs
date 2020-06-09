using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    public string name;
    public int health;

    public GameObject bloodSplashGo;
    public GameObject[] bloodParticleSystemList;
    public Transform bloodSpawnLocation;

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
        foreach (GameObject item in bloodParticleSystemList)
        {
            if (gameObject == null) return;
            GameObject bloodParticleGo = Instantiate(item, bloodSpawnLocation.position, bloodSpawnLocation.rotation);
            bloodParticleGo.transform.parent = transform;
        }
    }
}
