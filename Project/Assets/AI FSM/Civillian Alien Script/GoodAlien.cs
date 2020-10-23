using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EightDirectionalSpriteSystem;

public class GoodAlien : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    private Transform player;
    public float alienDistanceTORun = 6f;
    public float cryDistance = 4.2f;

    private float timer;
    public float wanderTimer;
    public float wanderRadius;

    public bool isFleeing;

    private DemoActor demoActor;
    public List<Transform> visibleTargets;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private bool isCrying = false;
    private EnemyController enemy;
    private bool isDead = false;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.GetChild(0).GetComponent<EnemyController>();
        demoActor = GetComponent<DemoActor>();
        agent = GetComponent<NavMeshAgent>();
        player = GameManager.Instance.playerGo.transform;
        visibleTargets.Clear();

        isFleeing = false;
        isCrying = false;
        isDead = false;

        timer = wanderTimer;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (enemy.CurHealth <= 0)
        {
            if (!isDead)
            {
                demoActor.SetCurrentState(DemoActor.State.DIE);
                isDead = true;
                agent.isStopped = true;
            }
        }

        if (!isDead)
        {

            if (visibleTargets.Count == 0)
            {
                // Patrolling
                timer += Time.deltaTime;
                if (timer >= wanderTimer)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, 1);
                    agent.SetDestination(newPos);
                    Debug.Log("ROAM");
                    timer = 0;
                }
            }
            else
            {

                // DetectPlayer();

                float distance = Vector3.Distance(player.position, transform.position);
                //Debug.Log("Distance: " + distance);
                if (distance <= cryDistance && isFleeing && !isCrying)
                {
                    Debug.Log("FACING");
                    FacePlayer();
                    isFleeing = false;
                }
                else if (isFleeing && !isCrying)
                {
                    // Chasing
                    RunAway();
                }
                isFleeing = true;
            }
            FindVisibleTargets();
        }
    }

    public void PlayCry()
    {
        if (!isCrying)
        {
            Debug.Log("is Crying");
            demoActor.SetCurrentState(DemoActor.State.SHOOT);
            isCrying = true;
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
                if (distance <= 5 && isFleeing)
                {
                    isFleeing = false;
                    Debug.Log("FACING");
                    FacePlayer();
                }
                else
                {
                    //RunAway();
                }
            }
        }

        FindVisibleTargets();
    }
    void FacePlayer()
    {
        PlayCry();
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 4.2f);
        agent.isStopped = true;

        if (!isFleeing)
        {

        }
    }


    void RunAway()
    {
        Debug.Log("FLEEING");
        isFleeing = true;
        demoActor.SetCurrentState(DemoActor.State.WALKING);
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

    public void FindVisibleTargets()
    {
        //visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    if (target.gameObject.tag == "Player") visibleTargets.Add(target);
                }
            }
        }
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
