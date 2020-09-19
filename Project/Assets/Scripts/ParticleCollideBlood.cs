using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollideBlood : MonoBehaviour
{
    public GameObject[] bloodSplatGo;
    public AudioClip[] bloodSounds;


    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;


    public bool isEnabled = true;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        part = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnParticleCollision(GameObject other)
    {
        if (isEnabled)
        {
            Debug.Log("Start");

            int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);


            //Debug.Log("Particle Hit!");
            // if (Random.value > 0.5) //%50 percent chance
            // {
            //     //Debug.Log("50% Chance");
            // }

            // if (Random.value > 0.2) //%80 percent chance (1 - 0.2 is 0.8)
            // {
            //     //Debug.Log("80% Chance");

            // }

            if (Random.value > 0.99) //%30 percent chance (1 - 0.7 is 0.3)
            {
                Debug.Log("1% Chance");
                int i = 0;
                while (i < numCollisionEvents)
                {
                    Vector3 pos = collisionEvents[i].intersection;
                    //Instantiate(bloodSplatGo[Random.Range(0, bloodSplatGo.Length)], pos, Quaternion.identity);

                    RaycastHit hit;
                    int layerMask = LayerMask.GetMask("Ground");
                    if (Physics.Raycast(transform.position, -Vector3.up, out hit, 50f, layerMask))
                    {
                        var paintSplatter = GameObject.Instantiate(bloodSplatGo[Random.Range(0, bloodSplatGo.Length)],
                                                                               pos,
                                                                              // Rotation from the original sprite to the normal
                                                                              // Prefab are currently oriented to z+ so we use the opposite
                                                                              Quaternion.FromToRotation(Vector3.back, hit.normal)
                                                                              ) as GameObject;
                    }

                    //Instantiate(bloodSplatGo[Random.Range(0, bloodSplatGo.Length)], pos, Quaternion.LookRotation(other.GetComponent<Collision>().contacts[0].normal));
                    i++;
                }
            }
        }
    }
}
