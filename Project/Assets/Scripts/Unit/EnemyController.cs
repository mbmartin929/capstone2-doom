using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class EnemyController : UnitController
    {
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
                        GameObject bloodSplat = Instantiate(bloodSplatGos[Random.Range(0, bloodSplatGos.Length)], transform.position, Quaternion.identity);
                        //bloodSplat.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(150, 200f), transform.position, 500f, 0.0f, ForceMode.Force);
                        bloodSplat.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-10f, 10f), Random.Range(-20f, 20f), Random.Range(-10f, 10f));
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
                Debug.Log("After IENumerator IS NOT DEAD");
                animator.SetTrigger("Attack");
            }
            else
            {
                Debug.Log("After IENumerator IS DEAD");
                animator.SetTrigger("Dead");
            }

        }
    }
}
