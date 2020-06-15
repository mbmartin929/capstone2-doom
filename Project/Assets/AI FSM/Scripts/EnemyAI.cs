using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator anim;
    public GameObject playerGo;
    public float rayCastdistance;


    public GameObject acid;
    public GameObject mouth;
    public float acidSpitrange;


    public GameObject bloodSplatter;
    public float distanceToStop = 3.0f;

    public Transform[] waypoints;

    private EnemyController enemyController;

    void Awake()
    {
        playerGo = GameObject.FindGameObjectWithTag("Player");
        enemyController = GetComponent<EnemyController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire()
    {
        GameObject a = Instantiate(acid, mouth.transform.position, mouth.transform.rotation);
        a.GetComponent<Rigidbody>().AddForce(mouth.transform.forward * acidSpitrange);
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
                anim.SetTrigger("Chase");
            }
            else
            {
                anim.SetTrigger("Patrol");
            }
        }
    }
}
