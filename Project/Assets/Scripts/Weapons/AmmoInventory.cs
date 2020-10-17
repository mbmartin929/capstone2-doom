﻿using System.Collections;
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
        public int curLauncherAmmo;
        public int maxLauncherCapacity = 21;

        void Awake()
        {
            Debug.Log("hi");

            // Sets Singleton
            Instance = this;

            if (Instance == this) Debug.Log("AmmoInventory Singleton Initialized");
        }

        public void PickUpPistolAmmo(int amount, Transform currentWeapon)
        {
            //WeaponController weapon = currentWeapon.GetComponent<WeaponController>();
            curPistolAmmo += amount;
            if (curPistolAmmo >= maxPistolCapacity) curPistolAmmo = maxPistolCapacity;

            TextManager.Instance.UpdateAmmoText();
        }

        public void PickUpShotgunAmmo(int amount, Transform currentWeapon)
        {
            //WeaponController weapon = currentWeapon.GetComponent<WeaponController>();
            curShotgunAmmo += amount;
            if (curShotgunAmmo >= maxShotgunCapacity) curShotgunAmmo = maxShotgunCapacity;

            TextManager.Instance.UpdateAmmoText();
        }

        public void PickUpLaunchernAmmo(int amount, Transform currentWeapon)
        {
            //WeaponController weapon = currentWeapon.GetComponent<WeaponController>();
            curLauncherAmmo += amount;
            if (curLauncherAmmo >= maxLauncherCapacity) curLauncherAmmo = maxShotgunCapacity;

            TextManager.Instance.UpdateAmmoText();
        }
    }
}