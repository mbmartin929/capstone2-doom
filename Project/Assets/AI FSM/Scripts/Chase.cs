using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EightDirectionalSpriteSystem
{
    public class Chase : NPCbaseFSM
    {
        private float nextActionTime = 1.0f;
        private float period = 0.0f;

        private bool oneTime = false;

        private float tempTime;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            if (!agent.enabled) return;
            else agent.isStopped = false;

            //Debug.Log("Chase State");

            //AISFM.DisableEightDirection();
            enemyAI.actor.SetCurrentState(DemoActor.State.WALKING);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!enemyController.IsDead())
            {
                if (enemyAI.billboard.enemy == ActorBillboard.Enemy.Worm)
                {
                    agent.SetDestination(playerGo.transform.position);

                    Vector3 targetPosition = new Vector3(playerGo.transform.position.x,
                                                         agent.transform.position.y,
                                                         playerGo.transform.position.z);

                    if (Vector3.Distance(agent.transform.position, targetPosition) <= enemyAI.distanceToAttack - 1.0f)
                    {
                        enemyAI.anim.SetTrigger("Attack");
                    }
                }
                else if (enemyAI.billboard.enemy == ActorBillboard.Enemy.GreenSlime)
                {
                    enemyAI.actor.SetCurrentState(DemoActor.State.SHOOT);
                }
                else if (enemyAI.billboard.enemy == ActorBillboard.Enemy.RedSlime)
                {
                    enemyAI.actor.SetCurrentState(DemoActor.State.SHOOT);
                }
                else if (enemyAI.billboard.enemy == ActorBillboard.Enemy.Spider)
                {
                    agent.SetDestination(playerGo.transform.position);

                    Vector3 targetPosition = new Vector3(playerGo.transform.position.x,
                                                         agent.transform.position.y,
                                                         playerGo.transform.position.z);

                    if (Vector3.Distance(agent.transform.position, targetPosition) <= enemyAI.distanceToAttack - 0.5f)
                    {
                        enemyAI.anim.SetTrigger("Attack");
                        //Debug.Log("Spider Attack");
                        //.actor.SetCurrentState(DemoActor.State.SHOOT);
                    }
                }
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