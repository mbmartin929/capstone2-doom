using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EightDirectionalSpriteSystem
{
    public class AmmoPickUp : PickUpController
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Transform playerWeapons = other.GetComponent<PlayerController>().weapons;

                // Player picks up a weapon
                PickUpWeapon(playerWeapons);

                // Player picks up normal ammo
                PickUpAmmo(playerWeapons);

                Destroy(gameObject, 0f);
            }
        }

        private void PickUpWeapon(Transform playerWeapons)
        {
            if (itemName == "Pistol")
            {
                Transform currentWeapon = SearchWeapons(playerWeapons, "Pistol");

                //  If player doesn't have this weapon yet
                if (currentWeapon == null)
                {
                    GameObject pistol = Instantiate(weapon, playerWeapons) as GameObject;
                    pistol.name = "Pistol";
                    if (playerWeapons.childCount >= 2)
                    {
                        pistol.SetActive(false);
                    }
                }
                // Player has this weapon already. Will add ammo instead
                else
                {
                    currentWeapon.GetComponent<WeaponController>().maxAmmo += recoverAmount;
                }
            }
            else if (itemName == "Shotgun")
            {
                Transform currentWeapon = SearchWeapons(playerWeapons, "Shotgun");
                if (currentWeapon == null)
                {
                    GameObject shotgun = Instantiate(weapon, playerWeapons) as GameObject;
                    shotgun.name = "Shotgun";
                    if (playerWeapons.childCount >= 2)
                    {
                        shotgun.SetActive(false);
                    }
                }
                else
                {
                    currentWeapon.GetComponent<WeaponController>().maxAmmo += recoverAmount;
                }
            }

            TextManager.Instance.UpdateAmmoText();
        }

        private void PickUpAmmo(Transform playerWeapons)
        {
            // Finds correct ammo type
            if (ammoType == AmmoType.Pistol)
            {
                Transform currentWeapon = SearchWeapons(playerWeapons, "Pistol");
                currentWeapon.GetComponent<WeaponController>().maxAmmo += recoverAmount;
            }
            else if (ammoType == AmmoType.Shotgun)
            {
                Transform currentWeapon = SearchWeapons(playerWeapons, "Shotgun");
                currentWeapon.GetComponent<WeaponController>().maxAmmo += recoverAmount;
            }

            TextManager.Instance.UpdateAmmoText();
        }

        private Transform SearchWeapons(Transform playerWeapons, string weaponName)
        {
            foreach (Transform weapon in playerWeapons)
            {
                if (weapon.name == weaponName)
                {
                    //Debug.Log("Return Weapon");
                    return weapon;
                }
            }
            return null;
        }
    }
}