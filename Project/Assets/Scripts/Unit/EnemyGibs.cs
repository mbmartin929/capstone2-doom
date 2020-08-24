﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGibs : MonoBehaviour
{
    public float explosionForece = 10.0f;
    public float radius = 5.0f;
    public float minY = 420f;
    public float maxY = 960;

    public float minX = 240;
    public float maxX = 420;

    public float minZ = 240;
    public float maxZ = 420;

    public float gravity = 9.8f;

    public Material[] gibMats;

    private Vector3 velocity;
    private Rigidbody rb;

    private float startingY;

    private bool startTransparency = false;

    private float lerpStart;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingY = transform.position.y;

        GetComponent<MeshRenderer>().material = gibMats[Random.Range(0, gibMats.Length)];

        //GetComponent<Rigidbody>().AddExplosionForce(explosionForece, transform.position, radius, upwardsModifier);

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        float z = Random.Range(minZ, maxZ);

        GetComponent<Rigidbody>().AddForce(x, y, z);
        StartCoroutine(LateCollision());

        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startTransparency)
        {
            // Color tempcolor = gameObject.GetComponent<MeshRenderer>().material.color;
            // tempcolor.a = Mathf.MoveTowards(1, 0, Time.deltaTime);
            // GetComponent<MeshRenderer>().material.SetColor("_BaseColor", tempcolor);

            float progress = Time.time - lerpStart;
            Color tempcolor = gameObject.GetComponent<MeshRenderer>().material.color;

            //Debug.Log("Before: " + tempcolor.a);
            tempcolor.a = Mathf.Lerp(1.0f, 0.0f, progress / 1.0f);
            //Debug.Log("After: " + tempcolor.a);

            GetComponent<MeshRenderer>().material.SetColor("_BaseColor", tempcolor);
        }
    }

    private void Transparency()
    {

    }

    private IEnumerator LateCollision()
    {
        GetComponent<BoxCollider>().isTrigger = true;

        yield return new WaitForSeconds(0.15f);

        GetComponent<BoxCollider>().isTrigger = false;

        yield return new WaitForSeconds(0.3f);

        GetComponent<BoxCollider>().isTrigger = true;

        yield return new WaitForSeconds(1.5f);

        lerpStart = Time.time;
        startTransparency = true;
        //InvokeRepeating("Transparency", 0.15f, 0.1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level")
        {
            // Debug.Log("Collide");
        }

        //Debug.Log(other.name);

        if (other.CompareTag("Level"))
        {
            // gravity = 0;

            // Debug.Log("Collide");
            // rb.useGravity = false;
            // rb.velocity = Vector3.zero;

            // Debug.Log(gameObject.name + ": " + other.transform.position.y + " vs " + startingY);
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            GetComponent<BoxCollider>().enabled = false;

            RaycastHit hit;
            int layerMask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 50f, layerMask))
            {
                //Debug.Log("Gib Paint");

                int randomBloodNumber = Random.Range(1, 5);
                StartCoroutine(GetComponent<DecalPainter>().Paint(hit.point + hit.normal * 1f, 1, 1.0f, 0));
            }
            // if (other.transform.position.y > startingY)
            // {
            //     Debug.Log(other.transform.position.y + " vs " + startingY);
            //     return;
            // }
            // else
            // {
            //     Debug.Log(gameObject.name + ": " + other.transform.position.y + " vs " + startingY);
            //     rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            //     GetComponent<BoxCollider>().enabled = false;
            // }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Gib"))
        {
            Physics.IgnoreCollision(other.collider, GetComponent<BoxCollider>());
            //Debug.Log("Collided with Gib");
        }
        if (other.gameObject.CompareTag("Level"))
        {
            //gravity = 0;
            //rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            //GetComponent<MeshCollider>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            //Debug.Log("Collide");
        }
    }
}
