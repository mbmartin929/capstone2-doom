﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EightDirectionalSpriteSystem
{
    public class WeaponController : MonoBehaviour
    {
        protected Camera fpsCam;

        #region  Main Variables
        [Header("Stats")]
        public int maxAmmo;
        public int maxCapacity;
        [SerializeField] private int curAmmo;
        public int clipAmmo = 9;

        public float range = 100f;
        public float spreadFactor = 0.1f;
        public float FOV;

        [Header("Sounds")]
        public AudioClip[] gunshotSounds;
        public AudioClip[] reloadSounds;

        #endregion

        #region Other Variables
        public Vector3 startWeaponSwitchVector;
        public Vector3 startWeaponSwitchRot;

        public List<GameObject> actors = new List<GameObject>();
        public float muzzleLightResetTime = 0.1f;
        public GameObject bulletCasingParticleGo;
        public ParticleSystem bulletTracerParticle;
        public GameObject muzzleLightGo;
        public GameObject hitEffectGo;
        public Vector3 camRotation;

        #endregion

        #region Private Variables
        protected GameObject cameraGo;
        [SerializeField] protected bool canAttack;
        [HideInInspector] public Animator anim;

        #endregion

        public int CurAmmo
        {
            get { return curAmmo; }
            set
            {
                curAmmo = value;
                if (curAmmo < 0) curAmmo = 0;
                if (curAmmo > maxCapacity) curAmmo = maxCapacity;
            }
        }

        protected void PlayGunshotSound()
        {
            GetComponent<AudioSource>().PlayOneShot(gunshotSounds[0]);
        }

        // Used as animation event
        protected void PlayShotgunShootSound(int index)
        {
            GetComponent<AudioSource>().PlayOneShot(gunshotSounds[index]);
        }

        public void PlayReloadSound(int id)
        {
            GetComponent<AudioSource>().PlayOneShot(reloadSounds[id]);
        }
        bool AnimatorIsPlaying(string stateName)
        {
            return anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }
        protected void SingleShoot()
        {
            //fpsCam.transform.eulerAngles += camRotation;

            //Debug.Log("SingleShoot");
            if (curAmmo <= 0)
            {
                //Debug.Log(curAmmo);
                GetComponent<AudioSource>().PlayOneShot(gunshotSounds[1]);
            }

            RaycastHit hit;
            // if ((canAttack) && (curAmmo >= 1))
            if (curAmmo >= 1)
            {

                canAttack = false;
                StartCoroutine(Wait(0.1f));

                #region  Comments
                // Debug.Log("Before: " + canAttack);
                // if (!canAttack) return;

                // canAttack = false;
                // Debug.Log("After: " + canAttack);

                // Debug.Log("Before: " + canAttack);
                // if (!canAttack) return;
                // Debug.Log("After: " + canAttack);

                // if (AnimatorIsPlaying("DS-Shoot"))
                // {
                //     return;
                //     //Debug.Log("Hi");
                // }
                //else

                // Debug.Log("Before: " + canAttack);
                // canAttack = false;
                // Debug.Log("After: " + canAttack);
                #endregion

                #region Gun Effects
                anim.SetTrigger("Shoot");
                //muzzleParticleGo.Play();
                StartCoroutine("MuzzleLight");
                PlayGunshotSound();
                #endregion

                Vector3 shootDirection = fpsCam.transform.forward;
                shootDirection.x += Random.Range(-spreadFactor, spreadFactor);
                shootDirection.y += Random.Range(-spreadFactor, spreadFactor);

                if (Physics.Raycast(fpsCam.transform.position, shootDirection, out hit, range))
                {
                    curAmmo--;
                    // Raycast hits Level
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
                //CheckAmmo();
            }
            //else if (!canAttack) return;
        }

        private void StartWeaponSwitch()
        {
            //transform.localPosition = startWeaponSwitchVector;
            //transform.localRotation = Quaternion.Euler(startWeaponSwitchRot.x, startWeaponSwitchRot.y, startWeaponSwitchRot.z);
            //Debug.Log("Start: " + transform.localPosition);
        }

        private void EndWeaponSwitch()
        {

        }

        public void SwitchWeapon(Transform _weapon)
        {
            WeaponController weapon = _weapon.GetComponent<WeaponController>();

            weapon.anim.SetTrigger("");
        }

        public void SwitchTo()
        {
            anim.SetTrigger("Switch To");
            transform.localPosition = startWeaponSwitchVector;
            transform.localRotation = Quaternion.Euler(startWeaponSwitchRot.x, startWeaponSwitchRot.y, startWeaponSwitchRot.z);
            //StartCoroutine(SwitchToNumerator());
            Debug.Log("SwitchTo");
        }

        public void SwitchAway()
        {
            // if (weapon.gameObject.activeSelf)
            // {
            //     StartCoroutine(SwitchAwayNumerator());
            //     Debug.Log("SwitchAway");
            // }

            anim.SetTrigger("Switch Away");
            //Debug.Log("SwitchAway");
        }

        private IEnumerator SwitchToNumerator()
        {
            anim.SetTrigger("Switch To");
            yield return new WaitForSeconds(0.30f);
            //SwitchAway();
        }

        private IEnumerator SwitchAwayNumerator()
        {
            anim.SetTrigger("Switch Away");
            yield return new WaitForSeconds(0.30f);
            SwitchTo();
        }

        private void SetIdle()
        {
            anim.SetTrigger("Idle");
        }

        private IEnumerator Wait(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            canAttack = true;
        }

        protected void Reload()
        {
            if (curAmmo >= clipAmmo)
            {
                Debug.Log("You have full ammo");
                return;
            }
            else if (maxAmmo <= 0)
            {
                Debug.Log("You have no ammo");
                return;
            }
            else if ((clipAmmo - curAmmo) >= maxAmmo)
            {
                curAmmo += maxAmmo;
                maxAmmo = 0;

                anim.SetTrigger("Reload");
                Debug.Log("Decreased Reload");
            }
            else
            {
                maxAmmo -= (clipAmmo - curAmmo);
                curAmmo = clipAmmo;

                Debug.Log("Normal Reload");
                anim.SetTrigger("Reload");
            }
        }

        // Used as Animation
        private void FinishReload()
        {
            anim.SetTrigger("Idle");
        }

        private void PickUpAmmo(int amount)
        {
            maxAmmo += amount;
            if (maxAmmo >= maxCapacity) maxAmmo = maxCapacity;
        }

        private IEnumerator MuzzleLight()
        {
            Debug.Log(muzzleLightGo.gameObject.name);
            muzzleLightGo.gameObject.SetActive(true);

            yield return new WaitForSeconds(muzzleLightResetTime);

            muzzleLightGo.gameObject.SetActive(false);
        }

        // Used as animation event
        private void FinishShooting()
        {
            //Debug.Log("FinishShooting: " + canAttack);
            anim.SetTrigger("Idle");
            //canAttack = true;
        }

        private void Idle()
        {
            anim.SetTrigger("Idle");
        }

        public void FindObjectwithTag(string _tag)
        {
            // Debug.Log(cameraGo.name);

            actors.Clear();
            Transform parent = cameraGo.transform;
            GetChildObject(parent, _tag);
        }

        protected void HitLevel(RaycastHit hit)
        {
            // Raycast hits Level

        }

        public void GetChildObject(Transform parent, string _tag)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                if (child.tag == _tag)
                {
                    actors.Add(child.gameObject);
                    cameraGo = child.gameObject;
                    fpsCam = cameraGo.GetComponent<Camera>();
                    // camTransform = cameraGo.transform;
                }
                if (child.childCount > 0)
                {
                    GetChildObject(child, _tag);
                }
            }
        }

        static Mesh GetMesh(GameObject go)
        {
            if (go)
            {
                MeshFilter mf = go.GetComponent<MeshFilter>();
                if (mf)
                {
                    Mesh m = mf.sharedMesh;
                    if (!m) { m = mf.mesh; }
                    if (m)
                    {
                        return m;
                    }
                }
            }
            return (Mesh)null;
        }

        public void CameraX(float value)
        {
            fpsCam.transform.eulerAngles += new Vector3(value, 0, 0);
        }
    }
}