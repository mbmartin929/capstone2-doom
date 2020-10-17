using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class LauncherController : WeaponController
{
    public bool readyToFire = true;

    private bool cancelReload = false;

    public int CurAmmo
    {
        get { return curAmmo; }
        set
        {
            curAmmo = value;
            if (curAmmo < 0) curAmmo = 0;
            if (curAmmo > AmmoInventory.Instance.curLauncherAmmo)
            {
                //curAmmo = AmmoInventory.Instance.curShotgunAmmo;
            }
        }
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraGo = GameObject.FindGameObjectWithTag("Player");
        FindObjectwithTag("MainCamera");

        FOV = fpsCam.fieldOfView;

        CurAmmo = clipAmmo;
        //CurAmmo = AmmoInventory.Instance.curShotgunAmmo;
        //Reload();

        canAttack = true;
        TextManager.Instance.UpdateAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        fpsCam.transform.eulerAngles += camRotation;
        fpsCam.fieldOfView = FOV;
    }

    void LateUpdate()
    {
        if ((Input.GetMouseButtonDown(0)) && (canAttack))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (CurAmmo <= 0)
        {
            Debug.Log("CurAmmo <= 0");
            //GetComponent<AudioSource>().PlayOneShot(gunshotSounds[1]);
            return;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            //Debug.Log("Playing Shoot");
            return;
        }

        CurAmmo--;

        anim.SetTrigger("Shoot");
        StartCoroutine("MuzzleLight");
        StartCoroutine(Wait(0.2f));

        TextManager.Instance.UpdateAmmoText();

        ShootDetection(GameManager.Instance.playerGo.transform.position, soundRadius);

        bulletTracerParticle.Play();

        canAttack = false;

        readyToFire = false;
    }
}
