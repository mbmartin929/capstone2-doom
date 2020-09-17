using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace EightDirectionalSpriteSystem
{
    public class PickUpController : MonoBehaviour
    {
        public Image overlayImage;
        protected PlayerController playerController;
        public enum AmmoType
        {
            Pistol, Shotgun
        }
        public AmmoType ammoType;

        public string itemName;
        public int recoverAmount;
        public GameObject weapon;

        private void Start()
        {
            playerController = GameManager.Instance.playerGo.GetComponent<PlayerController>();
            overlayImage.SetEnabled(false);
        }
    }
}
