using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsCamera : MonoBehaviour
{
    private float timer = 0.0f;
    public float bobbingSpeed = 0.2f;
    public float bobbingAmount = 0.08f;
    public float midPoint = 1.0f;

    void Awake()
    {
        //mainCamera = Camera.main;


    }

    void Update()
    {
        // Vector3 cameraPosition = mainCamera.transform.position;
        // cameraPosition.y = 0f;
        // Vector3 position = transform.position;
        // position.y = 0f;

        // Vector3 dirToCamera = (cameraPosition - position).normalized;
        // float angleToCamera = GetAngleFromVectorFloat(dirToCamera);
        // transform.eulerAngles = new Vector3(0f, -angleToCamera + 90 + 180, 0f);

        // float waveSlice = 0.0f;
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");

        // if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        // {
        //     timer = 0.0f;
        // }
        // else
        // {
        //     waveSlice = Mathf.Sin(timer);
        //     timer += bobbingSpeed;

        //     if (timer > Mathf.PI * 2)
        //     {
        //         timer = timer - (Mathf.PI * 2);
        //     }
        // }

        // if (waveSlice != 0)
        // {
        //     float translateChange = waveSlice * bobbingAmount;
        //     float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        //     totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
        //     translateChange = totalAxes * translateChange;
        //     transform.localPosition.y = midPoint + translateChange;
        //     transform.localPosition.x = midPoint + translateChange;
        // }
        // else
        // {
        //     transform.localPosition.y = midPoint;
        //     transform.localPosition.x = midPoint;
        // }
    }

    // public static float GetAngleFromVectorFloat(Vector3 dir)
    // {
    //     dir = dir.normalized;
    //     float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
    //     if (n < 0) n += 360;

    //     return n;
    // }
}
