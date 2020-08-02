using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

namespace EightDirectionalSpriteSystem
{
    public class TextManager : MonoBehaviour
    {
        #region Public Variables
        // Instantiates Singleton
        public static TextManager Instance { set; get; }

        public PlayerController playerController;
        public WeaponSwitching weaponSwitching;

        public TextMeshProUGUI healthText;
        public TextMeshProUGUI armorText;
        public TextMeshProUGUI curAmmoText;
        public TextMeshProUGUI maxAmmoText;

        #endregion
        private Transform currentWeapon;

        void Awake()
        {
            // Sets Singleton
            Instance = this;

            if (Instance == this) Debug.Log("TextManager Singleton Initialized");
        }

        // Start is called before the first frame update
        void Start()
        {
            UpdateAmmoText();
            UpdateHealthArmorText();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateAmmoText()
        {
            FindActiveWeapon();
            if (currentWeapon != null)
            {
                //curAmmoText.text = currentWeapon.GetComponent<WeaponController>().CurAmmo.ToString();
                //maxAmmoText.text = currentWeapon.GetComponent<WeaponController>().maxAmmo.ToString();

                if (currentWeapon.name == "Pistol")
                {
                    curAmmoText.text = currentWeapon.GetComponent<PistolController>().CurAmmo.ToString();
                    maxAmmoText.text = AmmoInventory.Instance.curPistolAmmo.ToString();
                }
                else if (currentWeapon.name == "Shotgun")
                {
                    curAmmoText.text = currentWeapon.GetComponent<ShotgunController>().CurAmmo.ToString();
                    maxAmmoText.text = AmmoInventory.Instance.curShotgunAmmo.ToString();
                }

                //Debug.Log("Update Ammo Text");
            }
            else
            {
                curAmmoText.text = "0";
                maxAmmoText.text = "0";

                Debug.Log("No Active Weapons");
            }
        }

        public void UpdateHealthArmorText()
        {
            healthText.text = "Health : " + playerController.CurHealth.ToString();
            armorText.text = "Armor : " + playerController.CurArmor.ToString();
        }

        private void FindActiveWeapon()
        {
            foreach (Transform weapon in weaponSwitching.gameObject.transform)
            {
                if (weapon.gameObject.activeSelf) currentWeapon = weapon;
            }
        }
    }
}
