using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class EnemyController : UnitController
    {
        public int damage = 15;

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

        void Awake()
        {
            //enemyAI = GetComponent<EnemyAI>();
        }

        // Start is called before the first frame update
        void Start()
        {
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
        }

        public void TakeDamage(int amount)
        {
            if (IsDead())
            {
                // This if statement is not being called
                Debug.Log("Take Damage Die");
                //Die();
            }
            else if (!IsDead())
            {
                GetComponent<AudioSource>().Play();
                if (CurArmor <= 0)
                {
                    // DECREASES HEALTH
                    CurHealth -= amount;

                    for (int i = 0; i <= 1; i++)
                    {
                        int id = Random.Range(0, bloodSplatGos.Length);
                        GameObject bloodSplat = Instantiate(bloodSplatGos[id], transform.position, bloodSplatGos[id].transform.rotation);
                        //bloodSplat.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(500, 700f), transform.position, 900f, 0.0f, ForceMode.Force);
                        bloodSplat.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-5, 5), Random.Range(5f, 15f), Random.Range(-5, 5));
                    }

                    if (IsDead())
                    {
                        GameObject bloodFlowGo = Instantiate(bloodFlow, transform.position, bloodFlow.transform.rotation);
                        bloodFlowGo.transform.parent = transform;

                        Die();
                    }
                    else StartCoroutine(GetHit());
                }
                else
                {
                    // DECREASES ARMOR
                    CurArmor -= amount;
                    StartCoroutine(GetHit());
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
                animator.SetTrigger("Get Hit");
            }

            yield return new WaitForSeconds(0.45f);

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
