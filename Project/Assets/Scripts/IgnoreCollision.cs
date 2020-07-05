﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetComponent<Rigidbody>().velocity);
    }

    //void OnCollisionEnter(Collision collision)
    //{
    // if (collision.gameObject.tag == "Enemy")
    // {
    //     Debug.Log("Collided with Enemy");
    //     Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
    // }
    // else
    // {
    //     //Debug.Log("Hi");
    //     //GetComponent<Rigidbody>().velocity = Vector3.zero;
    // }
    // if (collision.gameObject.layer == 8)
    // {
    //     Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3.0f);
    //     int i = 0;
    //     while (i < hitColliders.Length)
    //     {
    //         if (hitColliders[i].tag == "Blood Splat")
    //         {
    //             Destroy(gameObject);
    //         }
    //         i++;
    //     }


    //     GetComponent<Collider>().enabled = false;
    //     //Debug.Log("Hi");
    //     GetComponent<Rigidbody>().velocity = Vector3.zero;
    //     GetComponent<Rigidbody>().useGravity = false;

    //     foreach (Transform child in transform)
    //     {
    //         child.GetComponent<MeshRenderer>().enabled = true;
    //     }
    // }
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //Debug.Log("Collided with Enemy");
            //GetComponent<Rigidbody>().AddExplosionForce(Random.Range(150, 200f), transform.position, 500f, 0.0f, ForceMode.Force);
        }
        else if (other.gameObject.tag == "Level")
        {
            //GetComponent<Collider>().enabled = false;
            //Debug.Log("Hi");
            //Debug.Log(other.gameObject.name);
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            boxCollider.enabled = false;

        }
    }
}
