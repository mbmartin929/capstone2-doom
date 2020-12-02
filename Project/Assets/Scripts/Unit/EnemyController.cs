using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private bool endBoss = false;

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

                CheckAvatar();

                Destroy(transform.parent.gameObject);
                //Debug.Log("Destroys transform.parent.gameobject: " + transform.parent.gameObject.name);

                if (endBoss)
                {
                    //DialogueAssistant.Instance.StopAllCoroutines();
                    //DialogueAssistant.Instance.end
                    GameManager.Instance.StartCoroutine(GameManager.Instance.OpenArenaDoorInTime(4.2f));
                }
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
                    CheckAvatar();
                }

                if (transform.parent.parent != null)
                {
                    transform.parent.parent = GameManager.Instance.deadEnemies.transform;
                }

                // if (IsDead())
                // {
                //     Die();
                // }

                //
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

        transform.parent.SetParent(GameManager.Instance.deadEnemies.transform);

        // Debug.Log("My Parent: " + transform.parent.gameObject.name);

        //Debug.Log("Set Parent From Die");
        //enemyAI.actor.SetCurrentState(DemoActor.State.DIE);

        //CheckAvatar();
    }

    private void CheckAvatar()
    {
        if (GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.Spider)
        {
            Debug.Log("Avatar is Spider");
            for (int i = 0; i < ActorAvatarManager.Instance.spiderAvatars.Length; i++)
            {
                if (ActorAvatarManager.Instance.spiderAvatars[i] == null)
                {
                    Debug.Log("Index: " + i + " is empty.");
                    ActorAvatarManager.Instance.spiderAvatars[i] = GetComponent<SpriteRenderer>().material;
                    return;
                }
            }

            ActorAvatarManager.Instance.spiderAvatars = ActorAvatarManager.Instance.spiderAvatars.Distinct().ToArray();
        }
        else if (GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.GreenSlime)
        {
            Debug.Log("Avatar is Green Slime");
            for (int i = 0; i < ActorAvatarManager.Instance.slimeAvatars.Length; i++)
            {
                if (ActorAvatarManager.Instance.slimeAvatars[i] == null)
                {
                    Debug.Log("Index: " + i + " is empty.");
                    ActorAvatarManager.Instance.slimeAvatars[i] = GetComponent<SpriteRenderer>().material;
                    return;
                }
            }

            ActorAvatarManager.Instance.slimeAvatars = ActorAvatarManager.Instance.slimeAvatars.Distinct().ToArray();
        }
        else if (GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.Worm)
        {
            Debug.Log("Avatar is Worm");
            for (int i = 0; i < ActorAvatarManager.Instance.wormAvatars.Length; i++)
            {
                if (ActorAvatarManager.Instance.wormAvatars[i] == null)
                {
                    Debug.Log("Index: " + i + " is empty.");
                    ActorAvatarManager.Instance.wormAvatars[i] = GetComponent<SpriteRenderer>().material;
                    return;
                }
            }

            ActorAvatarManager.Instance.wormAvatars = ActorAvatarManager.Instance.wormAvatars.Distinct().ToArray();
        }
        else if (GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.BbWorm)
        {
            Debug.Log("Avatar is Bb Worm");
            for (int i = 0; i < ActorAvatarManager.Instance.bbWormAvatars.Length; i++)
            {
                if (ActorAvatarManager.Instance.bbWormAvatars[i] == null)
                {
                    Debug.Log("Index: " + i + " is empty.");
                    ActorAvatarManager.Instance.bbWormAvatars[i] = GetComponent<SpriteRenderer>().material;
                    return;
                }
            }

            ActorAvatarManager.Instance.bbWormAvatars = ActorAvatarManager.Instance.bbWormAvatars.Distinct().ToArray();
        }
        else if (GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.RedSlime)
        {
            Debug.Log("Avatar is Red Slime");
            for (int i = 0; i < ActorAvatarManager.Instance.redSlimeAvatars.Length; i++)
            {
                if (ActorAvatarManager.Instance.redSlimeAvatars[i] == null)
                {
                    Debug.Log("Index: " + i + " is empty.");
                    ActorAvatarManager.Instance.redSlimeAvatars[i] = GetComponent<SpriteRenderer>().material;
                    return;
                }
            }

            ActorAvatarManager.Instance.redSlimeAvatars = ActorAvatarManager.Instance.redSlimeAvatars.Distinct().ToArray();
        }
        else if (GetComponent<ActorBillboard>().enemy == ActorBillboard.Enemy.Snail)
        {
            Debug.Log("Avatar is Snail");
            for (int i = 0; i < ActorAvatarManager.Instance.snailAvatars.Length; i++)
            {
                if (ActorAvatarManager.Instance.snailAvatars[i] == null)
                {
                    Debug.Log("Index: " + i + " is empty.");
                    ActorAvatarManager.Instance.snailAvatars[i] = GetComponent<SpriteRenderer>().material;
                    return;
                }
            }

            ActorAvatarManager.Instance.snailAvatars = ActorAvatarManager.Instance.snailAvatars.Distinct().ToArray();
        }


    }
}

