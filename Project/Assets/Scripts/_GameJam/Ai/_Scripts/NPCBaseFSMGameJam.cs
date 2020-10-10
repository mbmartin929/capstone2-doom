
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCBaseFSMGameJam : StateMachineBehaviour
{

    public GameObject NPC;
    public GameObject[] opponent;
    public UnityEngine.AI.NavMeshAgent agent;
    public float speed;
    public float rotSpeed;
    public float accuracy;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        //GameObject opponent = NPC.GetComponent<VirusAI>().GetPlayer(); /// HERE

        agent = NPC.transform.parent.GetComponent<UnityEngine.AI.NavMeshAgent>();
        base.OnStateEnter(animator, stateInfo, layerIndex);


        //opponent = GameObject.FindGameObjectsWithTag("EnemyGameJam");
        //var target = opponent[Random.Range(0, opponent.Length)].transform;
    }
}
