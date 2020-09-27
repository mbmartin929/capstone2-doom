using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGibs : MonoBehaviour
{
    public float minY = 420f;
    public float maxY = 960;

    public float minX = 240;
    public float maxX = 420;

    public float minZ = 240;
    public float maxZ = 420;

    public Material[] gibMats;
    public AudioClip[] gibSounds;
    public float minTime = 0.1f;
    public float maxTime = 0.5f;

    private Vector3 velocity;
    private Rigidbody rb;


    private bool startTransparency = false;

    private float lerpStart;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GetComponent<MeshRenderer>().material = gibMats[Random.Range(0, gibMats.Length)];
        audioSource = GetComponent<AudioSource>();

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

            //Debug.Log(gameObject.name);

            #region Transparency
            // float progress = Time.time - lerpStart;
            // Color tempcolor = gameObject.GetComponent<MeshRenderer>().material.color;
            // tempcolor.a = Mathf.Lerp(1.0f, 0.0f, progress / 1.0f);
            // GetComponent<MeshRenderer>().material.SetColor("_BaseColor", tempcolor);
            #endregion

        }
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

    private void PlayGibSound()
    {
        int random = Random.Range(0, gibSounds.Length);

        audioSource.pitch = Random.Range(0.8f, 0.9f);

        audioSource.PlayOneShot(gibSounds[random]);
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
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            GetComponent<BoxCollider>().enabled = false;

            //PlayGibSound();
            float time = Random.Range(minTime, maxTime);
            Invoke("PlayGibSound", time);

            RaycastHit hit;
            int layerMask = LayerMask.GetMask("Ground");

            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 50f, layerMask))
            {
                //Debug.Log("Gib Paint");

                int randomBloodNumber = Random.Range(1, 5);

                //Debug.Log("Gib Blood Paint");
                //Debug.Log(hit.transform.gameObject.name);

                StartCoroutine(GetComponent<DecalPainter>().Paint(hit.point + hit.normal * 1f, 1, 1.0f, 0));
            }

            // var dir = transform.TransformDirection(Random.onUnitSphere * 5f);

            // if (Physics.Raycast(transform.position, dir, out hit, 50f, layerMask))
            // {
            //     //Debug.Log("Gib Paint");

            //     int randomBloodNumber = Random.Range(1, 5);

            //     //Debug.Log("Gib Blood Paint");
            //     //Debug.Log(hit.transform.gameObject.name);

            //     StartCoroutine(GetComponent<DecalPainter>().Paint(hit.point + hit.normal * 1f, 1, 1.0f, 0));
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
