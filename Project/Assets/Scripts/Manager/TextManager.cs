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

        // Start is called before the first frame update
        void Start()
        {
            // Sets Singleton
            Instance = this;



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
                curAmmoText.text = currentWeapon.GetComponent<WeaponController>().CurAmmo.ToString();
                maxAmmoText.text = currentWeapon.GetComponent<WeaponController>().maxAmmo.ToString();

                Debug.Log("Update Ammo Text");
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
