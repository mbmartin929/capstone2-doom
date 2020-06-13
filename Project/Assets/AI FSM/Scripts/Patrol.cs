using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Patrol : NPCbaseFSM
{

    GameObject[] wayPoints;
    int currentWp;
    private float waitTime;
    public float startTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Patrol State");

        isChasing = false;
        isPatrolling = true;
        isAttacking = false;
        isDead = false;

        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.isStopped = false;

        waitTime = startTime;
        wayPoints = GameObject.FindGameObjectsWithTag("wayPoints");
        currentWp = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!enemyController.IsDead())
        {
            if (Vector3.Distance(wayPoints[currentWp].transform.position, NPC.transform.position) < accuracy)
            {
                if (waitTime <= 0)
                {
                    waitTime = startTime;
                    currentWp = Random.Range(0, wayPoints.Length);
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
            agent.SetDestination(wayPoints[currentWp].transform.position);

            enemyAI.EnemyRaycast();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
