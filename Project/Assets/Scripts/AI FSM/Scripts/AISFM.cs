using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace EightDirectionalSpriteSystem
{
    public class AISFM : MonoBehaviour
    {
        #region  Animator Variables
        public Transform[] directionalGos;
        public List<Animator> directionalAnim;
        public Animator singleAnim;
        public Transform[] waypoints;
        #endregion

        #region Ranged Variables
        public Transform projectileSpawnLoc;
        public float projectileSpeed = 3.0f;
        public float distanceToStop = 4.0f;
        #endregion

        #region Other Variables
        //public int directionID = 0;
        public int waypointID;
        public float rayCastdistance = 10.0f;
        public float stopWaitTime;
        public bool tempPatrol = false;
        public int tempID = 1;
        #endregion

        #region  Private Variables
        private GameObject playerGo;
        private EnemyController enemyController;
        private NavMeshAgent navMeshAgent;
        //public bool isPatrolling;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            //InvokeRepeating("ChangeID", 0f, 1.5f);

            waypointID = 0;
            playerGo = GameManager.Instance.playerGo;
            singleAnim = GetComponent<Animator>();
            enemyController = GetComponent<EnemyController>();
            navMeshAgent = GetComponent<NavMeshAgent>();

            foreach (Transform child in directionalGos[0])
            {
                Animator animator = child.GetComponent<Animator>();
                directionalAnim.Add(animator);
            }
            foreach (Transform child in directionalGos[1])
            {
                Animator animator = child.GetComponent<Animator>();
                directionalAnim.Add(animator);
            }
            foreach (Transform child in directionalGos[2])
            {
                Animator animator = child.GetComponent<Animator>();
                directionalAnim.Add(animator);
            }
            foreach (Transform child in directionalGos[3])
            {
                Animator animator = child.GetComponent<Animator>();
                directionalAnim.Add(animator);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (navMeshAgent.velocity == Vector3.zero)
            {
                // Debug.Log("Reached!");

                foreach (Animator item in directionalAnim)
                {
                    // Debug.Log("Pause!");
                    item.speed = 0.0f;
                }
            }
            else
            {
                foreach (Animator item in directionalAnim)
                {
                    // Debug.Log("Resume!");
                    item.speed = 0.5f;
                }
            }

        }

        void FixedUpdate()
        {
            // if (GetComponent<SpriteRenderer>().enabled == true)
            // {
            //     GetComponent<EnemyDirectionalSprite>().isOn = false;
            // }
            // else if (GetComponent<SpriteRenderer>().enabled == false)
            // {
            //     GetComponent<EnemyDirectionalSprite>().isOn = true;
            // }
        }

        public void DisableEightDirection()
        {
            GetComponent<LooksAtPlayer>()._LookMode = LooksAtPlayer.LookMode.LookAway;
            GetComponent<EnemyDirectionalSprite>().isOn = false;
            GetComponent<MeshRenderer>().enabled = true;
            // Debug.Log("Disable Eight Direction");
            // GetComponent<EnemyDirectionalSprite>().enabled = false;

            GameObject[] faces = GetComponent<EnemyDirectionalSprite>().faces;
            foreach (GameObject item in faces)
            {
                item.SetActive(false);
            }
        }

        public void EnableEightDirection()
        {
            GetComponent<LooksAtPlayer>()._LookMode = LooksAtPlayer.LookMode.LookTowards;
            // Debug.Log("Enable Eight Direction");
            //GetComponent<EnemyDirectionalSprite>().enabled = true;

            GetComponent<EnemyDirectionalSprite>().isOn = true;
            GetComponent<MeshRenderer>().enabled = false;
        }

        public void EnableSingleAnim()
        {

        }

        public void Fire()
        {
            GameObject a = Instantiate(enemyController.projectileGo, projectileSpawnLoc.transform.position, projectileSpawnLoc.transform.rotation);
            a.GetComponent<Rigidbody>().AddForce(projectileSpawnLoc.transform.forward * projectileSpeed);
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
                //Debug.Log("Player Detected");
                PlayerDetection(hitInfoForward);
            }
            else if (Physics.Raycast(rightRay, out hitInfoLeft, rayCastdistance))
            {
                Debug.DrawRay(transform.position, (transform.forward * 2 - transform.right) * rayCastdistance, Color.red);
                //Debug.Log("Player Detected");
                PlayerDetection(hitInfoLeft);
            }
            else if (Physics.Raycast(leftRay, out hitInfoRight, rayCastdistance))
            {
                Debug.DrawRay(transform.position, (transform.forward * 2 - (-transform.right)) * rayCastdistance, Color.red);
                //Debug.Log("Player Detected");
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
                    Debug.Log("Set Trigger CHASE");
                    singleAnim.SetTrigger("Chase");
                }
                else
                {
                    singleAnim.SetTrigger("Patrol");
                }
            }
        }

        Transform GetClosestDirection(Transform[] directions, Transform fromThis)
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = fromThis.position;
            foreach (Transform potentialTarget in directions)
            {
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
            return bestTarget;
        }

        public void GoToDestination()
        {
            EnableEightDirection();

            waypointID = Random.Range(0, waypoints.Length);

            navMeshAgent.destination = waypoints[waypointID].position;
        }

        public void ChasePatrol(ref bool tempPatrol, ref bool oneTime)
        {
            EnableEightDirection();

            //waypointID = Random.Range(0, waypoints.Length);

            Transform closest = GetClosestDirection(waypoints, playerGo.transform);

            tempID = closest.GetComponent<DirectionID>().directionID;

            Debug.Log("Direction ID: " + tempID);

            navMeshAgent.destination = closest.position;

            StartCoroutine(WaitChase(tempPatrol, oneTime));

            //Debug.Log("Inside Void " + tempPatrol);
        }

        private IEnumerator WaitChase(bool tempPatrol, bool oneTime)
        {
            oneTime = true;
            yield return new WaitForSeconds(1.25f);

            tempPatrol = false;
            oneTime = false;
        }

        private void ChangeID()
        {
            int sameID = tempID;

            tempID = Random.Range(0, waypoints.Length);

            if (tempID == sameID) tempID = Random.Range(0, waypoints.Length);

            //Debug.Log("TempID: " + tempID);
        }
    }
}