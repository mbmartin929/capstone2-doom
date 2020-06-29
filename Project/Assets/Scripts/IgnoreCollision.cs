using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetComponent<Rigidbody>().velocity);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collided with Enemy");
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        else
        {
            //Debug.Log("Hi");
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (collision.gameObject.layer == 8)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3.0f);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].tag == "Blood Splat")
                {
                    Destroy(gameObject);
                }
                i++;
            }


            GetComponent<Collider>().enabled = false;
            //Debug.Log("Hi");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;

            foreach (Transform child in transform)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
            }
        }

    }
}
