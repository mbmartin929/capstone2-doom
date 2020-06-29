using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EightDirectionalSpriteSystem
{
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
            enemyAI.actor.SetCurrentState(DemoActor.State.SHOOT);

            agent.isStopped = true;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!enemyController.IsDead())
            {
                Vector3 targetRotation = new Vector3(playerGo.transform.position.x,
                                                    agent.transform.position.y,
                                                    playerGo.transform.position.z);

                //agent.transform.LookAt(targetRotation);

                if (Vector3.Distance(agent.transform.position, targetRotation) > 7.0f)
                {
                    enemyAI.anim.SetTrigger("Chase");
                }

                //enemyAI.actor.shootAnim.FrameCount

            }
            else
            {
                //agent.GetComponent<AISFM>().singleAnim.SetTrigger("Dead");
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
}
