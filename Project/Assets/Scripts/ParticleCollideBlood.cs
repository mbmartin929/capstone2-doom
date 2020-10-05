using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollideBlood : MonoBehaviour
{
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;


    public bool isEnabled = true;

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
            // Debug.Log("Collided GameObject: " + other.name);
            // Debug.Log("Collided Layer: " + other.layer);
            // Debug.Log("Target LayerMask: " + layerMask);
            // if (other.gameObject.layer == layerMask)
            // {
            int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

            if (Random.value > 0.99)
            {
                //Debug.Log("NumCollisionEvents:" + numCollisionEvents);

                int i = 0;
                while (i < numCollisionEvents)
                {
                    Vector3 pos = collisionEvents[i].intersection;
                    int layerMask = LayerMask.GetMask("Ground");
                    RaycastHit hit;

                    if (Physics.Raycast(transform.position, -Vector3.up, out hit, 50f, layerMask))
                    {
                        //Debug.Log("Collision ID: " + i);

                        // var paintSplatter = GameObject.Instantiate(bloodSplatGo[Random.Range(0, bloodSplatGo.Length)],
                        //                                                        hit.point + 0.01f * hit.normal,
                        //                                                       // Rotation from the original sprite to the normal
                        //                                                       // Prefab are currently oriented to z+ so we use the opposite
                        //                                                       Quaternion.LookRotation(hit.normal)
                        //                                                       ) as GameObject;

                        StartCoroutine(GetComponent<DecalPainter>().Paint(pos + 0.01f * hit.normal, 1, 1.0f, 0));
                    }
                    i++;

                    if (i >= 2)
                    {
                        //Debug.Log("i: " + i);
                        Destroy(gameObject);
                        return;
                    }
                }
            }
        }
    }
}

