using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EightDirectionalSpriteSystem
{
    public class EnemyController : UnitController
    {
        public AudioClip[] audioClips;
        public GameObject deathParticle;
        public Transform attackPos;

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

        public GameObject gibGo;
        public int gibsAmount = 12;
        public int gibAlive = 15;
        public int gibDeath = 30;

        public float projectileSpeed;

        public EnemyAI enemyAI;
        public Animator animator;

        public float velocity;

        private bool getHit = false;
        private Coroutine currentCoroutine = null;
        private EnemySounds enemySounds;

        private bool contributedGib = false;

        private AudioSource audioSource;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            enemySounds = GetComponent<EnemySounds>();
        }

        // Start is called before the first frame update
        void Start()
        {
            CurHealth = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void WormAttack()
        {
            audioSource.PlayOneShot(audioClips[0]);

            GameObject a1 = Instantiate(projectileGo, attackPos.position, transform.rotation);
            projectileGo.GetComponent<Projectile>().attackLockPos = attackPos;
            a1.GetComponent<Projectile>().damage = damage;
            a1.GetComponent<Projectile>().attackLockPos = attackPos;
            a1.GetComponent<Projectile>().LaunchProjectile1();

            GameObject a2 = Instantiate(projectileGo, attackPos.position, transform.rotation);
            a2.GetComponent<Projectile>().damage = damage;
            a1.GetComponent<Projectile>().attackLockPos = attackPos;
            a2.GetComponent<Projectile>().LaunchProjectile2();

            GameObject a3 = Instantiate(projectileGo, attackPos.position, transform.rotation);
            a3.GetComponent<Projectile>().damage = damage;
            a3.GetComponent<Projectile>().attackLockPos = attackPos;
            a3.GetComponent<Projectile>().LaunchProjectile3();

            float distance;
            distance = Vector3.Distance(transform.position, GameManager.Instance.playerGo.transform.position);


            if (distance <= 5.5f)
            {
                distance *= -30.0f;
            }
            else
            {
                distance *= 35.0f;
            }
        }

        public void TakeDamage(int amount)
        {
            for (int i = 0; i <= 1; i++)
            {
                int id = Random.Range(0, bloodSplatGos.Length);
            }

            if (IsDead())
            {
                audioSource.PlayOneShot(audioClips[1]);
                Instantiate(deathParticle, transform.position, Quaternion.identity);
                Destroy(transform.parent.gameObject, 0f);
            }
            else if (!IsDead())
            {
                //enemySounds.BloodSplatterSound();

                if (CurArmor <= 0)
                {
                    // DECREASES HEALTH
                    CurHealth -= amount;
                }

                if (IsDead())
                {
                    GameObject _deathParticle = Instantiate(deathParticle, transform.position, Quaternion.identity) as GameObject;
                    _deathParticle.GetComponent<ParticleSystem>().Play();
                    Destroy(transform.parent.gameObject, 0f);
                }
            }
            else
            {
                // DECREASES ARMOR
                CurArmor -= amount;
            }
        }
    }

    // public void Die()
    // {
    //     EndGameScreen.Instance.killedEnemies++;

    //     //Debug.Log("Die");
    //     animator.SetTrigger("Dead");
    //     enemySounds.DeathSound();

    //     Vector3 size = GetComponent<BoxCollider>().size;
    //     Vector3 center = GetComponent<BoxCollider>().center;

    //     GetComponent<BoxCollider>().size = new Vector3(size.x, deadColliderSize.y, size.z);
    //     GetComponent<BoxCollider>().center = new Vector3(center.x, deadColliderCenter.y, center.z);

    //     //Debug.Log(transform.parent);
    //     //Debug.Log(GameManager.Instance.DeadEnemies.transform);
    //     //transform.parent = GameManager.Instance.deadEnemies.transform;
    //     transform.parent.SetParent(GameManager.Instance.deadEnemies.transform);
    //     //Debug.Log("Set Parent");
    // }

    // private IEnumerator GetHit()
    // {
    //     if (IsDead())
    //     {
    //         //Debug.Log("IENumerator Die");
    //         //Die();
    //     }
    //     else
    //     {
    //         //Debug.Log("Get Hit");

    //         animator.SetTrigger("Get Hit");

    //         enemyAI.actor.SetCurrentState(DemoActor.State.PAIN);

    //         // transform.parent.GetComponent<NavMeshAgent>().isStopped = true;
    //         transform.parent.GetComponent<NavMeshAgent>().enabled = false;

    //         // getHit = true;

    //         //transform.parent.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
    //     }

    //     yield return new WaitForSeconds(painDuration);

    //     transform.parent.GetComponent<NavMeshAgent>().enabled = true;
    //     //transform.parent.GetComponent<NavMeshAgent>().isStopped = false;
    //     // getHit = false;

    //     if (!IsDead())
    //     {
    //         //Debug.Log("After IENumerator IS NOT DEAD");
    //         animator.SetTrigger("Attack");
    //     }
    //     else
    //     {
    //         //Debug.Log("After IENumerator IS DEAD");
    //         animator.SetTrigger("Dead");
    //     }

    // }
}


