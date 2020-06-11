using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Animator anim;
    public GameObject playerGo;
    public float rayCastdistance;
    public GameObject acid;
    public GameObject mouth;
    public float acidSpitrange;

    public float distanceToStop = 3.0f;

    void Awake()
    {
        playerGo = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyRaycast();
    }

    // Used in animation do not delete
    public void damageTo()
    {

    }

    public void Fire()
    {
        GameObject a = Instantiate(acid, mouth.transform.position, mouth.transform.rotation);
        a.GetComponent<Rigidbody>().AddForce(mouth.transform.forward * acidSpitrange);
    }
    public void StopFiring()
    {
        CancelInvoke("fire");
    }
    // public void Firing()
    // {
    //     InvokeRepeating("Fire", 1f, 1f);
    // }

    public void EnemyRaycast()
    {
        RaycastHit hitInfo;

        Ray forwardRay = new Ray(transform.position, transform.forward * rayCastdistance);
        Ray rightRay = new Ray(transform.position, (transform.forward - transform.right) * rayCastdistance);
        Ray leftRay = new Ray(transform.position, (transform.forward - (-transform.right)) * rayCastdistance);

        /// <summary>
        // Draws Green Line for raycast visual aide
        // new vector 3 serves as offset for raycast
        /// </summary>

        Debug.DrawRay(transform.position, transform.forward * rayCastdistance, Color.green);
        Debug.DrawRay(transform.position, (transform.forward - transform.right) * rayCastdistance, Color.green);
        Debug.DrawRay(transform.position, (transform.forward - (-transform.right)) * rayCastdistance, Color.green);

        #region Detects Player
        if (Physics.Raycast(forwardRay, out hitInfo, rayCastdistance))
        {
            PlayerDetection(hitInfo);
        }
        else if (Physics.Raycast(rightRay, out hitInfo, rayCastdistance))
        {
            PlayerDetection(hitInfo);
        }
        else if (Physics.Raycast(leftRay, out hitInfo, rayCastdistance))
        {
            PlayerDetection(hitInfo);
        }
        #endregion

        #region Attacks Player
        Vector3 targetPosition = new Vector3(playerGo.transform.position.x,
                                             transform.position.y,
                                             playerGo.transform.position.z);

        if (Vector3.Distance(transform.position, targetPosition) > distanceToStop)
        {
            anim.SetTrigger("Attack");
        }
        else if (Vector3.Distance(transform.position, targetPosition) < distanceToStop)
        {
            anim.SetTrigger("Chase");
        }
        #endregion


    }

    private void PlayerDetection(RaycastHit hitInfo)
    {
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.tag == "Player")
            {
                Debug.DrawRay(transform.position, transform.forward * rayCastdistance, Color.red);
                anim.SetTrigger("Chase");
            }
            else
            {
                anim.SetTrigger("Patrol");
            }
        }
    }
}
