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

    //public bool readyToFire = true;

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
        fpsCam = Camera.main;

        fpsCam.transform.eulerAngles += camRotation;
        fpsCam.fieldOfView = FOV;
    }

    void LateUpdate()
    {
        if ((Input.GetMouseButtonDown(0)) && (canAttack))
        {
            Shoot();
            //StartCoroutine(Shoot());
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
        //transform.localPosition = startPos;

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

    private void Shoot()
    {
        if (PauseManager.Instance.pressEscape) { return; }

        if (CurAmmo <= 0)
        {
            Debug.Log("No Ammo, please Reload");
            GetComponent<AudioSource>().PlayOneShot(gunshotSounds[1]);
            return;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            Debug.Log("Playing Shoot");
            return;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            Debug.Log("Playing Reload");
            return;
        }

        transform.localPosition = startPos;
        transform.localRotation = Quaternion.identity;

        if (CheatsManager.Instance.enableUnlimitedAmmo) { Debug.Log("Unlimited Ammo"); }
        else CurAmmo--;

        anim.SetTrigger("Shoot");
        //StartCoroutine("MuzzleLight");
        StartCoroutine(Wait(0.2f));
        Vector3 rotationVector = transform.rotation.eulerAngles;
        //GameObject bulletCasingGo = Instantiate(bulletCasingParticleGo, (bulletCasingLoc.position + new Vector3(0f, 0f, 0f)), Quaternion.Euler(new Vector3(0, rotationVector.y + 60.0f, 0)));

        TextManager.Instance.UpdateAmmoText();

        SingleRaycasts();

        //ShootDetection(GameManager.Instance.playerGo.transform.position, soundRadius);

        // for (int i = 0; i < pelletCount; ++i)
        // {
        //     bulletTracerParticle.Play();

        //     Vector3 direction = Random.insideUnitCircle * scaleLimit; //Random XY point inside a circle
        //     direction.z = z;// circle is at Z units 
        //     direction = transform.TransformDirection(direction.normalized); //converting the Vector3.forward to transform.forward
        //     Ray ray = new Ray(fpsCam.transform.position, direction);

        //     RaycastHit hit = new RaycastHit();
        //     if (Physics.Raycast(ray, out hit, range))
        //     {
        //         Debug.DrawRay(fpsCam.transform.position, direction * range, Color.red);

        //         switch (hit.transform.tag)
        //         {
        //             case "Level":
        //                 Instantiate(hitEffectGo, hit.point, Quaternion.LookRotation(hit.normal));
        //                 Instantiate(bulletHole, hit.point + 0.01f * hit.normal, Quaternion.LookRotation(hit.normal));
        //                 Debug.Log("Finish Hit Level");
        //                 break;

        //             case "Enemy":
        //                 Debug.Log("Hit Enemy");
        //                 EnemyController enemy = hit.transform.GetComponent<EnemyController>();

        //                 foreach (GameObject item in enemy.bloodSplashGos)
        //                 {
        //                     if (item.tag == "Hit Normal")
        //                     {
        //                         GameObject bloodGo = Instantiate(item, hit.point, Quaternion.LookRotation(hit.normal));
        //                         //bloodGo.transform.parent = hit.transform;
        //                     }
        //                     else
        //                     {
        //                         GameObject bloodGo = Instantiate(item, hit.point /*+ (hit.transform.forward * 1f)*/,
        //                                                          item.transform.rotation);
        //                         //bloodGo.transform.parent = hit.transform;
        //                     }
        //                 }
        //                 enemy.TakeDamage(damage);
        //                 break;

        //             case "Destructible":
        //                 Debug.Log("Hit Destructible");
        //                 DestructibleDoor door = hit.transform.GetComponent<DestructibleDoor>();

        //                 door.health -= damage;
        //                 if (door.health <= 0)
        //                 {
        //                     door.DestroyMesh();
        //                 }
        //                 break;

        //             case "Egg":
        //                 Debug.Log("Hit Egg");
        //                 hit.transform.GetComponent<EggController>().TakeDamage(damage);
        //                 break;

        //             case "Resource Block":
        //                 Debug.Log("Hit Resource Block");
        //                 ResourceBlock block = hit.transform.GetComponent<ResourceBlock>();

        //                 foreach (GameObject item in block.crystalEffects)
        //                 {
        //                     if (item.tag == "Hit Normal")
        //                     {
        //                         ParticleSystem particleSys = item.GetComponent<ParticleSystem>();

        //                         GameObject crystalHitGo = Instantiate(item, hit.transform.position, Quaternion.LookRotation(hit.normal));
        //                     }
        //                     else
        //                     {
        //                         GameObject crystalHitGo = Instantiate(item, hit.transform.position, item.transform.rotation);
        //                     }
        //                 }
        //                 hit.transform.GetComponent<ResourceBlock>().TakeDamage(damage);
        //                 break;

        //             default:
        //                 Debug.Log("Shotgun Hit Raycast Hit Something Else Name: " + hit.transform.gameObject.name);
        //                 Debug.Log("Shotgun Hit Raycast Hit Something Else Tag: " + hit.transform.gameObject.tag);
        //                 break;
        //         }
        //     }
        // }
        canAttack = false;
        //readyToFire = false;
        Debug.Log("Finish Shooting");
    }

    public void SingleRaycasts()
    {
        bulletTracerParticle.Play();

        Vector3 direction1 = Random.insideUnitCircle * scaleLimit; //Random XY point inside a circle
        direction1.z = z;// circle is at Z units 
        direction1 = transform.TransformDirection(direction1.normalized); //converting the Vector3.forward to transform.forward
        Ray ray1 = new Ray(fpsCam.transform.position, direction1);

        RaycastHit hit1 = new RaycastHit();
        if (Physics.Raycast(ray1, out hit1, range))
        {
            Debug.DrawRay(fpsCam.transform.position, direction1 * range, Color.red);

            switch (hit1.transform.tag)
            {
                case "Level":
                    Instantiate(hitEffectGo, hit1.point, Quaternion.LookRotation(hit1.normal));
                    Instantiate(bulletHole, hit1.point + 0.01f * hit1.normal, Quaternion.LookRotation(hit1.normal));
                    Debug.Log("Finish Hit Level");
                    break;

                case "Enemy":
                    Debug.Log("Hit Enemy");
                    EnemyController enemy = hit1.transform.GetComponent<EnemyController>();

                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            GameObject bloodGo = Instantiate(item, hit1.point, Quaternion.LookRotation(hit1.normal));
                            //bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            GameObject bloodGo = Instantiate(item, hit1.point /*+ (hit.transform.forward * 1f)*/,
                                                             item.transform.rotation);
                            //bloodGo.transform.parent = hit.transform;
                        }
                    }
                    enemy.TakeDamage(damage);
                    break;

                case "Destructible":
                    Debug.Log("Hit Destructible");
                    DestructibleDoor door = hit1.transform.GetComponent<DestructibleDoor>();

                    door.health -= damage;
                    if (door.health <= 0)
                    {
                        door.DestroyMesh();
                    }
                    break;

                case "Egg":
                    Debug.Log("Hit Egg");
                    hit1.transform.GetComponent<EggController>().TakeDamage(damage);
                    break;

                case "Resource Block":
                    Debug.Log("Hit Resource Block");
                    ResourceBlock block = hit1.transform.GetComponent<ResourceBlock>();

                    foreach (GameObject item in block.crystalEffects)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            ParticleSystem particleSys = item.GetComponent<ParticleSystem>();

                            GameObject crystalHitGo = Instantiate(item, hit1.transform.position, Quaternion.LookRotation(hit1.normal));
                        }
                        else
                        {
                            GameObject crystalHitGo = Instantiate(item, hit1.transform.position, item.transform.rotation);
                        }
                    }
                    hit1.transform.GetComponent<ResourceBlock>().TakeDamage(damage);
                    break;

                default:
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Name: " + hit1.transform.gameObject.name);
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Tag: " + hit1.transform.gameObject.tag);
                    break;
            }
        }

        bulletTracerParticle.Play();

        Vector3 direction2 = Random.insideUnitCircle * scaleLimit; //Random XY point inside a circle
        direction2.z = z;// circle is at Z units 
        direction2 = transform.TransformDirection(direction2.normalized); //converting the Vector3.forward to transform.forward
        Ray ray2 = new Ray(fpsCam.transform.position, direction2);

        RaycastHit hit2 = new RaycastHit();
        if (Physics.Raycast(ray2, out hit2, range))
        {
            Debug.DrawRay(fpsCam.transform.position, direction2 * range, Color.red);

            switch (hit2.transform.tag)
            {
                case "Level":
                    Instantiate(hitEffectGo, hit2.point, Quaternion.LookRotation(hit2.normal));
                    Instantiate(bulletHole, hit2.point + 0.01f * hit2.normal, Quaternion.LookRotation(hit2.normal));
                    Debug.Log("Finish Hit Level");
                    break;

                case "Enemy":
                    Debug.Log("Hit Enemy");
                    EnemyController enemy = hit2.transform.GetComponent<EnemyController>();

                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            GameObject bloodGo = Instantiate(item, hit2.point, Quaternion.LookRotation(hit2.normal));
                            //bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            GameObject bloodGo = Instantiate(item, hit2.point /*+ (hit.transform.forward * 1f)*/,
                                                             item.transform.rotation);
                            //bloodGo.transform.parent = hit.traccnsform;
                        }
                    }
                    enemy.TakeDamage(damage);
                    break;

                case "Destructible":
                    Debug.Log("Hit Destructible");
                    DestructibleDoor door = hit2.transform.GetComponent<DestructibleDoor>();

                    door.health -= damage;
                    if (door.health <= 0)
                    {
                        door.DestroyMesh();
                    }
                    break;

                case "Egg":
                    Debug.Log("Hit Egg");
                    hit2.transform.GetComponent<EggController>().TakeDamage(damage);
                    break;

                case "Resource Block":
                    Debug.Log("Hit Resource Block");
                    ResourceBlock block = hit2.transform.GetComponent<ResourceBlock>();

                    foreach (GameObject item in block.crystalEffects)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            ParticleSystem particleSys = item.GetComponent<ParticleSystem>();

                            GameObject crystalHitGo = Instantiate(item, hit2.transform.position, Quaternion.LookRotation(hit2.normal));
                        }
                        else
                        {
                            GameObject crystalHitGo = Instantiate(item, hit2.transform.position, item.transform.rotation);
                        }
                    }
                    hit2.transform.GetComponent<ResourceBlock>().TakeDamage(damage);
                    break;

                default:
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Name: " + hit2.transform.gameObject.name);
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Tag: " + hit2.transform.gameObject.tag);
                    break;
            }
        }

        bulletTracerParticle.Play();

        Vector3 direction3 = Random.insideUnitCircle * scaleLimit; //Random XY point inside a circle
        direction3.z = z;// circle is at Z units 
        direction3 = transform.TransformDirection(direction3.normalized); //converting the Vector3.forward to transform.forward
        Ray ray3 = new Ray(fpsCam.transform.position, direction3);

        RaycastHit hit3 = new RaycastHit();
        if (Physics.Raycast(ray3, out hit3, range))
        {
            Debug.DrawRay(fpsCam.transform.position, direction3 * range, Color.red);

            switch (hit3.transform.tag)
            {
                case "Level":
                    Instantiate(hitEffectGo, hit3.point, Quaternion.LookRotation(hit3.normal));
                    Instantiate(bulletHole, hit3.point + 0.01f * hit3.normal, Quaternion.LookRotation(hit3.normal));
                    Debug.Log("Finish Hit Level");
                    break;

                case "Enemy":
                    Debug.Log("Hit Enemy");
                    EnemyController enemy = hit3.transform.GetComponent<EnemyController>();

                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            GameObject bloodGo = Instantiate(item, hit3.point, Quaternion.LookRotation(hit3.normal));
                            //bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            GameObject bloodGo = Instantiate(item, hit3.point /*+ (hit.transform.forward * 1f)*/,
                                                             item.transform.rotation);
                            //bloodGo.transform.parent = hit.transform;
                        }
                    }
                    enemy.TakeDamage(damage);
                    break;

                case "Destructible":
                    Debug.Log("Hit Destructible");
                    DestructibleDoor door = hit3.transform.GetComponent<DestructibleDoor>();

                    door.health -= damage;
                    if (door.health <= 0)
                    {
                        door.DestroyMesh();
                    }
                    break;

                case "Egg":
                    Debug.Log("Hit Egg");
                    hit3.transform.GetComponent<EggController>().TakeDamage(damage);
                    break;

                case "Resource Block":
                    Debug.Log("Hit Resource Block");
                    ResourceBlock block = hit3.transform.GetComponent<ResourceBlock>();

                    foreach (GameObject item in block.crystalEffects)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            ParticleSystem particleSys = item.GetComponent<ParticleSystem>();

                            GameObject crystalHitGo = Instantiate(item, hit3.transform.position, Quaternion.LookRotation(hit3.normal));
                        }
                        else
                        {
                            GameObject crystalHitGo = Instantiate(item, hit3.transform.position, item.transform.rotation);
                        }
                    }
                    hit3.transform.GetComponent<ResourceBlock>().TakeDamage(damage);
                    break;

                default:
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Name: " + hit3.transform.gameObject.name);
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Tag: " + hit3.transform.gameObject.tag);
                    break;
            }
        }

        bulletTracerParticle.Play();

        Vector3 direction4 = Random.insideUnitCircle * scaleLimit; //Random XY point inside a circle
        direction4.z = z;// circle is at Z units 
        direction4 = transform.TransformDirection(direction4.normalized); //converting the Vector3.forward to transform.forward
        Ray ray4 = new Ray(fpsCam.transform.position, direction4);

        RaycastHit hit4 = new RaycastHit();
        if (Physics.Raycast(ray4, out hit4, range))
        {
            Debug.DrawRay(fpsCam.transform.position, direction4 * range, Color.red);

            switch (hit4.transform.tag)
            {
                case "Level":
                    Instantiate(hitEffectGo, hit4.point, Quaternion.LookRotation(hit4.normal));
                    Instantiate(bulletHole, hit4.point + 0.01f * hit4.normal, Quaternion.LookRotation(hit4.normal));
                    Debug.Log("Finish Hit Level");
                    break;

                case "Enemy":
                    Debug.Log("Hit Enemy");
                    EnemyController enemy = hit4.transform.GetComponent<EnemyController>();

                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            GameObject bloodGo = Instantiate(item, hit4.point, Quaternion.LookRotation(hit4.normal));
                            //bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            GameObject bloodGo = Instantiate(item, hit4.point /*+ (hit.transform.forward * 1f)*/,
                                                             item.transform.rotation);
                            //bloodGo.transform.parent = hit.transform;
                        }
                    }
                    enemy.TakeDamage(damage);
                    break;

                case "Destructible":
                    Debug.Log("Hit Destructible");
                    DestructibleDoor door = hit4.transform.GetComponent<DestructibleDoor>();

                    door.health -= damage;
                    if (door.health <= 0)
                    {
                        door.DestroyMesh();
                    }
                    break;

                case "Egg":
                    Debug.Log("Hit Egg");
                    hit4.transform.GetComponent<EggController>().TakeDamage(damage);
                    break;

                case "Resource Block":
                    Debug.Log("Hit Resource Block");
                    ResourceBlock block = hit4.transform.GetComponent<ResourceBlock>();

                    foreach (GameObject item in block.crystalEffects)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            ParticleSystem particleSys = item.GetComponent<ParticleSystem>();

                            GameObject crystalHitGo = Instantiate(item, hit4.transform.position, Quaternion.LookRotation(hit4.normal));
                        }
                        else
                        {
                            GameObject crystalHitGo = Instantiate(item, hit4.transform.position, item.transform.rotation);
                        }
                    }
                    hit4.transform.GetComponent<ResourceBlock>().TakeDamage(damage);
                    break;

                default:
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Name: " + hit4.transform.gameObject.name);
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Tag: " + hit4.transform.gameObject.tag);
                    break;
            }
        }

        bulletTracerParticle.Play();

        Vector3 direction5 = Random.insideUnitCircle * scaleLimit; //Random XY point inside a circle
        direction5.z = z;// circle is at Z units 
        direction5 = transform.TransformDirection(direction5.normalized); //converting the Vector3.forward to transform.forward
        Ray ray5 = new Ray(fpsCam.transform.position, direction5);

        RaycastHit hit5 = new RaycastHit();
        if (Physics.Raycast(ray5, out hit5, range))
        {
            Debug.DrawRay(fpsCam.transform.position, direction5 * range, Color.red);

            switch (hit5.transform.tag)
            {
                case "Level":
                    Instantiate(hitEffectGo, hit5.point, Quaternion.LookRotation(hit5.normal));
                    Instantiate(bulletHole, hit5.point + 0.01f * hit5.normal, Quaternion.LookRotation(hit5.normal));
                    Debug.Log("Finish Hit Level");
                    break;

                case "Enemy":
                    Debug.Log("Hit Enemy");
                    EnemyController enemy = hit5.transform.GetComponent<EnemyController>();

                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            GameObject bloodGo = Instantiate(item, hit5.point, Quaternion.LookRotation(hit5.normal));
                            //bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            GameObject bloodGo = Instantiate(item, hit5.point /*+ (hit.transform.forward * 1f)*/,
                                                             item.transform.rotation);
                            //bloodGo.transform.parent = hit.transform;
                        }
                    }
                    enemy.TakeDamage(damage);
                    break;

                case "Destructible":
                    Debug.Log("Hit Destructible");
                    DestructibleDoor door = hit5.transform.GetComponent<DestructibleDoor>();

                    door.health -= damage;
                    if (door.health <= 0)
                    {
                        door.DestroyMesh();
                    }
                    break;

                case "Egg":
                    Debug.Log("Hit Egg");
                    hit5.transform.GetComponent<EggController>().TakeDamage(damage);
                    break;

                case "Resource Block":
                    Debug.Log("Hit Resource Block");
                    ResourceBlock block = hit5.transform.GetComponent<ResourceBlock>();

                    foreach (GameObject item in block.crystalEffects)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            ParticleSystem particleSys = item.GetComponent<ParticleSystem>();

                            GameObject crystalHitGo = Instantiate(item, hit5.transform.position, Quaternion.LookRotation(hit5.normal));
                        }
                        else
                        {
                            GameObject crystalHitGo = Instantiate(item, hit5.transform.position, item.transform.rotation);
                        }
                    }
                    hit5.transform.GetComponent<ResourceBlock>().TakeDamage(damage);
                    break;

                default:
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Name: " + hit5.transform.gameObject.name);
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Tag: " + hit5.transform.gameObject.tag);
                    break;
            }
        }

        bulletTracerParticle.Play();

        Vector3 direction6 = Random.insideUnitCircle * scaleLimit; //Random XY point inside a circle
        direction6.z = z;// circle is at Z units 
        direction6 = transform.TransformDirection(direction6.normalized); //converting the Vector3.forward to transform.forward
        Ray ray6 = new Ray(fpsCam.transform.position, direction6);

        RaycastHit hit6 = new RaycastHit();
        if (Physics.Raycast(ray6, out hit6, range))
        {
            Debug.DrawRay(fpsCam.transform.position, direction6 * range, Color.red);

            switch (hit6.transform.tag)
            {
                case "Level":
                    Instantiate(hitEffectGo, hit6.point, Quaternion.LookRotation(hit6.normal));
                    Instantiate(bulletHole, hit6.point + 0.01f * hit6.normal, Quaternion.LookRotation(hit6.normal));
                    Debug.Log("Finish Hit Level");
                    break;

                case "Enemy":
                    Debug.Log("Hit Enemy");
                    EnemyController enemy = hit6.transform.GetComponent<EnemyController>();

                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            GameObject bloodGo = Instantiate(item, hit6.point, Quaternion.LookRotation(hit6.normal));
                            //bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            GameObject bloodGo = Instantiate(item, hit6.point /*+ (hit.transform.forward * 1f)*/,
                                                             item.transform.rotation);
                            //bloodGo.transform.parent = hit.transform;
                        }
                    }
                    enemy.TakeDamage(damage);
                    break;

                case "Destructible":
                    Debug.Log("Hit Destructible");
                    DestructibleDoor door = hit6.transform.GetComponent<DestructibleDoor>();

                    door.health -= damage;
                    if (door.health <= 0)
                    {
                        door.DestroyMesh();
                    }
                    break;

                case "Egg":
                    Debug.Log("Hit Egg");
                    hit6.transform.GetComponent<EggController>().TakeDamage(damage);
                    break;

                case "Resource Block":
                    Debug.Log("Hit Resource Block");
                    ResourceBlock block = hit6.transform.GetComponent<ResourceBlock>();

                    foreach (GameObject item in block.crystalEffects)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            ParticleSystem particleSys = item.GetComponent<ParticleSystem>();

                            GameObject crystalHitGo = Instantiate(item, hit6.transform.position, Quaternion.LookRotation(hit6.normal));
                        }
                        else
                        {
                            GameObject crystalHitGo = Instantiate(item, hit6.transform.position, item.transform.rotation);
                        }
                    }
                    hit6.transform.GetComponent<ResourceBlock>().TakeDamage(damage);
                    break;

                default:
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Name: " + hit6.transform.gameObject.name);
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Tag: " + hit6.transform.gameObject.tag);
                    break;
            }
        }

        bulletTracerParticle.Play();

        Vector3 direction7 = Random.insideUnitCircle * scaleLimit; //Random XY point inside a circle
        direction7.z = z;// circle is at Z units 
        direction7 = transform.TransformDirection(direction7.normalized); //converting the Vector3.forward to transform.forward
        Ray ray7 = new Ray(fpsCam.transform.position, direction7);

        RaycastHit hit7 = new RaycastHit();
        if (Physics.Raycast(ray7, out hit7, range))
        {
            Debug.DrawRay(fpsCam.transform.position, direction7 * range, Color.red);

            switch (hit7.transform.tag)
            {
                case "Level":
                    Instantiate(hitEffectGo, hit7.point, Quaternion.LookRotation(hit7.normal));
                    Instantiate(bulletHole, hit7.point + 0.01f * hit7.normal, Quaternion.LookRotation(hit7.normal));
                    Debug.Log("Finish Hit Level");
                    break;

                case "Enemy":
                    Debug.Log("Hit Enemy");
                    EnemyController enemy = hit7.transform.GetComponent<EnemyController>();

                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            GameObject bloodGo = Instantiate(item, hit7.point, Quaternion.LookRotation(hit7.normal));
                            //bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            GameObject bloodGo = Instantiate(item, hit7.point /*+ (hit.transform.forward * 1f)*/,
                                                             item.transform.rotation);
                            //bloodGo.transform.parent = hit.transform;
                        }
                    }
                    enemy.TakeDamage(damage);
                    break;

                case "Destructible":
                    Debug.Log("Hit Destructible");
                    DestructibleDoor door = hit7.transform.GetComponent<DestructibleDoor>();

                    door.health -= damage;
                    if (door.health <= 0)
                    {
                        door.DestroyMesh();
                    }
                    break;

                case "Egg":
                    Debug.Log("Hit Egg");
                    hit7.transform.GetComponent<EggController>().TakeDamage(damage);
                    break;

                case "Resource Block":
                    Debug.Log("Hit Resource Block");
                    ResourceBlock block = hit7.transform.GetComponent<ResourceBlock>();

                    foreach (GameObject item in block.crystalEffects)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            ParticleSystem particleSys = item.GetComponent<ParticleSystem>();

                            GameObject crystalHitGo = Instantiate(item, hit7.transform.position, Quaternion.LookRotation(hit7.normal));
                        }
                        else
                        {
                            GameObject crystalHitGo = Instantiate(item, hit7.transform.position, item.transform.rotation);
                        }
                    }
                    hit7.transform.GetComponent<ResourceBlock>().TakeDamage(damage);
                    break;

                default:
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Name: " + hit7.transform.gameObject.name);
                    Debug.Log("Shotgun Hit Raycast Hit Something Else Tag: " + hit7.transform.gameObject.tag);
                    break;
            }
        }
    }

    private IEnumerator Wait(float seconds)
    {
        //yield return new WaitForSeconds(seconds);
        yield return new WaitForFixedUpdate();
        canAttack = true;
    }
}
