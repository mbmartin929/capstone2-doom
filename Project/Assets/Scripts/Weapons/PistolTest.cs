using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolTest : MonoBehaviour
{
    public Camera fpsCam;
    public float range = 100f;

    private GameObject cameraGo;



    private bool canAttack;
    private Animator anim;

    public Vector3 camRotation;
    public float FOV;


    private bool readyToFIre;
    public float fireTime;
    public float maxBulletSpread;
    public float timeToSpread;
    public float timeToMaxSpread;
    public float fireDelay;

    void Awake()
    {
    
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
  
    }

    void Update()
    {
        fpsCam.transform.eulerAngles += camRotation;
        fpsCam.fieldOfView = FOV;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {

    }

    private void Shoot()
    {
        fpsCam.transform.eulerAngles += camRotation;


        RaycastHit hit;
        if (canAttack)
        {
            Vector3 shootDirection = fpsCam.transform.forward;
            Quaternion fireRotation = Quaternion.LookRotation(shootDirection);
            Quaternion randomRotation = Random.rotation;

            float currentSpread = Mathf.Lerp(0.0f, maxBulletSpread, fireTime / timeToMaxSpread); //first shot = perfect, more shots less accurate
            fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, currentSpread)); //random rotation of bullets
            if (Physics.Raycast(fpsCam.transform.position, fireRotation * Vector3.forward, out hit, range))
            {

            }

            readyToFIre = false;       
            Invoke("SetReadytoFire", fireDelay);
        }
    }


    private void SetReadyToFire()
    {
        readyToFIre = true;
    }
}
