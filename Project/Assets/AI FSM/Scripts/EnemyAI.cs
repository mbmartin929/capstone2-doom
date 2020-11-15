﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EightDirectionalSpriteSystem
{
    public class EnemyAI : MonoBehaviour
    {
        [HideInInspector] public DemoActor actor;
        [HideInInspector] public Animator anim;
        [HideInInspector] public ActorBillboard billboard;
        [HideInInspector] public AudioSource audioSource;
        [HideInInspector] public EnemySounds enemySounds;

        public Transform attackLoc;

        public EnemyController enemyController;

        [Header("AI Detection")]
        public bool attackPlayerOnStart = false;
        public float attackPlayerOnStartDelayTime = 2.9f;
        public float distanceToAttack = 16.0f;

        public float viewRadius;
        [Range(0, 360)]
        public float viewAngle;
        public LayerMask targetMask;
        public LayerMask obstacleMask;
        public List<Transform> visibleTargets;
        private NavMeshAgent agent;


        void Awake()
        {
            anim = GetComponent<Animator>();
            actor = GetComponent<DemoActor>();
            billboard = transform.GetChild(0).GetComponent<ActorBillboard>();
            agent = GetComponent<NavMeshAgent>();
            enemySounds = transform.GetChild(0).GetComponent<EnemySounds>();
            audioSource = transform.GetChild(0).GetComponent<AudioSource>();

            visibleTargets.Clear();
        }

        // Start is called before the first frame update
        void Start()
        {
            //StartCoroutine("FindTargetsWithDelay", .2f);

            if (attackPlayerOnStart)
            {
                Invoke("FindPlayer", attackPlayerOnStartDelayTime);
            }
        }

        private void FindPlayer()
        {
            visibleTargets.Add(GameManager.Instance.playerGo.transform);
            anim.SetTrigger("Chase");
        }

        public void AgentSetDestinationPatrol()
        {
            Vector3 newPos = EnemyAI.RandomNavSphere(transform.position, 5.0f, 0);
            Debug.Log(gameObject.name + ": " + transform.position + "," + newPos);
            agent.SetDestination(newPos);
        }

        public void AgentSetDestinationPlayer()
        {
            if (!agent.enabled) return;
            else agent.SetDestination(GameManager.Instance.playerGo.transform.position);
        }

        public void AgentStop(bool state)
        {
            try
            {
                if (!agent.enabled) return;
                else agent.isStopped = state;
            }
            catch (Exception e)
            {
                Debug.LogException(e, this);
                Debug.Log("Lol");
            }
        }

        public void WormAttack()
        {
            // GameObject a1 = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            // a1.GetComponent<Projectile>().enemyAI = this;
            // a1.GetComponent<Projectile>().damage = enemyController.damage;
            // a1.GetComponent<Projectile>().LaunchProjectile1();

            // GameObject a2 = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            // a2.GetComponent<Projectile>().enemyAI = this;
            // a2.GetComponent<Projectile>().damage = enemyController.damage;
            // a2.GetComponent<Projectile>().LaunchProjectile2();

            // GameObject a3 = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            // a3.GetComponent<Projectile>().enemyAI = this;
            // a3.GetComponent<Projectile>().damage = enemyController.damage;
            // a3.GetComponent<Projectile>().LaunchProjectile3();

            GameObject a1 = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            a1.GetComponent<Projectile>().enemyAI = this;
            a1.GetComponent<Projectile>().damage = enemyController.damage;
            a1.GetComponent<Projectile>().LaunchWormProjectile();

            float distance;
            distance = Vector3.Distance(transform.position, GameManager.Instance.playerGo.transform.position);
            //Debug.Log("Before Distance: " + distance);

            if (distance <= 5.5f)
            {
                distance *= -30.0f;
            }
            else
            {
                distance *= 35.0f;
            }
        }

        public void SnailAttack()
        {
            GameObject a1 = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            a1.GetComponent<Projectile>().enemyAI = this;
            a1.GetComponent<Projectile>().damage = enemyController.damage;
            a1.GetComponent<Projectile>().LaunchSnailProjectile(transform.position);
        }

        public void SlimeAttack(float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag == "Player")
                {
                    //Debug.Log("Hit Player!");

                    hitCollider.GetComponent<PlayerController>().TakeDamage(enemyController.damage);
                }
            }
        }

        public void EnemyRaycast()
        {
            if (visibleTargets.Count != 0)
            {
                //Debug.Log(gameObject.name + " Found Player!");
                anim.SetTrigger("Chase");
            }
        }

        // IEnumerator FindTargetsWithDelay(float delay)
        // {
        //     while (true)
        //     {
        //         yield return new WaitForSeconds(delay);
        //         FindVisibleTargets();
        //     }
        // }

        public void FindVisibleTargets()
        {
            visibleTargets.Clear();
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                //Debug.Log(targetsInViewRadius[i].gameObject.name);

                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle /* / 2 */)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        if (target.gameObject.tag == "Player") visibleTargets.Add(target); anim.SetTrigger("Chase");
                    }
                }
            }
        }

        public IEnumerator FindVisibleTargetsCoroutine()
        {
            visibleTargets.Clear();
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                //Debug.Log(targetsInViewRadius[i].gameObject.name);
                //Debug.Log(gameObject.name + " Finding Visible Targets");

                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle /* / 2 */)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        if (target.gameObject.tag == "Player") visibleTargets.Add(target);
                    }
                }

                yield return new WaitForSeconds(1.29f);
            }
        }

        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        private void PlayerDetection(RaycastHit hitInfo)
        {
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.tag == "Player")
                {
                    //actor.SetCurrentState(DemoActor.State.WALKING);
                    anim.SetTrigger("Chase");
                }
            }
        }

        public void ChasePlayer()
        {
            visibleTargets.Add(GameManager.Instance.playerGo.transform);
            Debug.Log("Chase Player");
        }

        public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
        {
            //Debug.Log("Random Nav Sphere");

            NavMeshHit navHit;
            Vector3 result;

            Vector3 randomPoint = origin + UnityEngine.Random.insideUnitSphere * dist;
            if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, 8))
            {
                result = navHit.position;
                return result;
            }
            else
            {
                result = randomPoint;
                return result;
            }
        }

        public void GetNewDir()
        {
            try
            {
                actor.SetCurrentState(DemoActor.State.WALKING);
                Vector3 newPos = EnemyAI.RandomNavSphere(transform.position, 4.0f, 0);
                GetComponent<NavMeshAgent>().SetDestination(newPos);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
                GetComponent<NavMeshAgent>().SetDestination(transform.position);
            }


            StartCoroutine(ActorAttack());
        }

        private IEnumerator ActorAttack()
        {
            yield return new WaitForSeconds(1.15f);
            if (enemyController.IsDead())
            {

            }
            else actor.SetCurrentState(DemoActor.State.SHOOT);
        }

        private IEnumerator RandomPatrolSound()
        {
            int rand = UnityEngine.Random.Range(0, enemySounds.idle.Length);
            float time = UnityEngine.Random.Range(enemySounds.minPatrolSoundTime, enemySounds.maxPatrolSoundTime);

            yield return new WaitForSeconds(time);

            AudioClip patrolSound = enemySounds.idle[rand];

            if (audioSource == null)
            {
                Destroy(gameObject);
                yield return null;
            }

            audioSource.clip = patrolSound;
            audioSource.Play();
        }

        public void CallRandomPatrolSound()
        {
            StartCoroutine(RandomPatrolSound());
        }
    }
}
