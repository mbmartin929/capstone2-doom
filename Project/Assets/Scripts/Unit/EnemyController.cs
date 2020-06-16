using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : UnitController
{
    public GameObject[] bloodSplashGos;

    public int CurrentHealth;
    public bool isDead;

    private EnemyAI enemyAI;
    private Animator animator;

    void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CurHealth = maxHealth;
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
            if (CurArmor <= 0)
            {
                // DECREASES HEALTH
                CurHealth -= amount;

                if (IsDead())
                {
                    //Debug.Log("Hi");
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
