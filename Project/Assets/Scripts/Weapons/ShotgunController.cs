using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class ShotgunController : WeaponController
{
    public Transform bulletCasingLoc;
    public int pelletCount = 8;

    public float scaleLimit;
    public float z = 10f;

    public bool readyToFire = true;

    private bool cancelReload = false;

    public int CurAmmo
    {
        get { return curAmmo; }
        set
        {
            curAmmo = value;
            if (curAmmo < 0) curAmmo = 0;
            if (curAmmo > AmmoInventory.Instance.curShotgunAmmo)
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
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(WillCancelReload());
        }
    }

    private IEnumerator WillCancelReload()
    {
        cancelReload = true;
        yield return new WaitForSeconds(0.42f);
        cancelReload = false;
    }

    private void Reload()
    {
        transform.localPosition = startPos;
        if (CurAmmo >= clipAmmo)
        {
            Debug.Log("You have full ammo");
            return;
        }
        else if (AmmoInventory.Instance.curShotgunAmmo <= 0)
        {
            Debug.Log("You have no ammo");
            return;
        }
        else if ((clipAmmo - CurAmmo) >= AmmoInventory.Instance.curShotgunAmmo)
        {
            //Debug.Log("Hi");
            // curAmmo += 1;
            // AmmoInventory.Instance.curShotgunAmmo -= 1;

            // anim.SetTrigger("Reload");
            // Debug.Log("Decreased Reload");

            if (AmmoInventory.Instance.curShotgunAmmo <= 0)
            {
                return;
            }
            else
            {
                Debug.Log("Normal Reload");
                anim.SetTrigger("Reload");
                canAttack = false;
            }
        }
        else
        {
            // AmmoInventory.Instance.curShotgunAmmo -= 1;
            // curAmmo += 1;

            Debug.Log("Normal Reload");
            anim.SetTrigger("Reload");

            canAttack = false;
        }


        TextManager.Instance.UpdateAmmoText();
    }

    private void AddBullet()
    {
        if (CurAmmo >= clipAmmo)
        {
            Debug.Log("You have full ammo");
            return;
        }
        else if (AmmoInventory.Instance.curShotgunAmmo <= 0)
        {
            Debug.Log("You have no ammo");
            cancelReload = true;
            return;
        }
        else
        {
            AmmoInventory.Instance.curShotgunAmmo -= 1;
            CurAmmo += 1;
        }

        TextManager.Instance.UpdateAmmoText();
    }

    public void CheckReload()
    {
        //Debug.Log("Check Reload");
        if (CurAmmo != clipAmmo)
        {
            //Debug.Log("IF Check Reload");

            if (!cancelReload) anim.Play("Reload", 0, 0.0f);

        }
    }

    public void CanAttackState()
    {
        canAttack = true;
    }

    public void CannotAttackState()
    {
        canAttack = false;
    }

    public void ResetTransform()
    {
        //transform.localPosition = new Vector3(transform.localPosition.x, -0.73f, transform.localPosition.z);
        //Debug.Log(transform.localPosition);
    }

    void Shoot()
    {
        //transform.localPosition = startPos;
        //transform.localPosition = startWeaponSwitchVector;

        RaycastHit hit;
        if (CurAmmo <= 0)
        {
            //Debug.Log(curAmmo);
            GetComponent<AudioSource>().PlayOneShot(gunshotSounds[1]);
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
        Vector3 rotationVector = transform.rotation.eulerAngles;
        GameObject bulletCasingGo = Instantiate(bulletCasingParticleGo, (bulletCasingLoc.position + new Vector3(0f, 0f, 0f)), Quaternion.Euler(new Vector3(0, rotationVector.y + 60.0f, 0)));

        TextManager.Instance.UpdateAmmoText();

        ShootDetection(GameManager.Instance.playerGo.transform.position, soundRadius);

        for (int i = 0; i < pelletCount; ++i)
        {
            bulletTracerParticle.Play();

            Vector3 direction = Random.insideUnitCircle * scaleLimit; //Random XY point inside a circle
            direction.z = z;// circle is at Z units 
            direction = transform.TransformDirection(direction.normalized); //converting the Vector3.forward to transform.forward
            Ray ray = new Ray(fpsCam.transform.position, direction);

            if (Physics.Raycast(ray, out hit, range))
            {
                Debug.DrawRay(fpsCam.transform.position, direction * range, Color.red);

                if (hit.transform.tag == "Level")
                {
                    MeshCollider collider = hit.collider as MeshCollider;
                    // Remember to handle case where collider is null because you hit a non-mesh primitive...

                    Mesh mesh = collider.sharedMesh;

                    // There are 3 indices stored per triangle
                    int limit = hit.triangleIndex * 3;
                    int submesh;
                    for (submesh = 0; submesh < mesh.subMeshCount; submesh++)
                    {
                        int numIndices = mesh.GetTriangles(submesh).Length;
                        if (numIndices > limit)
                            break;

                        limit -= numIndices;
                    }

                    Material material = collider.GetComponent<MeshRenderer>().sharedMaterials[submesh];

                    Instantiate(hitEffectGo, hit.point, Quaternion.LookRotation(hit.normal));
                    Instantiate(bulletHole, hit.point + 0.01f * hit.normal, Quaternion.LookRotation(hit.normal));
                }

                // Raycast hits Enemy
                else if (hit.transform.tag == "Enemy")
                {
                    EnemyController enemy = hit.transform.GetComponent<EnemyController>();

                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            GameObject bloodGo = Instantiate(item, hit.point, Quaternion.LookRotation(hit.normal));
                            //bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            GameObject bloodGo = Instantiate(item, hit.point /*+ (hit.transform.forward * 1f)*/,
                                                             item.transform.rotation);
                            //bloodGo.transform.parent = hit.transform;
                        }
                    }
                    enemy.TakeDamage(10);
                }
                else if (hit.transform.tag == "Destructible")
                {
                    DestructibleDoor door = hit.transform.GetComponent<DestructibleDoor>();

                    door.health -= damage;
                    if (door.health <= 0)
                    {
                        door.DestroyMesh();
                    }
                }
                else
                {
                    //Debug.Log(hit.transform.gameObject.name);
                    //Instantiate(hitEffectGo, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
        }
        canAttack = false;

        readyToFire = false;
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canAttack = true;
    }
}
