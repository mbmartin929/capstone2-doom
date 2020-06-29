using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EightDirectionalSpriteSystem
{
    public class EnemyAI : MonoBehaviour
    {
        public DemoActor actor;
        public Animator anim;

        public float rayCastdistance;

        public Transform attackLoc;

        public GameObject bloodSplatter;
        public float distanceToStop = 3.0f;

        public Transform[] waypoints;

        private GameObject playerGo;
        public EnemyController enemyController;

        void Awake()
        {
            //enemyController = GetComponent<EnemyController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            playerGo = GameManager.Instance.playerGo;
            anim = GetComponent<Animator>();

            actor = GetComponent<DemoActor>();
        }

        // Update is called once per frame
        void Update()
        {
            //actor.SetCurrentState(DemoActor.State.WALKING);
            //Debug.Log("Frame Count: " + actor.walkAnim.FrameCount);
        }

        public void Fire()
        {
            GameObject a = Instantiate(enemyController.projectileGo, attackLoc.position, attackLoc.rotation);
            a.GetComponent<Rigidbody>().AddForce(attackLoc.transform.forward * (Time.deltaTime * enemyController.projectileSpeed));
        }

        public void EnemyRaycast()
        {
            //Debug.Log("Enemy Raycast");

            #region  Raycast info
            RaycastHit hitInfoForward;
            RaycastHit hitInfoLeft;
            RaycastHit hitInfoRight;

            Ray forwardRay = new Ray(transform.position, transform.forward * rayCastdistance);
            Ray rightRay = new Ray(transform.position, (transform.forward * 2 - transform.right) * rayCastdistance);
            Ray leftRay = new Ray(transform.position, (transform.forward * 2 - (-transform.right)) * rayCastdistance);
            /// <summary>
            // Draws Green Line for raycast visual aide
            // new vector 3 serves as offset for raycast
            /// </summary>

            Debug.DrawRay(transform.position, transform.forward * rayCastdistance, Color.green);
            Debug.DrawRay(transform.position, (transform.forward * 2 - transform.right) * rayCastdistance, Color.green);
            Debug.DrawRay(transform.position, (transform.forward * 2 - (-transform.right)) * rayCastdistance, Color.green);
            #endregion

            #region Detects Player
            if (Physics.Raycast(forwardRay, out hitInfoForward, rayCastdistance))
            {
                Debug.DrawRay(transform.position, transform.forward * 2 * rayCastdistance, Color.red);
                PlayerDetection(hitInfoForward);
            }
            else if (Physics.Raycast(rightRay, out hitInfoLeft, rayCastdistance))
            {
                Debug.DrawRay(transform.position, (transform.forward * 2 - transform.right) * rayCastdistance, Color.red);
                PlayerDetection(hitInfoLeft);
            }
            else if (Physics.Raycast(leftRay, out hitInfoRight, rayCastdistance))
            {
                Debug.DrawRay(transform.position, (transform.forward * 2 - (-transform.right)) * rayCastdistance, Color.red);
                PlayerDetection(hitInfoRight);
            }
            #endregion

        }

        private void PlayerDetection(RaycastHit hitInfo)
        {
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.tag == "Player")
                {
                    //actor.SetCurrentState(DemoActor.State.WALKING);
                    anim.SetTrigger("Chase");
                }
            }
        }

        public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
        {
            Debug.Log("Random Nav Sphere");

            NavMeshHit navHit;
            Vector3 result;

            Vector3 randomPoint = origin + Random.insideUnitSphere * dist;
            if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
            {
                result = navHit.position;
                //Debug.Log("IF: " + result);
                return result;
            }
            else
            {
                result = randomPoint;
                //Debug.Log("ELSE: " + result);
                return result;
            }
        }
    }
}
