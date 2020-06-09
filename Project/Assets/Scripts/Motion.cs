using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    #region  Variables

    public Transform weaponParent;

    private Vector3 weaponParentOrigin;

    private Vector3 targetWeaponBobPosition;
    private float movementCounter;
    private float idleCounter;
    private float baseFOV;

    public float speed = 8f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        weaponParentOrigin = weaponParent.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        if (horizontalMove == 0 && verticalMove == 0)
        {
            HeadBob(idleCounter, 0.025f, 0.025f);
            idleCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 4f);
        }
        else
        {
            HeadBob(movementCounter, 45.0f, 29.75f);
            movementCounter += Time.deltaTime * speed;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 8f);
        }


    }

    #region  Private Methods

    void HeadBob(float z, float x_intensity, float y_intensity)
    {
        targetWeaponBobPosition = weaponParentOrigin + new Vector3(Mathf.Cos(z) * x_intensity, Mathf.Sin(z * 2) * y_intensity, 0);
    }

    #endregion
}
