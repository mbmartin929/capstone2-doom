using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class AmmoPickUp : PickUpController
{
    public enum AmmoType
    {
        Pistol, Shotgun
    }
    public AmmoType ammoType;
    public GameObject weapon;

    public int numberOfAmmo = 0;

    void Start()
    {
        EndGameScreen.Instance.totalAmmo += numberOfAmmo;
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

                DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.SwitchPistol());
            }
            // Player has this weapon already. Will add ammo instead
            else
            {
                //currentWeapon.GetComponent<WeaponController>().maxAmmo += recoverAmount;
                AmmoInventory.Instance.PickUpPistolAmmo(recoverAmount, currentWeapon);
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

                DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.SwitchShotgun());
            }
            else
            {
                //currentWeapon.GetComponent<WeaponController>().maxAmmo += recoverAmount;
                AmmoInventory.Instance.PickUpShotgunAmmo(recoverAmount, currentWeapon);
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

            AmmoInventory.Instance.PickUpPistolAmmo(recoverAmount, currentWeapon);
        }
        else if (ammoType == AmmoType.Shotgun)
        {
            Transform currentWeapon = SearchWeapons(playerWeapons, "Shotgun");

            AmmoInventory.Instance.PickUpShotgunAmmo(recoverAmount, currentWeapon);
        }

        GameObject pickUpSFX = new GameObject();
        GameObject _pickUpSFX = Instantiate(pickUpSFX, transform.position, Quaternion.identity);
        _pickUpSFX.name = "PickUp SFX";

        _pickUpSFX.AddComponent<AudioSource>();
        _pickUpSFX.GetComponent<AudioSource>().volume = 0.5f;
        _pickUpSFX.GetComponent<AudioSource>().PlayOneShot(pickUpSound);

        Debug.Log("Pick up Ammo");

        PickUpOverlayManager.Instance.AmmoOverlay();

        if (numberOfAmmo >= 1) EndGameScreen.Instance.ammoFound++;

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
