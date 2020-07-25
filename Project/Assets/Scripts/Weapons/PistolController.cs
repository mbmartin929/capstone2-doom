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
            //Debug.Log("CurAmmo: " + CurAmmo + " || " + "ClipAmmo: " + clipAmmo);

            canAttack = true;
            //Debug.Log("PistolController: " + canAttack);

            TextManager.Instance.UpdateAmmoText();
        }

        // Update is called once per frame
        void Update()
        {
            fpsCam.transform.eulerAngles += camRotation;
            fpsCam.fieldOfView = FOV;

            // int w = anim.GetCurrentAnimatorClipInfo(0).Length;
            // string[] clipName = new string[w];
            // for (int i = 0; i < w; i += 1)
            // {
            //     clipName[i] = anim.GetCurrentAnimatorClipInfo(0)[i].clip.name;
            //     Debug.Log(clipName[i]);
            // }

            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                anim.SetTrigger("Idle");
            }
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                anim.SetTrigger("Shoot");
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                anim.SetTrigger("Reload");
            }
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

        void Shoot()
        {
            RaycastHit hit;
            if (CurAmmo <= 0)
            {
                //Debug.Log(curAmmo);
                GetComponent<AudioSource>().PlayOneShot(gunshotSounds[1]);
                return;
            }

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
            {
                return;
                //Debug.Log("Playing Shoot");
            }

            Vector3 rotationVector = transform.rotation.eulerAngles;
            GameObject bulletCasingGo = Instantiate(bulletCasingParticleGo, (bulletCasingLoc.position + new Vector3(0f, 0f, 0f)), Quaternion.Euler(new Vector3(0, rotationVector.y + 60.0f, 0)));
            //GameObject tracerGo = Instantiate(bulletTracerGo, (bulletTracerLoc.position + new Vector3(0f, 0f, 0f)), Quaternion.Euler(new Vector3(0, rotationVector.y + 0.0f, 0)));
            //GameObject tracerGo = Instantiate(bulletTracerGo, transform.position, Quaternion.Euler(new Vector3(rotationVector.x, rotationVector.y + 20.0f, rotationVector.z)));
            //bulletCasingGo.transform.parent = this.transform;
            bulletTracerParticle.Play();

            StartCoroutine(Wait(0.2f));
            #region Gun Effects
            anim.SetTrigger("Shoot");

            StartCoroutine("MuzzleLight");
            PlayGunshotSound();
            #endregion

            ShootDetection(GameManager.Instance.playerGo.transform.position, soundRadius);

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
                HitLevel(hit);
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

                    //Debug.Log(material.name);

                    Instantiate(hitEffectGo, hit.point, Quaternion.LookRotation(hit.normal));
                }

                // Raycast hits Enemy
                else if (hit.transform.tag == "Enemy")
                {
                    EnemyController enemy = hit.transform.GetComponent<EnemyController>();

                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            //GameObject bloodGo = Instantiate(item, hit.point, Quaternion.LookRotation(hit.normal));

                            GameObject bloodGo = Instantiate(item, hit.transform.position, Quaternion.LookRotation(hit.normal));

                            bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            // GameObject bloodGo = Instantiate(item, hit.point /*+ (hit.transform.forward * 1f)*/,
                            //                                  item.transform.rotation);

                            GameObject bloodGo = Instantiate(item, hit.transform.position /*+ (hit.transform.forward * 1f)*/,
                                item.transform.rotation);

                            bloodGo.transform.parent = hit.transform;
                        }
                    }
                    enemy.TakeDamage(damage);
                }
                else
                {
                    Debug.Log("Hit: " + hit.transform.gameObject.name);
                    //Instantiate(hitEffectGo, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
            canAttack = false;

            readyToFire = false;
            Invoke("setReadyToFire", fireDelay);
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