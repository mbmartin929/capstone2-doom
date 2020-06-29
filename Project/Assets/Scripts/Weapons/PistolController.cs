using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class PistolController : WeaponController
    {
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();

            cameraGo = GameObject.FindGameObjectWithTag("Player");
            FindObjectwithTag("MainCamera");

            FOV = fpsCam.fieldOfView;


            CurAmmo = clipAmmo;
            //Debug.Log("CurAmmo: " + CurAmmo + " || " + "ClipAmmo: " + clipAmmo);

            canAttack = true;
            //Debug.Log("PistolController: " + canAttack);
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
                Test();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }

        void Test()
        {
            Debug.Log("Test");
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

            StartCoroutine(Wait(0.1f));

            #region Gun Effects
            anim.SetTrigger("Shoot");
            muzzleParticle.Play();
            StartCoroutine("MuzzleLight");
            PlayGunshotSound();
            #endregion

            CurAmmo--;

            Vector3 shootDirection = fpsCam.transform.forward;
            shootDirection.x += Random.Range(-spreadFactor, spreadFactor);
            shootDirection.y += Random.Range(-spreadFactor, spreadFactor);

            if (Physics.Raycast(fpsCam.transform.position, shootDirection, out hit, range))
            {
                HitLevel(hit);

                // Raycast hits Enemy
                if (hit.transform.tag == "Enemy")
                {
                    EnemyController enemy = hit.transform.GetComponent<EnemyController>();

                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            GameObject bloodGo = Instantiate(item, hit.point, Quaternion.LookRotation(hit.normal));
                            bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            GameObject bloodGo = Instantiate(item, hit.point /*+ (hit.transform.forward * 1f)*/,
                                                             item.transform.rotation);
                            bloodGo.transform.parent = hit.transform;
                        }
                    }
                    enemy.TakeDamage(10);
                }
            }
            canAttack = false;
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