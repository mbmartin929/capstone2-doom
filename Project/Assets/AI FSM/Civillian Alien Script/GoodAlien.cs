using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GoodAlien : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent agent;
    public Transform player;
    public float alienDistanceTORun = 6f;

    //public float range;


    private float timer;
    public float wanderTimer;
    public float wanderRadius;

    public bool isFleeing;

    // Start is called before the first frame update
    void Start()
    {
        isFleeing = false;
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, 1);
            agent.SetDestination(newPos);
            timer = 0;
        }
        else
        {
            isFleeing = true;
            DetectPlayer();
        }



    }


    void DetectPlayer()
    {

        RaycastHit hit;
        Vector3 rayDirection = player.position - transform.position;
        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        float distance = Vector3.Distance(player.position, transform.position);
        if (Physics.Raycast(transform.position, rayDirection, out hit, 10))
        {
            if (hit.transform == player)
            {

                Debug.Log("FLEEING");
                RunAway();
                if (distance <= 5)
                {
                    isFleeing = false;
                    Debug.Log("ENEMY DETECTED");
                    FacePlayer();
                }


            }

        }

    }
    void FacePlayer()
    {
        if (!isFleeing)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            agent.isStopped = true;
        }


    }


    void RunAway()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < alienDistanceTORun)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;

            agent.SetDestination(newPos);
        }
        //Vector3 flee = transform.position + ((transform.position - player.position)) * multiplier;
        //float distance = Vector3.Distance(transform.position, player.position);
        //if (distance < range) agent.SetDestination(flee);


    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}
