using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // private Rigidbody rb;

    // public Transform playerTransform;
    // public float stopDistance = 2.0f;
    // public float movementSpeed = 1.0f;

    // private void Awake()
    // {
    //     rb = GetComponent<Rigidbody>();
    // }

    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     FollowTargetWithRotation(playerTransform, stopDistance, movementSpeed);
    // }

    // private void FollowTargetWithRotation(Transform target, float distanceToStop, float speed)
    // {
    //     if (Vector3.Distance(transform.position, target.position) > distanceToStop)
    //     {
    //         transform.LookAt(target);
    //         rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
    //     }
    // }
    public Transform target;
    public float distanceToStop = 5.0f;

    [SerializeField] private Rigidbody rb;

    [Range(0f, 1f)] public float positionStrength = 1f;
    [Range(0f, 1f)] public float rotationStrength = 1f;

    public Renderer renderer;

    void Awake()
    {
        renderer.receiveShadows = true;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.maxAngularVelocity = 30f; //set it to something pretty high so it can actually follow properly!
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = new Vector3(target.transform.position.x,
                                             transform.position.y,
                                             target.transform.position.z);

        if (Vector3.Distance(transform.position, targetPosition) > distanceToStop)
        {
            Vector3 deltaPos = targetPosition - transform.position;
            rb.velocity = 1f / Time.fixedDeltaTime * deltaPos * Mathf.Pow(positionStrength, 90f * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        Vector3 targetRotation = new Vector3(target.transform.position.x,
                                             transform.position.y,
                                             target.transform.position.z);
        //transform.LookAt(targetRotation);

        LookAwayFrom(targetRotation);
    }

    private void LookAwayFrom(Vector3 point)
    {
        point = 2.0f * transform.position - point;
        transform.LookAt(point);
    }
}
