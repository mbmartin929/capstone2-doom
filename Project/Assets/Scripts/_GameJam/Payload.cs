using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payload : MonoBehaviour
{
    public GameObject[] waypoints;
    public float startTime = 2.9f;
    private int current = 0;
    public float rotSpeed;
    public float moveSpeed = 1.0f;
    float waypointRadius = 1f;

    private bool startMove = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WillMove(startTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (startMove)
        {
            if (Vector3.Distance(waypoints[current].transform.position, transform.position) < waypointRadius)
            {
                current++;
                if (current >= waypoints.Length) current = 0;
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * moveSpeed);

            Vector3 targetDirection = waypoints[current].transform.position - transform.position;

            float singleStep = rotSpeed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.5f);

            transform.localRotation = Quaternion.LookRotation(newDirection);
        }
    }

    private IEnumerator WillMove(float time)
    {
        yield return new WaitForSeconds(time);
        startMove = true;
    }
}
