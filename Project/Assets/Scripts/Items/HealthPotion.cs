using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EightDirectionalSpriteSystem
{
    public class HealthPotion : PickUpController
    {

        private int HealAmountHp;
        public float time;

        void Start()
        {
            PickUpController Potion = new PickUpController();
            playerController = GameManager.Instance.playerGo.GetComponent<PlayerController>();

            Potion.itemName = "Heart";
            Potion.recoverAmount = HealAmountHp;
        }



        private void OnTriggerEnter(Collider other)
        {
            PickUp(other);
        }

        IEnumerator blinkImage()
        {

            yield return new WaitForSeconds(time);
            overlayImage.SetEnabled(false);

        }
    }
}



