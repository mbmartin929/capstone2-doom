using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCbaseFSM : StateMachineBehaviour
{
    public GameObject NPC;
    public GameObject playerGo;

    public float accuracy = 2.0f;
    public NavMeshAgent agent;

    protected bool isAttacking;
    protected bool isPatrolling;
    protected bool isChasing;
    protected bool isDead;

    protected EnemyController enemyController;

    protected AISFM AISFM;

    public bool canAttack;



    void Awake()
    {

    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;

        //Debug.Log(animator.gameObject.name);
        //Debug.Log("Hi");

        agent = NPC.GetComponent<NavMeshAgent>();

        AISFM = NPC.GetComponent<AISFM>();

        playerGo = GameManager.Instance.playerGo;

        enemyController = NPC.GetComponent<EnemyController>();
    }

}
