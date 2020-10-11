using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EightDirectionalSpriteSystem
{
    public class HealthPotion : PickUpController
    {
        public int numberOfHealth = 0;

        void Start()
        {
            PickUpController Health = new PickUpController();
            playerController = GameManager.Instance.playerGo.GetComponent<PlayerController>();
            audioSource = GetComponent<AudioSource>();

            audioSource.clip = ambientSound;
            audioSource.Play();

            Health.itemName = "Health";

            //EndGameScreen.Instance.totalHealth += numberOfHealth;
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

            if (other.CompareTag("Player") && numberOfHealth >= 1) EndGameScreen.Instance.healthFound++;
        }
    }
}



