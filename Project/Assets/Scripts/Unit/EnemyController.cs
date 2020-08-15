﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EightDirectionalSpriteSystem
{
    public class EnemyController : UnitController
    {
        public int damage = 15;
        public float painChance = 0.5f;
        public float painDuration = 0.2f;
        public float painStrength = 2.0f;
        public float painResistance = 1.0f;

        public Vector3 deadColliderCenter;
        public Vector3 deadColliderSize;

        public GameObject bloodFlow;
        public GameObject[] bloodSplashGos;
        public GameObject[] bloodSplatGos;

        public int CurrentHealth;
        public bool isDead;

        public float projectileSpeed;

        public EnemyAI enemyAI;
        public Animator animator;
        private Vector3 trajectory;

        public float velocity;

        private bool getHit = false;
        private Coroutine currentCoroutine = null;

        void Awake()
        {
            //enemyAI = GetComponent<EnemyAI>();
        }

        // Start is called before the first frame update
        void Start()
        {
            // Debug options
            //maxHealth -= 20;

            CurHealth = maxHealth;
            trajectory = UnityEngine.Random.insideUnitCircle * velocity;
            // Debug.Log(CurHealth);
        }

        // Update is called once per frame
        void Update()
        {
            //CurrentHealth = CurHealth;
            //isDead = IsDead();
            //if (IsDead()) Die();

            if (getHit)
            {
                if (painStrength <= 0) painStrength = 0;
                Debug.Log("Pain Strength: " + painStrength);
                //Debug.Log("Pain: " + (transform.forward * (painStrength - painResistance)));
                transform.parent.position += (transform.forward * (painStrength - painResistance)) * Time.deltaTime;
            }
        }

        public void TakeDamage(int amount)
        {
            if (IsDead())
            {
                // This if statement is not being called
                //Debug.Log("Take Damage Die");
                //Die();
            }
            else if (!IsDead())
            {
                GetComponent<AudioSource>().Play();
                if (CurArmor <= 0)
                {
                    // DECREASES HEALTH
                    CurHealth -= amount;

                    // Checks Hurt Chance
                    float randValue = Random.value;
                    if (randValue < painChance)
                    {
                        Debug.Log("Pain");
                        //enemyAI.actor.SetCurrentState(DemoActor.State.PAIN);

                        if (currentCoroutine != null)
                        {
                            StopCoroutine(currentCoroutine);
                        }

                        currentCoroutine = StartCoroutine(GetHit());
                    }

                    for (int i = 0; i <= 1; i++)
                    {
                        int id = Random.Range(0, bloodSplatGos.Length);
                        //GameObject bloodSplat = Instantiate(bloodSplatGos[id], transform.position, bloodSplatGos[id].transform.rotation);

                        //bloodSplat.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(500, 700f), transform.position, 900f, 0.0f, ForceMode.Force);
                        //bloodSplat.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-5, 5), Random.Range(5f, 15f), Random.Range(-5, 5));


                    }

                    RaycastHit hit;
                    int layerMask = LayerMask.GetMask("Ground");
                    if (Physics.Raycast(transform.position, -Vector3.up, out hit, 50f, layerMask))
                    {
                        //if (hit.collider.CompareTag("Level")) StartCoroutine(GetComponent<DecalPainter>().PaintDecal(hit, 1f, 0.35f));
                        //StartCoroutine(GetComponent<DecalPainter>().PaintDecal(hit, 1f, 0.35f));

                        int randomBloodNumber = Random.Range(1, 5);
                        float randomBloodTimer = Random.Range(0.1f, 0.35f);

                        StartCoroutine(GetComponent<DecalPainter>().Paint(hit.point + hit.normal * 1f, randomBloodNumber, 1.0f, randomBloodTimer));
                        //Debug.Log("Blood");
                    }



                    if (IsDead())
                    {
                        GameObject bloodFlowGo = Instantiate(bloodFlow, transform.position, bloodFlow.transform.rotation);
                        bloodFlowGo.transform.parent = transform;

                        Die();
                    }
                    //else StartCoroutine(GetHit());
                }
                else
                {
                    // DECREASES ARMOR
                    CurArmor -= amount;
                    //StartCoroutine(GetHit());

                    // float randValue = Random.value;
                    // if (randValue < painChance)
                    // {
                    //     Debug.Log("Pain");
                    //     enemyAI.actor.SetCurrentState(DemoActor.State.PAIN);
                    //     StartCoroutine(GetHit());
                    // }

                }
            }
        }

        public void Die()
        {
            //Debug.Log("Die");
            animator.SetTrigger("Dead");

            Vector3 size = GetComponent<BoxCollider>().size;
            Vector3 center = GetComponent<BoxCollider>().center;

            GetComponent<BoxCollider>().size = new Vector3(size.x, deadColliderSize.y, size.z);
            GetComponent<BoxCollider>().center = new Vector3(center.x, deadColliderCenter.y, center.z);

            //Debug.Log(transform.parent);
            //Debug.Log(GameManager.Instance.DeadEnemies.transform);
            transform.parent.parent = GameManager.Instance.DeadEnemies.transform;
        }

        private IEnumerator GetHit()
        {
            if (IsDead())
            {
                Debug.Log("IENumerator Die");
                //Die();
            }
            else
            {
                //Debug.Log("Get Hit");

                animator.SetTrigger("Get Hit");

                enemyAI.actor.SetCurrentState(DemoActor.State.PAIN);

                // transform.parent.GetComponent<NavMeshAgent>().isStopped = true;
                transform.parent.GetComponent<NavMeshAgent>().enabled = false;

                // getHit = true;

                //transform.parent.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            }

            yield return new WaitForSeconds(painDuration);

            transform.parent.GetComponent<NavMeshAgent>().enabled = true;
            //transform.parent.GetComponent<NavMeshAgent>().isStopped = false;
            // getHit = false;

            if (!IsDead())
            {
                //Debug.Log("After IENumerator IS NOT DEAD");
                animator.SetTrigger("Attack");
            }
            else
            {
                //Debug.Log("After IENumerator IS DEAD");
                animator.SetTrigger("Dead");
            }

        }
    }
}
