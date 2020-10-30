using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDirectionalSprite : MonoBehaviour
{
    public GameObject rotGo;
    public GameObject[] facesGo;

    public Vector3[] rot;
    public Vector3[] faces;

    private float angleX, angleY, angleZ;

    private Vector3 currentAngle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // angleX = transform.rotation.eulerAngles.x;
        // angleY = transform.rotation.eulerAngles.y;
        // angleZ = transform.rotation.eulerAngles.z;

        // Vector3 objectRotation = new Vector3(angleX, angleY, angleZ);

        // transform.eulerAngles = Vector3.Lerp(objectRotation, faces[0], 0.1f);

        //currentAngle = transform.eulerAngles;

        // currentAngle = new Vector3(
        // Mathf.LerpAngle(rotGo.transform.rotation.x, rot[0].x, Time.deltaTime),
        // Mathf.LerpAngle(rotGo.transform.rotation.y, rot[0].y, Time.deltaTime),
        // Mathf.LerpAngle(rotGo.transform.rotation.z, rot[0].z, Time.deltaTime));

        // rotGo.transform.eulerAngles = currentAngle;

        // foreach (GameObject item in facesGo)
        // {
        //     Vector3 facesAngle = new Vector3(
        //     Mathf.LerpAngle(item.transform.rotation.x, faces[0].x, Time.deltaTime),
        //     Mathf.LerpAngle(item.transform.rotation.y, faces[0].y, Time.deltaTime),
        //     Mathf.LerpAngle(item.transform.rotation.z, faces[0].z, Time.deltaTime));

        //     item.transform.eulerAngles = facesAngle;
        // }

        // if (Input.GetKeyDown(KeyCode.Keypad0))
        // {
        //     //rotGo.transform.rotation = Quaternion.Euler(rot[0].x, rot[0].y, rot[0].z);
        //     rotGo.transform.eulerAngles = Vector3.Lerp(rotGo.transform.eulerAngles, rot[0], Time.deltaTime * 100);

        //     foreach (GameObject item in facesGo)
        //     {
        //         item.transform.eulerAngles = Vector3.Lerp(item.transform.eulerAngles, faces[0], Time.deltaTime * 100);
        //     }
        // }
        // if (Input.GetKeyDown(KeyCode.Keypad1))
        // {
        //     rotGo.transform.eulerAngles = Vector3.Lerp(rotGo.transform.eulerAngles, rot[1], Time.deltaTime * 100);

        //     foreach (GameObject item in facesGo)
        //     {
        //         item.transform.eulerAngles = Vector3.Lerp(item.transform.eulerAngles, faces[1], Time.deltaTime * 100);
        //     }
        // }

        // if (Input.GetKeyDown(KeyCode.Keypad0))
        // {
        //     rotGo.transform.Rotate(rot[0]);

        //     foreach (GameObject item in facesGo)
        //     {
        //         item.transform.Rotate(faces[0], Space.Self);
        //     }
        // }
        // if (Input.GetKeyDown(KeyCode.Keypad1))
        // {
        //     rotGo.transform.Rotate(rot[1]);

        //     foreach (GameObject item in facesGo)
        //     {
        //         item.transform.Rotate(faces[1], Space.Self);
        //     }
        // }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.forward = new Vector3(10, 0, 0);
        }

    }
}
