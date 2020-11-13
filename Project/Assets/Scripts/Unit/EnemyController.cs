using System.Collections;
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

        public bool useGib = true;
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

        void Awake()
        {
            //enemyAI = GetComponent<EnemyAI>();

            enemySounds = GetComponent<EnemySounds>();
        }

        // Start is called before the first frame update
        void Start()
        {
            //Debug.Log("Parent: " + transform.parent);
            //Debug.Log("Parent Parent: " + transform.parent.parent);

            // Debug options
            //maxHealth -= 20;

            CurHealth = maxHealth;
            EndGameScreen.Instance.totalEnemies++;
        }

        // Update is called once per frame
        void Update()
        {
            //CurrentHealth = CurHealth;W

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
            // for (int i = 0; i <= 1; i++)
            // {

            // }
            //int id = Random.Range(0, bloodSplatGos.Length);

            RaycastHit hit;
            int layerMask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 50f, layerMask))
            {
                int randomBloodNumber = Random.Range(1, 5);
                float randomBloodTimer = Random.Range(0.1f, 0.25f);

                StartCoroutine(GetComponent<DecalPainter>().Paint(hit.point + hit.normal * 1f, randomBloodNumber, 1.0f, randomBloodTimer));
            }

            if (IsDead())
            {
                Debug.Log("Spawning Dead Gibs");
                enemySounds.BloodSplatterSound();

                // DECREASES HEALTH
                CurHealth -= amount;

                // Gibbed
                if (CurHealth <= -gibDeath)
                {
                    enemySounds.GibExplosionSound();

                    if (!contributedGib)
                    {
                        EndGameScreen.Instance.enemiesGibbed++;
                        contributedGib = true;
                    }

                    //GameObject gib = Instantiate(gibGo, transform.position, transform.rotation);
                    gameObject.SetActive(false);

                    if (useGib)
                    {
                        Debug.Log("Dead Use Gib");
                        for (int i = 0; i < gibsAmount; i++)
                        {
                            GameObject gib = Instantiate(gibGo, transform.position, gibGo.transform.rotation);
                            Debug.Log("Finish Spawning Gibs");
                        }
                    }

                    GetComponent<EnemyDrops>().Drop();


                    //transform.parent = GameManager.Instance.deadEnemies.transform;
                    //transform.parent.SetParent(GameManager.Instance.deadEnemies.transform);
                    Debug.Log("Parent: " + transform.parent.gameObject);
                    Destroy(transform.parent.gameObject);

                    Debug.Log("Dead Gib!");

                    // for (int i = 0; i < ActorAvatarManager.Instance.spiderAvatars.Length; i++)
                    // {
                    //     if (ActorAvatarManager.Instance.spiderAvatars[i] == null)
                    //     {
                    //         ActorAvatarManager.Instance.spiderAvatars[i] = GetComponent<SpriteRenderer>().material;
                    //         return;
                    //     }
                    // }
                    Destroy(gameObject, 5.0f);
                }
                Debug.Log("Finished Dead Gibs");
            }
            else if (!IsDead())
            {
                Debug.Log("Spawning Not-Dead Gibs");
                //GetComponent<AudioSource>().Play();

                enemySounds.BloodSplatterSound();

                if (CurArmor <= 0)
                {
                    // DECREASES HEALTH
                    CurHealth -= amount;

                    // Checks Hurt Chance
                    float randValue = Random.value;
                    if (randValue < painChance)
                    {
                        //Debug.Log("Pain");
                        //enemyAI.actor.SetCurrentState(DemoActor.State.PAIN);
                        enemySounds.PainSound();

                        if (currentCoroutine != null)
                        {
                            StopCoroutine(currentCoroutine);
                        }

                        currentCoroutine = StartCoroutine(GetHit());
                    }

                    if (CurHealth <= -gibAlive)
                    {
                        enemySounds.GibExplosionSound();

                        if (!contributedGib)
                        {
                            EndGameScreen.Instance.enemiesGibbed++;
                            contributedGib = true;
                        }

                        //GameObject gib = Instantiate(gibGo, transform.position, transform.rotation);
                        //gameObject.SetActive(false);
                        //transform.parent = GameManager.Instance.deadEnemies.transform;


                        if (useGib)
                        {
                            Debug.Log("Not-Dead Use Gib");
                            for (int i = 0; i < gibsAmount; i++)
                            {
                                GameObject gib = Instantiate(gibGo, transform.position, gibGo.transform.rotation);
                                Debug.Log("Finish Spawning Gibs");
                            }
                        }

                        //Destroy(gameObject, 5.0f);

                        Debug.Log("Gib!");

                        // for (int i = 0; i < ActorAvatarManager.Instance.spiderAvatars.Length; i++)
                        // {
                        //     if (ActorAvatarManager.Instance.spiderAvatars[i] == null)
                        //     {
                        //         ActorAvatarManager.Instance.spiderAvatars[i] = GetComponent<SpriteRenderer>().material;
                        //         return;
                        //     }
                        // }
                        //transform.parent.SetParent(GameManager.Instance.deadEnemies.transform);
                        Debug.Log("Parent: " + transform.parent.gameObject);
                        Destroy(transform.parent.gameObject);
                        Debug.Log("Finished setting parent");
                    }
                    else
                    {
                        Debug.Log(CurHealth + " <= " + -gibAlive);
                    }

                    if (IsDead())
                    {
                        Debug.Log("Start IsDead from !IsDead");

                        // GameObject bloodFlowGo = Instantiate(bloodFlow, transform.position, bloodFlow.transform.rotation);
                        // bloodFlowGo.transform.parent = transform;

                        // Destroy(transform.parent.GetChild(2).gameObject);

                        Die();
                        Debug.Log("Finished IsDead from !IsDead");
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
                Debug.Log("Finished Not-Dead Gibs");
            }
        }

        public void Die()
        {
            Debug.Log("Start Die");
            EndGameScreen.Instance.killedEnemies++;

            //Debug.Log("Die");
            animator.SetTrigger("Dead");
            enemySounds.DeathSound();

            Vector3 size = GetComponent<BoxCollider>().size;
            Vector3 center = GetComponent<BoxCollider>().center;

            GetComponent<BoxCollider>().size = new Vector3(size.x, deadColliderSize.y, size.z);
            GetComponent<BoxCollider>().center = new Vector3(center.x, deadColliderCenter.y, center.z);

            //Debug.Log(GameManager.Instance.DeadEnemies.transform);
            //transform.parent = GameManager.Instance.deadEnemies.transform;
            //transform.parent.SetParent(GameManager.Instance.deadEnemies.transform);

            Debug.Log("My Parent: " + transform.parent.gameObject.name);
            Destroy(transform.parent.gameObject);

            Debug.Log("Set Parent From Die");
        }

        private IEnumerator GetHit()
        {
            if (IsDead())
            {
                //Debug.Log("IENumerator Die");
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
            transform.parent.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
