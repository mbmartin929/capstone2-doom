using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TempNavMesh : MonoBehaviour
{
    public float destinationWaitTime = 3f;
    public float stopWaitTime;

    public Transform[] waypoints;

    private NavMeshAgent navMeshAgent;

    public int waypointID;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        waypointID = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();
        InvokeRepeating("GoToDestination", 0.0f, destinationWaitTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    private void GoToDestination()
    {
        //navMeshAgent.destination = RandomNavSphere(transform.position, 10f, -1);
        Debug.Log("TempNavMesh GoToDestination");

        waypointID = Random.Range(0, waypoints.Length);

        navMeshAgent.destination = waypoints[waypointID].position;
    }
}
