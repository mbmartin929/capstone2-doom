using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EightDirectionalSpriteSystem
{


    public class Dead : NPCbaseFSM
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("Dead State");

            base.OnStateEnter(animator, stateInfo, layerIndex);
            Debug.Log(agent.name);
            //agent.isStopped = true;

            agent.enabled = false;
            enemyAI.actor.SetCurrentState(DemoActor.State.DIE);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //Debug.Log("Dead State Update");

            // Vector3 targetRotation = new Vector3(playerGo.transform.position.x,
            //                                      agent.transform.position.y,
            //                                      playerGo.transform.position.z);

            // agent.transform.LookAt(targetRotation);
            //LookAwayFrom(targetRotation);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //agent.enabled = true;
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
