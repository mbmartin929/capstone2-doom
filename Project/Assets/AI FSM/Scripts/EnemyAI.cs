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

        [Header("AI Detection")]
        private Camera viewCamera;
        public float distanceToAttack = 16.0f;

        public float viewRadius;
        [Range(0, 360)]
        public float viewAngle;
        public LayerMask targetMask;
        public LayerMask obstacleMask;
        public List<Transform> visibleTargets = new List<Transform>();

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

            viewCamera = Camera.main;

            StartCoroutine("FindTargetsWithDelay", .2f);
        }

        // Update is called once per frame
        void Update()
        {
            //actor.SetCurrentState(DemoActor.State.WALKING);
            //Debug.Log("Frame Count: " + actor.walkAnim.FrameCount);
        }

        private Vector3 PredictedPosition(Vector3 targetPosition, Vector3 shooterPosition, Vector3 targetVelocity, float projectileSpeed)
        {
            Vector3 displacement = targetPosition - shooterPosition;
            float targetMoveAngle = Vector3.Angle(-displacement, targetVelocity) * Mathf.Deg2Rad;

            //if the target is stopping or if it is impossible for the projectile to catch up with the target (Sine Formula)
            if (targetVelocity.magnitude == 0 || targetVelocity.magnitude > projectileSpeed && Mathf.Sin(targetMoveAngle) / projectileSpeed > Mathf.Cos(targetMoveAngle) / targetVelocity.magnitude)
            {
                Debug.Log("Position prediction is not feasible.");
                return targetPosition;
            }
            //also Sine Formula
            float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * targetVelocity.magnitude / projectileSpeed);
            return targetPosition + targetVelocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
        }

        public void WormAttack()
        {
            // float distance = Vector3.Distance(transform.position, playerGo.transform.position);
            // distance /= 5.5f;
            // Debug.Log("Distance: " + distance);

            // GameObject a = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);      
            // a.GetComponent<Rigidbody>().AddForce((transform.forward) * enemyController.projectileSpeed * distance);
            // a.GetComponent<Rigidbody>().AddForce(transform.up * 69);

            // GameObject a1 = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            // a1.GetComponent<Rigidbody>().AddForce((transform.forward + (-transform.right / 2)) * enemyController.projectileSpeed * distance);
            // a1.GetComponent<Rigidbody>().AddForce(transform.up * 69);

            // GameObject a2 = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            // a2.GetComponent<Rigidbody>().AddForce((transform.forward + (transform.right / 2)) * enemyController.projectileSpeed * distance);
            // a2.GetComponent<Rigidbody>().AddForce(transform.up * 69);

            GameObject a1 = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            a1.GetComponent<Projectile>().enemyAI = this;
            a1.GetComponent<Projectile>().LaunchProjectile1();

            GameObject a2 = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            a2.GetComponent<Projectile>().enemyAI = this;
            a2.GetComponent<Projectile>().LaunchProjectile2();

            GameObject a3 = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            a3.GetComponent<Projectile>().enemyAI = this;
            a3.GetComponent<Projectile>().LaunchProjectile3();

            float distance;
            distance = Vector3.Distance(transform.position, playerGo.transform.position);
            //Debug.Log("Before Distance: " + distance);

            if (distance <= 5.5f)
            {
                Debug.Log("Short Distance");
                distance *= -30.0f;
            }
            else
            {
                Debug.Log("Long Distance");
                distance *= 35.0f;
            }
            //Debug.Log("After Distance: " + distance);
            Debug.Log("Distance: " + distance);

            // GameObject a = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            // a.GetComponent<Rigidbody>().AddForce((transform.forward) * enemyController.projectileSpeed);
            // a.GetComponent<Rigidbody>().AddForce(transform.up * (69 + distance));

            // GameObject a1 = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            // a1.GetComponent<Rigidbody>().AddForce((transform.forward + (-transform.right / 2)) * enemyController.projectileSpeed);
            // a1.GetComponent<Rigidbody>().AddForce(transform.up * (69 + distance));

            // GameObject a2 = Instantiate(enemyController.projectileGo, attackLoc.position, transform.rotation);
            // a2.GetComponent<Rigidbody>().AddForce((transform.forward + (transform.right / 2)) * enemyController.projectileSpeed);
            // a2.GetComponent<Rigidbody>().AddForce(transform.up * (69 + distance));
        }

        public void EnemyRaycast()
        {
            //Debug.Log("Enemy Raycast");

            // #region  Raycast info
            // RaycastHit hitInfoForward;
            // RaycastHit hitInfoLeft;
            // RaycastHit hitInfoRight;

            // Ray forwardRay = new Ray(transform.position, transform.forward * rayCastdistance);
            // Ray rightRay = new Ray(transform.position, (transform.forward * 2 - transform.right) * rayCastdistance);
            // Ray leftRay = new Ray(transform.position, (transform.forward * 2 - (-transform.right)) * rayCastdistance);
            // /// <summary>
            // // Draws Green Line for raycast visual aide
            // // new vector 3 serves as offset for raycast
            // /// </summary>

            // Debug.DrawRay(transform.position, transform.forward * rayCastdistance, Color.green);
            // Debug.DrawRay(transform.position, (transform.forward * 2 - transform.right) * rayCastdistance, Color.green);
            // Debug.DrawRay(transform.position, (transform.forward * 2 - (-transform.right)) * rayCastdistance, Color.green);
            // #endregion

            // #region Detects Player
            // if (Physics.Raycast(forwardRay, out hitInfoForward, rayCastdistance))
            // {
            //     Debug.DrawRay(transform.position, transform.forward * 2 * rayCastdistance, Color.red);
            //     PlayerDetection(hitInfoForward);
            // }
            // else if (Physics.Raycast(rightRay, out hitInfoLeft, rayCastdistance))
            // {
            //     Debug.DrawRay(transform.position, (transform.forward * 2 - transform.right) * rayCastdistance, Color.red);
            //     PlayerDetection(hitInfoLeft);
            // }
            // else if (Physics.Raycast(leftRay, out hitInfoRight, rayCastdistance))
            // {
            //     Debug.DrawRay(transform.position, (transform.forward * 2 - (-transform.right)) * rayCastdistance, Color.red);
            //     PlayerDetection(hitInfoRight);
            // }
            // #endregion

            if (visibleTargets.Count != 0)
            {
                anim.SetTrigger("Chase");
            }

        }

        IEnumerator FindTargetsWithDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                FindVisibleTargets();
            }
        }

        void FindVisibleTargets()
        {
            visibleTargets.Clear();
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        visibleTargets.Add(target);
                    }
                }
            }
        }

        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
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
            //Debug.Log("Random Nav Sphere");

            NavMeshHit navHit;
            Vector3 result;

            Vector3 randomPoint = origin + Random.insideUnitSphere * dist;
            if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, 8))
            {
                result = navHit.position;
                //Debug.Log("IF: " + result);
                return result;
            }
            else
            {
                // while (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, 8))
                // {
                //     result = randomPoint;
                //     //Debug.Log("ELSE: " + result);
                //     return result;
                // }

                //result = origin;
                result = randomPoint;
                return result;
            }
        }

        public void GetNewDir()
        {
            try
            {
                actor.SetCurrentState(DemoActor.State.WALKING);
                Vector3 newPos = EnemyAI.RandomNavSphere(transform.position, 4.0f, 0);
                GetComponent<NavMeshAgent>().SetDestination(newPos);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
                GetComponent<NavMeshAgent>().SetDestination(transform.position);
            }


            StartCoroutine(ActorAttack());
        }

        private IEnumerator ActorAttack()
        {
            yield return new WaitForSeconds(1.0f);
            if (enemyController.IsDead())
            {

            }
            else actor.SetCurrentState(DemoActor.State.SHOOT);
        }
    }
}
