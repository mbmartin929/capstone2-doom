using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace EightDirectionalSpriteSystem
{
    public class PickUpController : MonoBehaviour
    {
        public float distanceToPickUp = 3.69f;
        public float lerpSpeed = 0.69f;

        public Image overlayImage;
        public AudioClip ambientSound;
        public AudioClip pickUpSound;
        protected PlayerController playerController;
        protected AudioSource audioSource;

        public string itemName;
        public int recoverAmount;

        protected float fraction = 0;
        protected Vector3 target;

        private void Start()
        {
            playerController = GameManager.Instance.playerGo.GetComponent<PlayerController>();
            //overlayImage.SetEnabled(false);
        }

        protected bool CheckCloseToTag(string tag, float minimumDistance)
        {
            GameObject[] goWithTag = GameObject.FindGameObjectsWithTag(tag);

            for (int i = 0; i < goWithTag.Length; ++i)
            {
                if (Vector3.Distance(transform.position, goWithTag[i].transform.position) <= minimumDistance)
                {
                    //target = goWithTag[i].transform.position;
                    target = Camera.main.transform.position;
                    return true;
                }
            }
            return false;
        }

        protected void HealthArmorPickUp(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (itemName == "Armor")
                {
                    playerController.RecoverArmor(recoverAmount);
                    PickUpOverlayManager.Instance.ShieldOverlay();
                    Debug.Log("Armor PICKED! " + recoverAmount);
                }
                else if (itemName == "Health")
                {
                    playerController.RecoverHealth(recoverAmount);
                    PickUpOverlayManager.Instance.HealthOverlay();
                    Debug.Log("Health PICKED! " + recoverAmount);
                }

                TextManager.Instance.UpdateHealthArmorText();

                GameObject pickUpSFX = new GameObject();
                GameObject _pickUpSFX = Instantiate(pickUpSFX, transform.position, Quaternion.identity);
                _pickUpSFX.name = "PickUp SFX";

                _pickUpSFX.AddComponent<AudioSource>();
                _pickUpSFX.GetComponent<AudioSource>().volume = 0.42f;
                _pickUpSFX.GetComponent<AudioSource>().PlayOneShot(pickUpSound);

                Destroy(_pickUpSFX, 2.0f);

                //audioSource.PlayOneShot(pickUpSound);

                Destroy(this.gameObject);
            }
        }
    }
}
