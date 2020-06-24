using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISFM : MonoBehaviour
{
    #region  Animator Variables
    public Animator[] directionalAnim;
    public Animator singleAnim;
    public Transform[] waypoints;
    #endregion

    #region Ranged Variables
    public GameObject projectielGo;
    public Transform projectileSpawnLoc;
    public float projectileSpeed = 3.0f;
    public float distanceToStop = 4.0f;
    #endregion

    #region Other Variables
    public float rayCastdistance = 10.0f;
    public float stopWaitTime;
    #endregion

    #region  Private Variables
    private GameObject playerGo;
    private EnemyController enemyController;
    private NavMeshAgent navMeshAgent;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerGo = GameManager.Instance.playerGo;
        singleAnim = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire()
    {
        GameObject a = Instantiate(projectielGo, projectileSpawnLoc.transform.position, projectileSpawnLoc.transform.rotation);
        a.GetComponent<Rigidbody>().AddForce(projectileSpawnLoc.transform.forward * projectileSpeed);
    }

    public void EnemyRaycast()
    {
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
                singleAnim.SetTrigger("Chase");
            }
            else
            {
                singleAnim.SetTrigger("Patrol");
            }
        }
    }
}
