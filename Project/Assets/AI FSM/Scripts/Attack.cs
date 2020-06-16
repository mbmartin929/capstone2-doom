using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : NPCbaseFSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (enemyController.IsDead())
        {
            Debug.Log("Attack State DEAD");
        }

        Debug.Log("Attack State");

        isChasing = false;
        isPatrolling = false;
        isAttacking = true;
        isDead = false;

        agent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!enemyController.IsDead())
        {
            Vector3 targetRotation = new Vector3(opponent.transform.position.x,
                                                 agent.transform.position.y,
                                                 opponent.transform.position.z);

            agent.transform.LookAt(targetRotation);

            if (Vector3.Distance(agent.transform.position, targetRotation) > enemyAI.distanceToStop + 3)
            {
                agent.GetComponent<EnemyAI>().anim.SetTrigger("Chase");
            }
        }
        else
        {
            agent.GetComponent<EnemyAI>().anim.SetTrigger("Dead");
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
