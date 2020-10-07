using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EightDirectionalSpriteSystem
{
    public class WeaponSwitching : MonoBehaviour
    {
        public int selectedWeapon = 0;

        [SerializeField] private RawImage rawImage;
        [SerializeField] private Texture2D[] ammoIcons;

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
                Debug.Log("Pressed 1 Switch Weapon");
                selectedWeapon = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("Pressed 2 Switch Weapon");
                selectedWeapon = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("Pressed 3 Switch Weapon");
                selectedWeapon = 2;
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

                // TESTING
                // if (previousWeapon.GetComponent<WeaponController>().anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                // {
                //     selectedWeapon = previousSelectedWeapon;
                // }

                if (i == selectedWeapon)
                {
                    if (!currentWeapon.gameObject.activeSelf)
                    {
                        //Debug.Log("PreviousSelectedWeapon: " + previousSelectedWeapon);
                        //if (previousSelectedWeapon <= -1) previousSelectedWeapon = 0;

                        //Debug.Log(previousWeapon);


                        // INSERT CODE HERE TO CHECK IF THERE IS AN ACTION PLAYING
                        if (previousWeapon.GetComponent<WeaponController>().anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                        {
                            //Debug.Log("SelectedWeapon: " + selectedWeapon);
                            Debug.Log("Current Weapon: " + currentWeapon.gameObject.name);

                            previousWeapon.GetComponent<WeaponController>().SwitchAway();

                            StartCoroutine(SwitchIENumerator(0.1f, currentWeapon, true, true));
                            StartCoroutine(SwitchIENumerator(0.1f, previousWeapon, false, false));

                            if (currentWeapon.gameObject.name == "Fists")
                            {
                                rawImage.texture = ammoIcons[0];
                            }
                            else if (currentWeapon.gameObject.name == "Pistol")
                            {
                                rawImage.texture = ammoIcons[1];
                            }
                            else if (currentWeapon.gameObject.name == "Shotgun")
                            {
                                rawImage.texture = ammoIcons[2];
                            }

                            return;
                        }
                        else
                        {
                            // RESET SELECTED WEAPON VARIABLE

                        }
                    }
                    TextManager.Instance.UpdateAmmoText();
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
                TextManager.Instance.UpdateAmmoText();
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