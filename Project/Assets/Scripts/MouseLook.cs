using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;

    public Transform playerBody;
    public Transform weapon;

    private float xRoatation = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRoatation -= mouseY;
        xRoatation = Mathf.Clamp(xRoatation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRoatation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        //weapon.Rotate(Vector3.up * mouseX);
    }
}
