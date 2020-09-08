using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EightDirectionalSpriteSystem
{
    public class NPCbaseFSM : StateMachineBehaviour
    {
        public GameObject NPC;
        public GameObject playerGo;

        public float accuracy = 2.0f;
        public NavMeshAgent agent;

        protected EnemyController enemyController;

        protected AISFM AISFM;
        protected EnemyAI enemyAI;

        public bool canAttack;

        void Awake()
        {

        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            NPC = animator.gameObject;

            //Debug.Log(animator.gameObject.name);

            agent = NPC.GetComponent<NavMeshAgent>();

            AISFM = NPC.GetComponent<AISFM>();
            enemyAI = NPC.GetComponent<EnemyAI>();

            playerGo = GameManager.Instance.playerGo;

            //enemyController = enemyAI.enemyController;
            enemyController = NPC.transform.GetChild(0).GetComponent<EnemyController>();
        }
    }
}
