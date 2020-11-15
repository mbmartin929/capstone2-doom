using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EightDirectionalSpriteSystem;

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


    public bool useGib = true;
    public GameObject gibGo;
    public int gibsAmount = 12;
    public int gibAlive = 15;
    public int gibDeath = 30;

    public Animator animator;

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
        CurHealth = maxHealth;
        EndGameScreen.Instance.totalEnemies++;
    }

    public void TakeDamage(int amount)
    {
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 50f, layerMask))
        {
            int randomBloodNumber = Random.Range(1, 5);

            GetComponent<DecalPainter>().PaintVoid(hit.point + hit.normal * 1f, randomBloodNumber, 1.0f);
        }

        // DECREASES HEALTH
        CurHealth -= amount;

        if (IsDead())
        {
            enemySounds.BloodSplatterSound();

            // Gibbed
            if (CurHealth <= -gibDeath)
            {
                enemySounds.GibExplosionSound();

                if (!contributedGib)
                {
                    EndGameScreen.Instance.enemiesGibbed++;
                    contributedGib = true;
                }

                if (useGib)
                {
                    //Debug.Log("Dead Use Gib");
                    for (int i = 0; i < gibsAmount; i++)
                    {
                        GameObject gib = Instantiate(gibGo, transform.position, gibGo.transform.rotation);
                    }
                }

                GetComponent<EnemyDrops>().Drop();

                Destroy(transform.parent.gameObject);
                Debug.Log("Destroys transform.parent.gameobject: " + transform.parent.gameObject.name);
            }
            else Die();
        }
        else if (!IsDead())
        {
            enemySounds.BloodSplatterSound();

            if (CurHealth <= -gibAlive)
            {
                if (!contributedGib)
                {
                    EndGameScreen.Instance.enemiesGibbed++;
                    contributedGib = true;
                }

                if (useGib)
                {
                    enemySounds.GibExplosionSound();
                    //Debug.Log("Not-Dead Use Gib");
                    for (int i = 0; i < gibsAmount; i++)
                    {
                        GameObject gib = Instantiate(gibGo, transform.position, gibGo.transform.rotation);
                        //Debug.Log("Finish Spawning Gibs");
                    }
                }

                if (transform.parent.parent != null)
                {
                    transform.parent.parent = GameManager.Instance.deadEnemies.transform;
                }

                // if (IsDead())
                // {
                //     Die();
                // }
            }
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
        transform.parent.SetParent(GameManager.Instance.deadEnemies.transform);

        // Debug.Log("My Parent: " + transform.parent.gameObject.name);
        // Debug.Log("My Parent's Parent: " + transform.parent.parent.gameObject.name);
        //Destroy(transform.parent.gameObject);

        //Debug.Log("Set Parent From Die");
        //enemyAI.actor.SetCurrentState(DemoActor.State.DIE);
    }
}

