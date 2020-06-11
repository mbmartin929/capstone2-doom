using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCbaseFSM : StateMachineBehaviour
{
    public GameObject NPC;
    public GameObject opponent;
    //private float speed = 2.0f;
    //private float rotSpeed = 0.5f;
    public float accuracy = 2.0f;
    public NavMeshAgent agent;

    void Awake()
    {

    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        agent = NPC.GetComponent<NavMeshAgent>();
        opponent = NPC.GetComponent<EnemyAI>().GetPlayer();
    }
}
