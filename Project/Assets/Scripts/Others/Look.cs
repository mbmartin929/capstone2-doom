using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    #region Variables
    public static bool cursorLocked = true;

    public Transform player;
    public Transform cams;
    public Transform weapon;

    public float xSensitivity;
    public float ySensitivity;
    public float maxAngle;

    private Quaternion camCenter;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        camCenter = cams.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        SetY();
        SetX();

        UpdateCursorLock();
    }

    void SetY()
    {
        float input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        Quaternion adj = Quaternion.AngleAxis(input, -Vector3.right);
        Quaternion delta = cams.localRotation * adj;

        if (Quaternion.Angle(camCenter, delta) < maxAngle)
        {
            cams.localRotation = delta;
        }

        weapon.rotation = delta;
    }

    void SetX()
    {
        float input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        Quaternion adj = Quaternion.AngleAxis(input, -Vector3.up);
        Quaternion delta = cams.localRotation * adj;

        if (Quaternion.Angle(camCenter, delta) < maxAngle)
        {
            cams.localRotation = delta;
        }
    }

    void UpdateCursorLock()
    {
        // if (cursorLocked)
        // {
        //     Cursor.locState = CursorLockMode.Locked;
        //     Cursor.visible = false;
        // }
    }
}
