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
        public int curShotgunAmmo;

        // [SerializeField] private int curPistolAmmo;

        // [SerializeField] private int curShotgunAmmo;

        // public int CurAmmo
        // {
        //     get { return curAmmo; }
        //     set
        //     {
        //         curAmmo = value;
        //         if (curAmmo < 0) curAmmo = 0;
        //         if (curAmmo > maxCapacity) curAmmo = maxCapacity;
        //     }
        // }

        void Awake()
        {
            // Sets Singleton
            Instance = this;
        }

        public void PickUpPistolAmmo(int amount, Transform currentWeapon)
        {
            WeaponController weapon = currentWeapon.GetComponent<WeaponController>();
            curPistolAmmo += amount;
            if (curPistolAmmo >= weapon.maxCapacity) curPistolAmmo = weapon.maxCapacity;

            TextManager.Instance.UpdateAmmoText();
        }

        public void PickUpShotgunAmmo(int amount, Transform currentWeapon)
        {
            WeaponController weapon = currentWeapon.GetComponent<WeaponController>();
            curShotgunAmmo += amount;
            if (curShotgunAmmo >= weapon.maxCapacity) curShotgunAmmo = weapon.maxCapacity;

            TextManager.Instance.UpdateAmmoText();
        }
    }
}