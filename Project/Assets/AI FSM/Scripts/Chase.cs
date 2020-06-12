using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : NPCbaseFSM
{



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Chase State");

        isChasing = true;
        isPatrolling = false;
        isAttacking = false;
        isDead = false;

        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent.isStopped = false;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(opponent.transform.position);
        Vector3 targetRotation = new Vector3(opponent.transform.position.x,
                                             agent.transform.position.y,
                                             opponent.transform.position.z);

        agent.transform.LookAt(targetRotation);


        Vector3 targetPosition = new Vector3(opponent.transform.position.x,
                                             agent.transform.position.y,
                                             opponent.transform.position.z);

        if (Vector3.Distance(agent.transform.position, targetPosition) < enemyAI.distanceToStop)
        {
            enemyAI.anim.SetTrigger("Attack");
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
