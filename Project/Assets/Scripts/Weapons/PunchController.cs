using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class PunchController : WeaponController
    {
        public AudioClip[] wooshSounds;

        public float fireTime = 1.5f;
        public float fireDelay = 1.5f;
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
                anim.SetTrigger("Start Punch");
                RandomPunch();

                //Shoot();
            }
        }

        private void RandomPunch()
        {
            int punchID = Random.Range(0, 4);
            //int punchID = Random.Range(3, 4);

            if (punchID == 0) anim.SetTrigger("Left Punch 0");
            else if (punchID == 1) anim.SetTrigger("Right Punch 0");
            else if (punchID == 2) anim.SetTrigger("Left Punch 1");
            else if (punchID == 3) anim.SetTrigger("Right Punch 1");
        }

        // Used as animation event
        public void PlayRandomPunchSound()
        {
            int index = Random.Range(0, gunshotSounds.Length);
            //GetComponent<AudioSource>().PlayOneShot(gunshotSounds[index]);

            AudioClip sfx = gunshotSounds[index];
            GameObject punchSFX = Instantiate(hitSFX, transform.position, Quaternion.identity);
            punchSFX.GetComponent<AudioSource>().clip = sfx;
            punchSFX.GetComponent<AudioSource>().Play();
        }

        // Used as animation event
        public void PlayRandomWooshSound()
        {
            int index = Random.Range(0, wooshSounds.Length);
            //GetComponent<AudioSource>().PlayOneShot(wooshSounds[index]);

            AudioClip sfx = wooshSounds[index];
            GameObject wooshSFX = Instantiate(hitSFX, transform.position, Quaternion.identity);
            wooshSFX.GetComponent<AudioSource>().clip = sfx;
            wooshSFX.GetComponent<AudioSource>().Play();
        }

        void Shoot()
        {
            RaycastHit hit;

            // if (anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
            // {
            //     return;
            // }

            StartCoroutine(Wait(0.2f));
            #region Gun Effects

            //PlayGunshotSound();
            #endregion

            ShootDetection(GameManager.Instance.playerGo.transform.position, soundRadius);

            Vector3 fireDirection = fpsCam.transform.forward;
            Quaternion fireRotation = Quaternion.LookRotation(fireDirection);

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

                    Instantiate(hitEffectGo, hit.point, Quaternion.LookRotation(hit.normal));
                    Instantiate(bulletHole, hit.point + 0.01f * hit.normal, Quaternion.LookRotation(hit.normal));

                    PlayRandomPunchSound();
                }

                // Raycast hits Enemy
                else if (hit.transform.tag == "Enemy")
                {
                    EnemyController enemy = hit.transform.GetComponent<EnemyController>();
                    if (enemy == null) enemy = hit.transform.GetChild(0).GetComponent<EnemyController>();

                    //Debug.Log(hit.transform.gameObject.name);
                    //Debug.Log(enemy.gameObject.name);

                    foreach (GameObject item in enemy.bloodSplashGos)
                    {
                        if (item.tag == "Hit Normal")
                        {
                            GameObject bloodGo = Instantiate(item, hit.transform.position, Quaternion.LookRotation(hit.normal));
                            //bloodGo.transform.parent = hit.transform;
                        }
                        else
                        {
                            GameObject bloodGo = Instantiate(item, hit.transform.position, item.transform.rotation);
                            //bloodGo.transform.parent = hit.transform;
                        }
                    }
                    enemy.TakeDamage(damage);
                    enemy.painStrength = painStrength;
                    PlayRandomPunchSound();
                }
                else if (hit.transform.tag == "Destructible")
                {
                    DestructibleDoor door = hit.transform.GetComponent<DestructibleDoor>();

                    door.health -= damage;
                    if (door.health <= 0) door.DestroyMesh();
                    PlayRandomPunchSound();
                }
                else { }
                //Debug.Log(hit.transform.name);
            }
            canAttack = false;

            readyToFire = false;
            //Invoke("setReadyToFire", fireDelay);
        }

        private IEnumerator Wait(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            canAttack = true;
        }
    }
}
