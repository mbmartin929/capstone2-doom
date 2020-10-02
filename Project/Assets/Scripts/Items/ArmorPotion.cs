using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class ArmorPotion : PickUpController
    {
        void Start()
        {
            PickUpController Armor = new PickUpController();
            playerController = GameManager.Instance.playerGo.GetComponent<PlayerController>();
            audioSource = GetComponent<AudioSource>();

            audioSource.clip = ambientSound;
            audioSource.Play();

            Armor.itemName = "Armor";
        }

        private void OnTriggerEnter(Collider other)
        {
            HealthArmorPickUp(other);
        }
    }
}
