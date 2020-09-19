using EightDirectionalSpriteSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float radius = 3f;
    float timer = 2f;
    float grenadeForceExplosion = 500;
    float countdown;
    int power = 10;
    bool canFire;

    UnitController player;

    bool hasExplode;
    GameObject explotionParticle;
    // Start is called before the first frame update
    void Start()
    {
        countdown = timer;

        UnitController player = GameManager.Instance.playerGo.GetComponent<PlayerController>();
        //UnitController enemy = 
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !hasExplode)
        {
            Explode();
            hasExplode = true;
  

        }
    }

    void Explode()
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
                            GameManager.Instance.playerGo.GetComponent<PlayerController>().TakeDamage(power);
                        }
                        if (hit.collider.tag == "Enemy")
                        {
                            EnemyController enemy = hit.transform.GetComponent<EnemyController>();                            
                            enemy = hit.transform.GetChild(0).GetComponent<EnemyController>();

                            Debug.Log(enemy.CurrentHealth);
                            hit.rigidbody.AddExplosionForce(0, transform.position, radius);
                            enemy.TakeDamage(power);
                            Debug.Log("HERE" + hit.transform.name);
                           
                            Debug.Log(power);
                            Debug.Log(enemy.CurrentHealth);

                            //DAMAGE ENEMY
                        }
                        if (hit.collider.tag == "Untagged")
                        {
                            hit.rigidbody.AddExplosionForce(grenadeForceExplosion, transform.position, radius);
                        }
                    }
                }
                Destroy(gameObject);
           
            }
        }
        //Destroy(gameObject);

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


}
