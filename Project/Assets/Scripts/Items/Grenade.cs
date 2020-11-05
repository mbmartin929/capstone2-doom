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

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

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

    public void LauncherExplode()
    {
        hasExplode = true;

        GameObject explosionSFX = new GameObject();
        GameObject _explosionSFX = Instantiate(explosionSFX, transform.position, Quaternion.identity);

        _explosionSFX.AddComponent<AudioSource>();
        _explosionSFX.GetComponent<AudioSource>().clip = audioSource.clip;
        _explosionSFX.GetComponent<AudioSource>().priority = 1;
        _explosionSFX.GetComponent<AudioSource>().spatialBlend = 0.8f;
        _explosionSFX.GetComponent<AudioSource>().Play();
        Destroy(_explosionSFX, 4.2f);
        //_explosionSFX.AddComponent<DestroyInTime>();

        //instantiate explotion anim
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            // RaycastHit hit;
            // if (Physics.Linecast(transform.position, nearbyObject.transform.position, out hit))
            // {
            //     if (hit.collider == nearbyObject)
            //     {
            //         if (hit.collider.tag == "Player")
            //         {
            //             Debug.Log("Hit Player");
            //             // hit.rigidbody.AddExplosionForce(0, transform.position, radius);
            //             GameManager.Instance.playerGo.GetComponent<PlayerController>().TakeDamage(damage);
            //         }
            //         else if (hit.collider.tag == "Enemy")
            //         {
            //             Debug.Log("Hit Enemy");
            //             hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(damage);

            //             EnemyController enemy = hit.transform.GetComponent<EnemyController>();

            //             foreach (GameObject item in enemy.bloodSplashGos)
            //             {
            //                 for (int i = 0; i < 3; i++)
            //                 {
            //                     if (item.tag == "Hit Normal")
            //                     {
            //                         GameObject bloodGo = Instantiate(item, hit.point, Quaternion.LookRotation(hit.normal));
            //                         //bloodGo.transform.parent = hit.transform;
            //                     }
            //                     else
            //                     {
            //                         GameObject bloodGo = Instantiate(item, hit.point /*+ (hit.transform.forward * 1f)*/,
            //                                                          item.transform.rotation);
            //                         //bloodGo.transform.parent = hit.transform;
            //                     }
            //                 }
            //             }
            //         }
            //         else if (hit.collider.tag == "Barricade")
            //         {
            //             Debug.Log("Hit Barricade");
            //             // hit.rigidbody.AddExplosionForce(0, transform.position, radius);
            //             Destroy(hit.collider.gameObject);
            //         }

            //     }
            // }


            if (nearbyObject.tag == "Player")
            {
                Debug.Log("Hit Player");
                // hit.rigidbody.AddExplosionForce(0, transform.position, radius);
                GameManager.Instance.playerGo.GetComponent<PlayerController>().TakeDamage(damage / 2);
            }
            else if (nearbyObject.tag == "Enemy")
            {
                Debug.Log("Hit Enemy");
                nearbyObject.gameObject.GetComponent<EnemyController>().TakeDamage(damage);

                EnemyController enemy = nearbyObject.transform.GetComponent<EnemyController>();

                foreach (GameObject item in enemy.bloodSplashGos)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            GameObject bloodGo = Instantiate(item, nearbyObject.transform.position, Quaternion.LookRotation(GameManager.Instance.playerGo.transform.position));
                            //bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            GameObject bloodGo = Instantiate(item, nearbyObject.transform.position, item.transform.rotation);

                        }
                    }
                }
            }
            else if (nearbyObject.tag == "Barricade")
            {
                Debug.Log(nearbyObject.name);
                // hit.rigidbody.AddExplosionForce(0, transform.position, radius);
                nearbyObject.GetComponent<Barricade>().TakeDamage(damage);
            }
            else if (nearbyObject.tag == "Egg")
            {
                nearbyObject.GetComponent<EggController>().TakeDamage(damage);
            }
            else if (nearbyObject.tag == "Resource Block")
            {
                Debug.Log("Hit Resource Block");
                ResourceBlock block = nearbyObject.GetComponent<ResourceBlock>();

                foreach (GameObject item in block.crystalEffects)
                {
                    if (item.tag == "Hit Normal")
                    {
                        ParticleSystem particleSys = item.GetComponent<ParticleSystem>();

                        GameObject crystalHitGo = Instantiate(item, nearbyObject.transform.position, Quaternion.LookRotation(GameManager.Instance.playerGo.transform.position));
                    }
                    else
                    {
                        GameObject crystalHitGo = Instantiate(item, nearbyObject.transform.position, item.transform.rotation);
                    }
                }
                nearbyObject.transform.GetComponent<ResourceBlock>().TakeDamage(damage);
            }
        }

        GameObject grenadeVfx = Instantiate(explosionParticle, this.transform.position, Quaternion.identity);
        if (useCrater)
        {
            GameObject grenadeCrate = Instantiate(grenadeCrater, this.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public void InstantiateCrater()
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