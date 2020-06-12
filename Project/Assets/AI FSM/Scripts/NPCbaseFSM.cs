﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCbaseFSM : StateMachineBehaviour
{
    public GameObject NPC;
    public GameObject opponent;

    public float accuracy = 2.0f;
    public NavMeshAgent agent;

    protected bool isAttacking;
    protected bool isPatrolling;
    protected bool isChasing;
    protected bool isDead;

    protected EnemyAI enemyAI;

    public bool canAttack;

    void Awake()
    {

    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        agent = NPC.GetComponent<NavMeshAgent>();
        opponent = NPC.GetComponent<EnemyAI>().playerGo;
        enemyAI = NPC.GetComponent<EnemyAI>();
    }
}
