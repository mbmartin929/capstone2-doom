using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : UnitController
{
    public GameObject[] bloodSplashGos;

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
        Debug.Log(CurHealth);
    }

    // Update is called once per frame
    void Update()
    {

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
            StartCoroutine(GetHit());

            if (CurArmor <= 0)
            {
                // Debug.Log("Health Damage: " + amount);
                // Debug.Log("Remaining Health: " + CurHealth);

                CurHealth -= amount;
            }
            else
            {
                Debug.Log("Armor Damage: " + amount);
                CurArmor -= amount;
            }

            if (CurHealth <= 0)
            {
                Debug.Log("Hi");
                Die();
            }
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        animator.SetTrigger("Dead");
    }

    private IEnumerator GetHit()
    {
        if (IsDead())
        {
            Debug.Log("IENumerator Die");
            //Die();
        }
        else animator.SetTrigger("Get Hit");

        yield return new WaitForSeconds(0.45f);

        if (!IsDead()) animator.SetTrigger("Attack");
        else animator.SetTrigger("Dead");

    }
}
