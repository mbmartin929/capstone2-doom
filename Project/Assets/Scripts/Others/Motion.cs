using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public float divisible = 8f;

    #region  Variables

    public Transform weaponParent;

    private Vector3 weaponParentOrigin;

    private Vector3 targetWeaponBobPosition;
    [SerializeField] private float movementCounter;
    private float idleCounter;
    private float baseFOV;

    #endregion

    public AudioClip[] footsteps;
    private AudioSource audioSource;

    private int f = 0;
    private int lastf = 0;

    // Start is called before the first frame update
    void Start()
    {
        weaponParentOrigin = weaponParent.localPosition;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        if (horizontalMove == 0 && verticalMove == 0)
        {
            HeadBob(idleCounter, 0.0f, 0.00f);
            idleCounter += Time.deltaTime;
            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 1.29f);

            //Debug.Log("Idle Counter");
            //Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, targetWeaponBobPosition, Time.deltaTime * 4f);
        }
        else
        {
            HeadBob(movementCounter, 69.0f, 20.0f);
            movementCounter += Time.deltaTime * 9f;

            //Debug.Log("Else");

            // Debug.Log((int)f);
            // if ((int)f % 2 == 0)
            // {
            //     audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);

            // }

            f = (int)(movementCounter / divisible);

            if (lastf != f)
            {
                //audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);
                lastf = f;
            }

            weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 8f);

            //Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, targetWeaponBobPosition, Time.deltaTime * 8f);
        }


    }

    #region  Private Methods

    void HeadBob(float z, float x_intensity, float y_intensity)
    {
        targetWeaponBobPosition = weaponParentOrigin + new Vector3(Mathf.Cos(z) * x_intensity, Mathf.Sin(z * 2) * y_intensity, 0);
    }

    #endregion
}
