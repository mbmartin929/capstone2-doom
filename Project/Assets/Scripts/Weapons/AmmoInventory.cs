using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class AmmoInventory : MonoBehaviour
    {
        // Instantiates Singleton
        public static AmmoInventory Instance { set; get; }

        public int curPistolAmmo;
        public int maxPistolCapacity = 41;
        public int curShotgunAmmo;
        public int maxShotgunCapacity = 21;

        void Awake()
        {
            // Sets Singleton
            Instance = this;

            if (Instance == this) Debug.Log("AmmoInventory Singleton Initialized");
        }

        public void PickUpPistolAmmo(int amount, Transform currentWeapon)
        {
            if (currentWeapon == null)
            {
                curPistolAmmo += amount;
                TextManager.Instance.UpdateAmmoText();
                return;
            }
            PistolController weapon = currentWeapon.GetComponent<PistolController>();

            curPistolAmmo += amount;
            if (curPistolAmmo >= maxPistolCapacity) curPistolAmmo = maxPistolCapacity;
            weapon.CurAmmo = curPistolAmmo;

            TextManager.Instance.UpdateAmmoText();
        }

        public void PickUpShotgunAmmo(int amount, Transform currentWeapon)
        {
            if (currentWeapon == null)
            {
                curShotgunAmmo += amount;
                TextManager.Instance.UpdateAmmoText();
                return;
            }
            ShotgunController weapon = currentWeapon.GetComponent<ShotgunController>();

            curShotgunAmmo += amount;
            if (curShotgunAmmo >= maxShotgunCapacity) curShotgunAmmo = maxShotgunCapacity;
            weapon.CurAmmo = curShotgunAmmo;

            TextManager.Instance.UpdateAmmoText();
        }
    }
}