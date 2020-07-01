using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;

    public Transform playerBody;
    //public Transform weapon;

    private float xRoatation = 0.0f;

    public float headbobSpeed = 1f;
    public float headbobStepCounter;
    public float headbobAmountX = 1f;
    public float headbobAmountY = 1f;
    public Vector3 parentLastPos;
    public float eyeHeightRation = 0.9f;
    public float currentAimRatio = -1f;

    void Awake()
    {
        parentLastPos = transform.parent.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.enableHeadBob)
        {
            headbobStepCounter += Vector3.Distance(parentLastPos, transform.parent.position) * headbobSpeed;

            float newX = Mathf.Sin(headbobStepCounter) * headbobAmountX * currentAimRatio;
            float newY = (Mathf.Cos(headbobStepCounter * 2f) * headbobAmountY * -1 * currentAimRatio) + (transform.parent.localScale.y * eyeHeightRation) - (transform.parent.localScale.y / 2);

            transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);

            parentLastPos = transform.parent.position;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRoatation -= mouseY;
        xRoatation = Mathf.Clamp(xRoatation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRoatation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        //weapon.Rotate(Vector3.up * mouseX);


    }
}
