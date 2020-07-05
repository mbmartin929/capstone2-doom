using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForward : MonoBehaviour
{
    public bool rotateTowardsTarget = true;

    public float speed = 10.0f;

    //values that will be set in the Inspector
    public Transform Target;
    public float rotationSpeed = 1.0f;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameManager.Instance.playerGo.transform;

        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

        if (rotateTowardsTarget)
        {
            //find the vector pointing from our position to the target
            _direction = (Target.position - transform.position).normalized;

            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(_direction);

            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level")
        {
            Destroy(gameObject, 0f);
        }
        else if (other.tag == "Player")
        {
            Debug.Log("Hit Player");
        }
    }
}
