using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EightDirectionalSpriteSystem
{
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

            //Debug.Log("Patrol State");


            if (enemyAI.billboard.enemy == ActorBillboard.Enemy.Spider)
            {
                //Debug.Log(enemyAI.actor.name);
            }
            enemyAI.actor.SetCurrentState(DemoActor.State.WALKING);

        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!enemyController.IsDead())
            {
                if (Time.time > nextActionTime)
                {
                    nextActionTime += period;
                    //Debug.Log("Stop Wait Time");
                    //AISFM.GoToDestination();

                    Vector3 newPos = EnemyAI.RandomNavSphere(NPC.transform.position, 5.0f, 0);
                    //Debug.Log(NPC.gameObject.name + ": " + NPC.transform.position + "," + newPos);

                    //Vector3 newPos = Random.insideUnitSphere * 1.0f;
                    agent.SetDestination(newPos);

                    enemyAI.CallRandomPatrolSound();
                }

                float dist = agent.remainingDistance;
                if (dist != Mathf.Infinity && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
                {
                    //Debug.Log("Reached Destination");
                    enemyAI.actor.SetCurrentState(DemoActor.State.IDLE);
                    //Arrived.
                }
                else enemyAI.actor.SetCurrentState(DemoActor.State.WALKING);

                // if (Vector3.Distance(NPC.transform.position, GameManager.Instance.playerGo.transform.position) < 5)
                // {
                //     //Debug.Log("Hi");
                //     animator.SetTrigger("Get Hit");
                //     enemyAI.actor.SetCurrentState(DemoActor.State.SHOOT);
                // }

                enemyAI.EnemyRaycast();
                enemyAI.FindVisibleTargets();


                //AISFM.EnemyRaycast();
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}