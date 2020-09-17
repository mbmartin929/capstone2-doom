using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class ArmorPotion : PickUpController
    {
        private int HealAmountArmor;

        void Start()
        {
            PickUpController Armor = new PickUpController();
            playerController = GameManager.Instance.playerGo.GetComponent<PlayerController>();
            audioSource = GetComponent<AudioSource>();

            audioSource.clip = ambientSound;
            audioSource.Play();

            Armor.itemName = "Armor";
            Armor.recoverAmount = HealAmountArmor;
        }


        private void OnTriggerEnter(Collider other)
        {
            PickUp(other);
        }

        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
                playerController.currentArmor += recoverAmount;

                PickUpOverlayManager.Instance.ShieldOverlay();

                Debug.Log("Armor PICKED! " + recoverAmount);

                TextManager.Instance.UpdateHealthArmorText();

                Destroy(this.gameObject);
            }
        }
    }
}
