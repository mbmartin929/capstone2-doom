using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public float lerpSpeed = 10f;
    public Vector3 offset;

    public float targetOffsetX = 0.001f;

    private float newOffsetX;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

        //Vector3 newTarget = new Vector3(-target.position.x, -target.position.y, -target.position.z);
        //transform.LookAt(newTarget);

        //Look Left
        if (Input.GetAxis("Mouse X") < 0)
        {
            Debug.Log("Look Left");

            // if (newOffsetX != -targetOffsetX)
            // {
            //     Debug.Log("Left offset");
            //     newOffsetX = Mathf.Lerp(0, -targetOffsetX, lerpSpeed * Time.deltaTime);
            //     offset.x = newOffsetX;
            // }

            newOffsetX = Mathf.Lerp(0, -targetOffsetX, lerpSpeed * Time.deltaTime);
            offset.x = newOffsetX;
        }
        //Look Right
        else if (Input.GetAxis("Mouse X") > 0)
        {
            Debug.Log("Look Right");

            // if (newOffsetX != targetOffsetX)
            // {
            //     Debug.Log("Right offset");
            //     newOffsetX = Mathf.Lerp(0, targetOffsetX, lerpSpeed * Time.deltaTime);
            //     offset.x = newOffsetX;
            // }

            newOffsetX = Mathf.Lerp(0, targetOffsetX, lerpSpeed * Time.deltaTime);
            offset.x = newOffsetX;
        }
        // Look Straight
        else
        {
            Debug.Log("Look Straight");

            offset.x = Mathf.Lerp(targetOffsetX, 0, lerpSpeed * Time.deltaTime);
        }
    }
}

