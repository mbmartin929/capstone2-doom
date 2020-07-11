using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    float radius = 3f;
    float timer = 2f;
    float force = 500;
    float countdown;

    bool hasExplode;
    GameObject explotionParticle;
    // Start is called before the first frame update
    void Start()
    {
        countdown = timer;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0 && !hasExplode)
        {
            explode();
        }
    }

    void explode()
    {
        Debug.Log("KABOOM!");
        hasExplode = true;

        //instantiate explotion anim

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
            
            //damage enemy
        }
        Destroy(gameObject);

    }
}
