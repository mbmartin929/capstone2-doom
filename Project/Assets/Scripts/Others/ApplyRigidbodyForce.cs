using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyRigidbodyForce : MonoBehaviour
{
    public float forwardForce;
    public float upwardForce;

    public Rigidbody rb;

    private bool doneRotating = false;

    public Vector3 destination;
    public float speed;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //rb.AddForce(transform.forward * forwardForce);
        //rb.AddForce(transform.up * upwardForce);
    }

    // Update is called once per frame
    void Update()
    {
        if ((rb.velocity != Vector3.zero) && (!doneRotating))
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }

        //transform.position = Vector3.MoveTowards(transform.position, destination, speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // rb.velocity = Vector3.zero;
        // rb.useGravity = false;
    }

    /// <summary>

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Hi");
        // if ((other.gameObject.tag == "Enemy") || (other.gameObject.tag == "Level")) { }
        // else
        // {
        //     doneRotating = true;
        //     rb.velocity = Vector3.zero;
        // }

        //transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
