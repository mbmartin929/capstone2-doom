using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EightDirectionalSpriteSystem;

public class AmmoPickUp : PickUpController
{
    public enum AmmoType
    {
        Pistol, Shotgun, Launcher
    }
    public AmmoType ammoType;
    public GameObject weapon;

    public int numberOfAmmo = 0;

    void Start()
    {
        EndGameScreen.Instance.totalAmmo += numberOfAmmo;
    }

    void Update()
    {

    }

    private void LateUpdate()
    {
        if (itemName == "Ammo")
        {
            if (CheckCloseToTag("Player", distanceToPickUp))
            {
                if (fraction < 1)
                {
                    fraction += lerpSpeed * Time.deltaTime;
                    transform.position = Vector3.Lerp(transform.position, target, fraction);
                }
            }
        }
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
        else if (itemName == "Shotgun" || itemName == "Shotgun_URP")
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
        else if (itemName == "Launcher")
        {
            Transform currentWeapon = SearchWeapons(playerWeapons, "Launcher");
            if (currentWeapon == null)
            {
                GameObject launcher = Instantiate(weapon, playerWeapons) as GameObject;
                launcher.name = "Launcher";
                if (playerWeapons.childCount >= 2)
                {
                    launcher.SetActive(false);
                }

                DialogueAssistant.Instance.StartCoroutine(DialogueAssistant.Instance.SwitchShotgun());
            }
            else
            {
                //currentWeapon.GetComponent<WeaponController>().maxAmmo += recoverAmount;
                AmmoInventory.Instance.PickUpLauncherAmmo(recoverAmount, currentWeapon);
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
        else if (ammoType == AmmoType.Launcher)
        {
            Transform currentWeapon = SearchWeapons(playerWeapons, "Launcher");

            AmmoInventory.Instance.PickUpLauncherAmmo(recoverAmount, currentWeapon);
        }

        GameObject pickUpSFX = new GameObject();
        GameObject _pickUpSFX = Instantiate(pickUpSFX, transform.position, Quaternion.identity);
        _pickUpSFX.name = "PickUp SFX";

        _pickUpSFX.AddComponent<AudioSource>();
        _pickUpSFX.GetComponent<AudioSource>().priority = 29;
        _pickUpSFX.GetComponent<AudioSource>().volume = 0.5f;
        _pickUpSFX.GetComponent<AudioSource>().PlayOneShot(pickUpSound);

        Destroy(_pickUpSFX, 2.9f);

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
