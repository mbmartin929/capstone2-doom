using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class LauncherController : WeaponController
{
    public GameObject projectileGo;

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


    public void CanAttackState()
    {
        canAttack = true;
    }

    void Shoot()
    {
        if (CurAmmo <= 0)
        {
            //Debug.Log("CurAmmo <= 0");
            //GetComponent<AudioSource>().PlayOneShot(gunshotSounds[1]);
            return;
        }



        int shootLayerIndex = anim.GetLayerIndex("Shoot");
        int reloadLayerIndex = anim.GetLayerIndex("Reload");
        if (anim.GetCurrentAnimatorStateInfo(shootLayerIndex).IsName("Shoot"))
        {
            Debug.Log("Playing Shoot");
            return;
        }
        if (anim.GetCurrentAnimatorStateInfo(reloadLayerIndex).IsName("Reload"))
        {
            Debug.Log("Playing Reload");
            return;
        }

        StartCoroutine(Wait(2.42f));

        CurAmmo--;

        GameObject _projectileGo = Instantiate(projectileGo, (transform.position + transform.forward), Quaternion.identity);
        _projectileGo.GetComponent<Projectile>().LaunchLauncherProjectile(transform.forward * 2.29f + transform.up * 1f);

        anim.SetTrigger("Shoot");
        StartCoroutine("MuzzleLight");

        TextManager.Instance.UpdateAmmoText();

        //ShootDetection(GameManager.Instance.playerGo.transform.position, soundRadius);

        canAttack = false;

        readyToFire = false;
    }

    private void AddBullet()
    {
        AmmoInventory.Instance.curLauncherAmmo -= 1;
        CurAmmo += 1;

        TextManager.Instance.UpdateAmmoText();
    }

    private void AmmoCheck()
    {
        if (AmmoInventory.Instance.curLauncherAmmo <= 0)
        {
            Debug.Log("Idle after Shoot");
            anim.SetTrigger("Idle");
        }
        else
        {
            Debug.Log("Reload after Shoot");
            anim.SetTrigger("Reload");
        }
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canAttack = true;
    }
}
