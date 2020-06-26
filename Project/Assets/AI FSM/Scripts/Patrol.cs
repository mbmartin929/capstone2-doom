using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Patrol : NPCbaseFSM
{
    Transform[] wayPoints;
    int currentWp;

    private float nextActionTime = 0.0f;
    private float period = 3.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.isStopped = false;

        wayPoints = AISFM.waypoints;
        currentWp = 0;

        Debug.Log("Patrol State");
        //Debug.Log(NPC.name);

        // isChasing = false;
        // isPatrolling = true;
        // isAttacking = false;
        // isDead = false;

        AISFM.EnableEightDirection();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Patrol State On State Update");

        if (!enemyController.IsDead())
        {
            if (Time.time > nextActionTime)
            {
                nextActionTime += period;
                Debug.Log("Stop Wait Time");
                AISFM.GoToDestination();
            }

            AISFM.EnemyRaycast();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
