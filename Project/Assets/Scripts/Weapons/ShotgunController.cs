﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class ShotgunController : WeaponController
    {
        public Transform bulletCasingLoc;
        public Transform bulletTracerLoc;
        public int pelletCount = 8;

        public float scaleLimit;
        public float z = 10f;

        public float fireDelay = 1.0f;
        public bool readyToFire = true;

        private Vector3 startTransform;

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

                //CurAmmo = AmmoInventory.Instance.curShotgunAmmo;
            }
        }

        void Awake()
        {
            //Debug.Log("Hi");
            anim = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {
            cameraGo = GameObject.FindGameObjectWithTag("Player");
            FindObjectwithTag("MainCamera");

            FOV = fpsCam.fieldOfView;

            CurAmmo = AmmoInventory.Instance.curShotgunAmmo;

            canAttack = true;
            TextManager.Instance.UpdateAmmoText();
        }

        // Update is called once per frame
        void Update()
        {
            fpsCam.transform.eulerAngles += camRotation;
            fpsCam.fieldOfView = FOV;

            //Debug.Log(transform.localPosition);
        }

        void LateUpdate()
        {
            if ((Input.GetMouseButtonDown(0)) && (canAttack))
            {
                Shoot();
            }
        }

        private IEnumerator WillCancelReload()
        {
            cancelReload = true;
            yield return new WaitForSeconds(0.42f);
            cancelReload = false;
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
            //startTransform = transform.localPosition;
            //Debug.Log(startTransform);

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
            PlayGunshotSound();

            anim.SetTrigger("Shoot");
            StartCoroutine("MuzzleLight");
            StartCoroutine(Wait(0.2f));
            Vector3 rotationVector = transform.rotation.eulerAngles;

            TextManager.Instance.UpdateAmmoText();

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

        public void ReloadSound()
        {
            //GetComponent<AudioSource>().playone
        }
    }
}