using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsScript : MonoBehaviour
{
    public Camera fpsCam;
    private GameObject cameraGo;
    private bool canAttack;
    private Animator anim;
    public Vector3 camRotation;
    public float FOV;


    [Header("Gun Settings")]
    public float maxBulletSpread;
    public float timeToMaxSpread;
    public float fireDelay;
    public float fireTime;
    public float range = 100f;

    public bool readyToFire = true;
    //public float spreadFactor = 0.1f;

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
        Vector3 fireDirection = fpsCam.transform.forward;
        Quaternion fireRotation = Quaternion.LookRotation(fireDirection);
        Quaternion randomRotation = Random.rotation;
        float currentSpread = Mathf.Lerp(0.0f, maxBulletSpread, fireTime / timeToMaxSpread);//Bullets first shot is perfect = 0.0f. Every shoot is less accurate
        fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, currentSpread)); //Random rotation of bullet every shoot
        RaycastHit hit;
        if (canAttack)
        {         
            if (Physics.Raycast(fpsCam.transform.position, fireRotation * Vector3.forward, out hit, range))
            {

            }

            readyToFire = false;
            Invoke("setReadyToFire",fireDelay);
        }
    }


    void setReadyToFire()
    {
        readyToFire = true;
    }
}
