using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EightDirectionalSpriteSystem
{


    public class WeaponSwitching : MonoBehaviour
    {
        public int selectedWeapon = 0;

        int previousSelectedWeapon;

        // Start is called before the first frame update
        void Start()
        {
            //SelectWeapon();
        }

        // Update is called once per frame
        void Update()
        {
            previousSelectedWeapon = selectedWeapon;

            // if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            // {
            //     if (selectedWeapon >= transform.childCount - 1)
            //     {
            //         selectedWeapon = 0;
            //     }
            //     else
            //     {
            //         selectedWeapon++;
            //     }
            // }
            // if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            // {
            //     if (selectedWeapon <= 0)
            //     {
            //         selectedWeapon = transform.childCount - 1;
            //     }
            //     else
            //     {
            //         selectedWeapon--;
            //     }
            // }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedWeapon = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedWeapon = 1;
            }

            if (previousSelectedWeapon != selectedWeapon)
            {
                SelectWeapon();
            }
        }

        private void SelectWeapon()
        {
            int i = 0;
            foreach (Transform currentWeapon in transform)
            {
                if (i == selectedWeapon)
                {
                    //weapon.gameObject.GetComponent<WeaponController>().SwitchTo();
                    //weapon.gameObject.SetActive(true);

                    //StartCoroutine(SwitchIENumerator(0.30f, weapon, true));

                    if (!currentWeapon.gameObject.activeSelf)
                    {
                        //Debug.Log(previousSelectedWeapon);

                        // INSERT CODE HERE TO CHECK IF THERE IS AN ACTION PLAYING

                        Transform previousWeapon = transform.GetChild(previousSelectedWeapon);
                        previousWeapon.GetComponent<WeaponController>().SwitchAway();

                        StartCoroutine(SwitchIENumerator(0.1f, currentWeapon, true, true));
                        StartCoroutine(SwitchIENumerator(0.1f, previousWeapon, false, false));
                        return;
                    }
                    //weapon.gameObject.GetComponent<WeaponController>().SwitchTo();
                }
                else
                {
                    //StartCoroutine(SwitchIENumerator(0.30f, weapon, false));
                    //weapon.gameObject.GetComponent<WeaponController>().SwitchAway();
                }
                i++;
            }
        }

        private IEnumerator SwitchIENumerator(float seconds, Transform weapon, bool active, bool switchTo)
        {
            yield return new WaitForSeconds(seconds);
            weapon.gameObject.SetActive(active);

            if (switchTo)
            {
                weapon.GetComponent<WeaponController>().SwitchTo();
            }
        }
    }
}