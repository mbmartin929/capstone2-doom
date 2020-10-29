using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class PistolController : WeaponController
    {
        public Transform bulletCasingLoc;
        public Transform bulletTracerLoc;

        public float maxBulletSpread = 1.0f;
        public float fireTime = 0.5f;
        public float timeToMaxSpread = 2.0f;
        public float fireDelay = 1.0f;
        public bool readyToFire = true;

        public int CurAmmo
        {
            get { return curAmmo; }
            set
            {
                curAmmo = value;
                if (curAmmo < 0)
                {
                    Debug.Log("curAmmo: " + curAmmo);
                    curAmmo = 0;
                }
                if (curAmmo > AmmoInventory.Instance.curPistolAmmo)
                {
                    Debug.Log("AmmoInventory: " + curAmmo);

                    //curAmmo = AmmoInventory.Instance.curPistolAmmo;
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
            //pos = transform.position;

            cameraGo = GameObject.FindGameObjectWithTag("Player");
            FindObjectwithTag("MainCamera");

            FOV = fpsCam.fieldOfView;

            CurAmmo = clipAmmo;
            //CurAmmo = AmmoInventory.Instance.curPistolAmmo;
            //Reload();

            //Debug.Log("CurAmmo: " + CurAmmo + " || " + "ClipAmmo: " + clipAmmo);

            canAttack = true;
            //Debug.Log("PistolController: " + canAttack);

            TextManager.Instance.UpdateAmmoText();

            //InvokeRepeating("RepeatingFix", 0.69f, 0.1f);
        }

        // Update is called once per frame
        void Update()
        {
            fpsCam.transform.eulerAngles += camRotation;
            fpsCam.fieldOfView = FOV;

            //transform.position = new Vector3(transform.position.x, transform.position.y, startPos.z);

            // Debug.Log("startPos: " + startPos);
            // transform.position = startPos;
            // Debug.Log("transform: " + transform.position);
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
        }

        private void RepeatingFix()
        {
            transform.localPosition = startPos;
        }

        private void Reload()
        {
            transform.localPosition = startPos;

            if (curAmmo >= clipAmmo)
            {
                Debug.Log("You have full ammo");
                return;
            }
            else if (AmmoInventory.Instance.curPistolAmmo <= 0)
            {
                Debug.Log("You have no ammo");
                return;
            }
            else if ((clipAmmo - curAmmo) >= AmmoInventory.Instance.curPistolAmmo)
            {
                curAmmo += AmmoInventory.Instance.curPistolAmmo;
                AmmoInventory.Instance.curPistolAmmo = 0;

                anim.SetTrigger("Reload");
                Debug.Log("Decreased Reload");
            }
            else
            {
                AmmoInventory.Instance.curPistolAmmo -= (clipAmmo - curAmmo);
                curAmmo = clipAmmo;

                Debug.Log("Normal Reload");
                anim.SetTrigger("Reload");
            }

            TextManager.Instance.UpdateAmmoText();
        }

        void Shoot()
        {
            transform.localPosition = startPos;

            RaycastHit hit;
            if (CurAmmo <= 0)
            {
                //Debug.Log(curAmmo);
                Debug.Log("No Ammo, please Reload");
                GetComponent<AudioSource>().PlayOneShot(gunshotSounds[1]);
                return;
            }

            Vector3 rotationVector = transform.rotation.eulerAngles;
            GameObject bulletCasingGo = Instantiate(bulletCasingParticleGo, (bulletCasingLoc.position + new Vector3(0f, 0f, 0f)), Quaternion.Euler(new Vector3(0, rotationVector.y + 60.0f, 0)));
            bulletTracerParticle.Play();

            StartCoroutine(Wait(0.2f));
            #region Gun Effects
            anim.SetTrigger("Shoot");
            StartCoroutine("MuzzleLight");
            PlayGunshotSound();
            #endregion

            //ShootDetection(GameManager.Instance.playerGo.transform.position, soundRadius);

            CurAmmo--;

            Vector3 shootDirection = fpsCam.transform.forward;
            shootDirection.x += Random.Range(-spreadFactor, spreadFactor);
            shootDirection.y += Random.Range(-spreadFactor, spreadFactor);

            Vector3 fireDirection = fpsCam.transform.forward;
            Quaternion fireRotation = Quaternion.LookRotation(fireDirection);
            Quaternion randomRotation = Random.rotation;
            float currentSpread = Mathf.Lerp(0.0f, maxBulletSpread, fireTime / timeToMaxSpread);//Bullets first shot is perfect = 0.0f. Every shoot is less accurate
            fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, currentSpread)); //Random rotation of bullet every shoot

            if (Physics.Raycast(fpsCam.transform.position, fireRotation * (Vector3.forward * 100f), out hit, range))
            {
                TextManager.Instance.UpdateAmmoText();
                if (hit.transform.tag == "Level")
                {
                    Debug.Log("Hit Level");

                    // MeshCollider collider = hit.collider as MeshCollider;
                    // // Remember to handle case where collider is null because you hit a non-mesh primitive...

                    // Mesh mesh = collider.sharedMesh;

                    // // There are 3 indices stored per triangle
                    // int limit = hit.triangleIndex * 3;
                    // int submesh;
                    // for (submesh = 0; submesh < mesh.subMeshCount; submesh++)
                    // {
                    //     int numIndices = mesh.GetTriangles(submesh).Length;
                    //     if (numIndices > limit)
                    //         break;

                    //     limit -= numIndices;
                    // }

                    // Material material = collider.GetComponent<MeshRenderer>().sharedMaterials[submesh];

                    //Debug.Log(material.name);

                    Instantiate(hitEffectGo, hit.point, Quaternion.LookRotation(hit.normal));
                    Instantiate(bulletHole, hit.point + 0.01f * hit.normal, Quaternion.LookRotation(hit.normal));
                    Debug.Log("Finish Hit Level");
                }

                // Raycast hits Enemy
                else if (hit.transform.tag == "Enemy")
                {
                    Debug.Log("Hit Enemy");
                    EnemyController enemy = hit.transform.GetComponent<EnemyController>();
                    if (enemy == null) enemy = hit.transform.GetChild(0).GetComponent<EnemyController>();

                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            //GameObject bloodGo = Instantiate(item, hit.point, Quaternion.LookRotation(hit.normal));

                            ParticleSystem particleSys = item.GetComponent<ParticleSystem>();
                            // ParticleSystem.EmissionModule emissionModule = particleSys.emission;

                            // ParticleSystem.Burst burst = emissionModule.GetBurst(0);
                            // burst.minCount = minimumBloodParticles;
                            // burst.maxCount = maximumBloodParticles;
                            //emissionModule.SetBurst(0, burst);
                            // emissionModule.burstCount = maximumBloodParticles;

                            // emissionModule.enabled = true;



                            // item.GetComponent<ParticleSystem>().emission.enabled = true;
                            // item.GetComponent<ParticleSystem>().emission.type = ParticleSystemEmissionType.Time;
                            // item.GetComponent<ParticleSystem>().emission.SetBurst(0, new ParticleSystem.Burst(0.0f, maximumBloodParticles));

                            //particleSys.main.startSpeedMultiplier = maximumBloodParticles;

                            GameObject bloodGo = Instantiate(item, hit.transform.position, Quaternion.LookRotation(hit.normal));
                            //bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            // GameObject bloodGo = Instantiate(item, hit.point /*+ (hit.transform.forward * 1f)*/,
                            //                                  item.transform.rotation);

                            //item.GetComponent<ParticleSystem>().emission.SetBurst(0, new ParticleSystem.Burst(0.0f, maximumBloodParticles));

                            GameObject bloodGo = Instantiate(item, hit.transform.position /*+ (hit.transform.forward * 1f)*/,
                                                            item.transform.rotation);
                            //bloodGo.transform.parent = hit.transform;
                        }
                    }
                    enemy.painStrength = painStrength;
                    enemy.TakeDamage(damage);
                }
                else if (hit.transform.tag == "Destructible")
                {
                    Debug.Log("Hit Destructible");
                    DestructibleDoor door = hit.transform.GetComponent<DestructibleDoor>();

                    door.health -= damage;
                    if (door.health <= 0)
                    {
                        door.DestroyMesh();
                    }
                }
                else if (hit.transform.tag == "Egg")
                {
                    Debug.Log("Hit Egg");
                    hit.transform.GetComponent<EggController>().TakeDamage(damage);
                }
                else
                {
                    Debug.Log("Pistol Hit Raycast Hit Something Else: " + hit.transform.gameObject.name);
                    //Instantiate(hitEffectGo, hit.point, Quaternion.LookRotation(hit.normal));
                }
                //Debug.Log("Hit Name: " + hit.transform.gameObject.name);
                //Debug.Log("Hit Tag: " + hit.transform.gameObject.tag);
            }
            canAttack = false;
            Debug.Log("Finish Shooting");
            //Debug.Log("Before: " + transform.position);
            //readyToFire = false;
            //Invoke("setReadyToFire", fireDelay);
        }

        void setReadyToFire()
        {
            readyToFire = true;
        }

        private IEnumerator Wait(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            canAttack = true;
        }

        public void ReloadSound()
        {
            //GetComponent<AudioSource>().playone
        }
    }
}