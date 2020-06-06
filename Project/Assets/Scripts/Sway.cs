using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    public float intensity;
    public float smooth;

    private Quaternion originRotation;
    private Vector3 originPosition;

    // Start is called before the first frame update
    private void Start()
    {
        originRotation = transform.localRotation;
        originPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        //UpdateSway();
        TransformSway();
    }

    private void UpdateSway()
    {
        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");

        Quaternion xAdj = Quaternion.AngleAxis(intensity * xMouse, Vector3.up);
        Quaternion yAdj = Quaternion.AngleAxis(intensity * yMouse, Vector3.up);
        Quaternion targetRotation = originRotation * xAdj * yAdj;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }

    private void TransformSway()
    {
        float xMouse = Input.GetAxis("Mouse X");
        float yMouse = Input.GetAxis("Mouse Y");

        Quaternion xAdj = Quaternion.AngleAxis(intensity * xMouse, Vector3.up);
        // Quaternion yAdj = Quaternion.AngleAxis(intensity * yMouse, Vector3.up);

        // Vector3 xAdjj =

        Vector3 targetPosition = originPosition * xMouse * yMouse;

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smooth);
    }
}
