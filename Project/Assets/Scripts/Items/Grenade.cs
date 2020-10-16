using EightDirectionalSpriteSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float radius = 3f;
    public float timer = 2f;
    float grenadeForceExplosion = 500;
    float countdown;
    public int damage = 50;
    bool canFire;

    UnitController player;

    bool hasExplode;
    public GameObject explosionParticle;
    public GameObject grenadeCrater;
    public bool useCrater = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = timer;

        UnitController player = GameManager.Instance.playerGo.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // countdown -= Time.deltaTime;
        // if (countdown <= 0 && !hasExplode)
        // {
        //     instantiateCrater();
        //     Explode();
        //     hasExplode = true;
        // }

        //raycast this grenade

    }

    public void EnemyExplode()
    {
        hasExplode = true;
        //instantiate explotion anim
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, nearbyObject.transform.position, out hit))
            {
                if (hit.collider == nearbyObject)
                {
                    if (hit.rigidbody)
                    {
                        if (hit.collider.tag == "Player")
                        {
                            hit.rigidbody.AddExplosionForce(0, transform.position, radius);
                            GameManager.Instance.playerGo.GetComponent<PlayerController>().TakeDamage(damage);
                        }
                    }
                }
            }
        }

        GameObject grenadeVfx = Instantiate(explosionParticle, this.transform.position, Quaternion.identity);
        if (useCrater)
        {
            GameObject grenadeCrate = Instantiate(grenadeCrater, this.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }


    public void instantiateCrater()
    {
        Vector3 currentPos = this.transform.position;
        RaycastHit hit;

        if (Physics.Raycast(currentPos, Vector3.down, out hit))
        {
            GameObject grenadeCrate = Instantiate(grenadeCrater, hit.point, Quaternion.identity);
            grenadeCrate.transform.rotation = Quaternion.LookRotation(transform.up, hit.normal) * grenadeCrate.transform.rotation;
        }
    }
}




//void explode()
//{
//    Debug.Log("KABOOM!");
//    hasExplode = true;

//    //instantiate explotion anim

//    Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

//    foreach(Collider nearbyObject in colliders)
//    {
//        Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
//        if(rb != null)
//        {
//            rb.AddExplosionForce(grenadeForceExplosion, transform.position, radius);
//        }

//        //damage enemy
//    }
//    Destroy(gameObject);

//}


