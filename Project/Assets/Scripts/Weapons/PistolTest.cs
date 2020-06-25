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


    public float spreadFactor = 0.1f;

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
            shootDirection.x += Random.Range(-spreadFactor, spreadFactor);
            shootDirection.y += Random.Range(-spreadFactor, spreadFactor);

            if (Physics.Raycast(fpsCam.transform.position, shootDirection, out hit, range))
            {

            }
        }
    }

}
