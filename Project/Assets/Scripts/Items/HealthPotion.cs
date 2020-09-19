using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EightDirectionalSpriteSystem
{
    public class HealthPotion : PickUpController
    {
        void Start()
        {
            PickUpController Health = new PickUpController();
            playerController = GameManager.Instance.playerGo.GetComponent<PlayerController>();
            audioSource = GetComponent<AudioSource>();

            audioSource.clip = ambientSound;
            audioSource.Play();

            Health.itemName = "Health";
        }

        private void OnTriggerEnter(Collider other)
        {
            PickUp(other);
        }
    }
}



