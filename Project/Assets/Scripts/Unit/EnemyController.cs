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
        if (!IsDead())
        {
            StartCoroutine(GetHit());

            if (CurArmor <= 0)
            {
                Debug.Log("Health Damage: " + amount);
                Debug.Log("Remaining Health: " + CurHealth);

                CurHealth -= amount;
            }
            else
            {
                Debug.Log("Armor Damage: " + amount);
                CurArmor -= amount;
            }

            if (CurHealth <= 0) Die();
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        animator.SetTrigger("Dead");
    }

    private IEnumerator GetHit()
    {
        if (IsDead()) Die();
        else animator.SetTrigger("Get Hit");

        yield return new WaitForSeconds(0.35f);

        if (!IsDead()) animator.SetTrigger("Attack");
        else animator.SetTrigger("Dead");

    }
}
