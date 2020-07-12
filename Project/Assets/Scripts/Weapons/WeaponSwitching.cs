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

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                {
                    selectedWeapon = 0;
                }
                else
                {
                    selectedWeapon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon <= 0)
                {
                    selectedWeapon = transform.childCount - 1;
                }
                else
                {
                    selectedWeapon--;
                }
            }

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
                Transform previousWeapon = transform.GetChild(previousSelectedWeapon);

                // if (i != selectedWeapon)
                // {
                //     for (int x = 0; x <= transform.childCount - 1; x++)
                //     {
                //         if (transform.GetChild(x).gameObject.activeSelf)
                //         {
                //             //Debug.Log("X: " + x);

                //             i = (x);
                //         }
                //     }
                // }
                // Debug.Log("SelectedWeapon: " + selectedWeapon);

                if (previousWeapon.GetComponent<WeaponController>().anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                {
                    selectedWeapon = previousSelectedWeapon;
                }

                if (i == selectedWeapon)
                {
                    if (!currentWeapon.gameObject.activeSelf)
                    {
                        Debug.Log("PreviousSelectedWeapon: " + previousSelectedWeapon);
                        //if (previousSelectedWeapon <= -1) previousSelectedWeapon = 0;



                        // INSERT CODE HERE TO CHECK IF THERE IS AN ACTION PLAYING
                        if (previousWeapon.GetComponent<WeaponController>().anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                        {
                            Debug.Log("SelectedWeapon: " + selectedWeapon);

                            previousWeapon.GetComponent<WeaponController>().SwitchAway();

                            StartCoroutine(SwitchIENumerator(0.1f, currentWeapon, true, true));
                            StartCoroutine(SwitchIENumerator(0.1f, previousWeapon, false, false));
                            return;
                        }
                        else
                        {
                            // RESET SELECTED WEAPON VARIABLE

                        }
                    }
                    //weapon.gameObject.GetComponent<WeaponController>().SwitchTo();
                }
                else
                {

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

        bool isPlaying(Animator anim, string stateName)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                    anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                return true;
            else
                return false;
        }
    }
}