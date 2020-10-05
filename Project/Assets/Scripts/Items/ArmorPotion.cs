using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EightDirectionalSpriteSystem
{
    public class ArmorPotion : PickUpController
    {
        public int numberOfArmor = 0;

        void Start()
        {
            PickUpController Armor = new PickUpController();
            playerController = GameManager.Instance.playerGo.GetComponent<PlayerController>();
            audioSource = GetComponent<AudioSource>();

            audioSource.clip = ambientSound;
            audioSource.Play();

            Armor.itemName = "Armor";

            EndGameScreen.Instance.totalArmor += numberOfArmor;
        }

        void Update()
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

        private void OnTriggerEnter(Collider other)
        {
            HealthArmorPickUp(other);

            if (CompareTag("Player") && numberOfArmor >= 1) EndGameScreen.Instance.armorFound++;
        }
    }
}
